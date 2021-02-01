namespace Servicio
{
    using AutoMapper;
    using Dtos;
    using Modelos;
    using Repositorio;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BlogService : IBlogService
    {
        #region Propiedades
        private readonly IBlogRepository blogRepository;
        private readonly IMapper mapper;
        #endregion
        #region Constructores
        public BlogService(IBlogRepository blogRepository, IMapper mapper)
        {
            this.blogRepository = blogRepository;
            this.mapper = mapper;
        }
        #endregion
        #region Metodos y funciones
        /// <summary>
        /// Obtiene una unica entrada dado un slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public BlogDetalleDto MostrarEntradaPorSlug(string slug)
        {
            try
            {
                Blogs blog = this.blogRepository.ObtenerSlug(slug);
                if (blog is null)
                    throw new NegocioExecption("No existe el slug", 404);
                BlogDetalleDto blogDto = this.blogRepository.MostrarEntradaPorSlug(slug);
                return blogDto;
            }
            catch (NegocioExecption e)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Listado de todos los entradas disponibles ordenadas de fecha mas reciente
        /// </summary>
        /// <returns></returns>
        public List<BlogDto> MostrarListadoEntradas()
        {
            try
            {
                List<BlogDto> blogs = this.blogRepository.MostrarListadoEntradas();
                return blogs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista los 5 post mas recientes
        /// </summary>
        /// <returns></returns>
        public List<PostRecienteDto> ListarRecientes()
        {
            try
            {
                List<PostRecienteDto> posts = this.blogRepository.ListarRecientes();
                return posts;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista todos los comentarios de un post en especifico
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public List<ComentarioDto> MostrarComentarios(string slug)
        {
            try
            {
                Blogs blog = this.blogRepository.ObtenerSlug(slug);
                if (blog is null)
                    throw new NegocioExecption("No existe el slug", 404);
                List<ComentarioDto> comentarios = this.blogRepository.MostrarComentarios(slug);
                return comentarios;
            }
            catch (NegocioExecption)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Guarda el comentario de un post en especifico
        /// </summary>
        /// <param name="comentarioDto"></param>
        /// <returns></returns>
        public ApiCallResult GuardarComentario(ComentarioSavedDto comentarioDto)
        {
            try
            {
                Blogs blog = this.blogRepository.ObtenerSlug(comentarioDto.Slug);
                if (blog is null)
                    throw new Exception("No existe la entrada");
                Comentarios comentario = mapper.Map<Comentarios>(comentarioDto);
                comentario.Idblog = blog.Idblog;
                comentario.Fechacreaciion = DateTime.Now;
                this.blogRepository.GuardarComentario(comentario);
                return new ApiCallResult
                {
                    Estado = true,
                    Mensaje = "Comentario guardado"
                };
            }
            catch (Exception e)
            {
                return new ApiCallResult
                {
                    Estado = false,
                    Mensaje = $"Error en {e.Message}"
                };
            }
        }

        /// <summary>
        /// Hace una busqueda de los posts con la coincidencia de busqueda
        /// </summary>
        /// <param name="busqueda"></param>
        /// <returns></returns>
        public List<BusquedaDto> BuscarPost(string busqueda)
        {
            try
            {
                List<BusquedaDto> busquedasDto = this.blogRepository.BuscarPost(busqueda);
                List<BusquedaDto> busquedas = new List<BusquedaDto>();

                if (busquedasDto.Count == 0)
                {
                    busquedas.Add(new BusquedaDto { Slug = "", Titulo = "No hay resultados" });
                    return busquedas;
                }

                busquedas.AddRange(from item in busquedasDto.GroupBy(g => g.Slug)
                                   let busquedaDto = new BusquedaDto
                                   {
                                       Slug = item.Select(s => s.Slug).FirstOrDefault(),
                                       Titulo = item.Select(s => s.Titulo).FirstOrDefault()
                                   }
                                   select busquedaDto);
                return busquedas;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista todas las categorias con la catidad de post que tienen
        /// </summary>
        /// <returns></returns>
        public List<CategoriasDto> ListarCategorias()
        {
            try
            {
                List<CategoriasDto> categorias = this.blogRepository.ListarCategorias();
                if (categorias.Count <= 0)
                    throw new Exception("No hay categorias");

                List<CategoriasDto> categoriasDtos = new List<CategoriasDto>();
                foreach (var line in categorias.GroupBy(info => info.Nombre)
                    .Select(group => new CategoriasDto
                    {
                        Nombre = group.Key,
                        Cantidad = group.Count()
                    }).OrderBy(x => x.Cantidad))
                {
                    if (line.Cantidad > 0)
                    {
                        CategoriasDto categoriasDto = new CategoriasDto
                        {
                            Cantidad = line.Cantidad,
                            Nombre = line.Nombre
                        };
                        categoriasDtos.Add(categoriasDto);
                    }
                }
                return categoriasDtos.OrderByDescending(o => o.Cantidad).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista los post que tiene una categoria especifica por ordern de creacion
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public List<BlogDto> ListarPostCategoria(string categoria)
        {
            try
            {
                List<BlogDto> blogs = this.blogRepository.MostrarListadoEntradas()
                    .Where(w => w.Categoria.ToLower() == categoria.ToLower())
                    .OrderByDescending(o => o.FechaCreacion)
                    .ToList();
                return blogs;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
