namespace Servicio
{
    using AutoMapper;
    using Dtos;
    using Microsoft.AspNetCore.Http;
    using Modelos;
    using Repositorio;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Transactions;

    public class BlogService : IBlogService
    {
        #region Propiedades
        private readonly IBlogRepository blogRepository;
        private readonly IUsuarioService usuarioService;
        private readonly IMapper mapper;
        #endregion
        #region Constructores
        public BlogService(IBlogRepository blogRepository, IMapper mapper, IUsuarioService usuarioService)
        {
            this.blogRepository = blogRepository;
            this.mapper = mapper;
            this.usuarioService = usuarioService;
        }
        #endregion
        #region Metodos y funciones
        /// <summary>
        /// Obtiene una unica entrada dado un slug
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public BlogDetalleDto MostrarEntradaPorSlug(string slug, bool estado)
        {
            try
            {
                Blogs blog = this.blogRepository.ObtenerSlug(slug);
                if (blog is null)
                    throw new NegocioExecption("No existe el slug", 404);
                BlogDetalleDto blogDto = this.blogRepository.MostrarEntradaPorSlug(slug, estado);
                return blogDto;
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
        /// Listado de todos los entradas disponibles ordenadas de fecha mas reciente
        /// </summary>
        /// <param name="entrada"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public List<BlogDto> MostrarListadoEntradas(string entrada, bool estado)
        {
            try
            {
                List<BlogDto> blogs = this.blogRepository.MostrarListadoEntradas(entrada, estado);
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
                if (categorias.Count > 0)
                {
                    List<CategoriasDto> categoriasDtos = new List<CategoriasDto>();
                    foreach (var line in categorias.GroupBy(info => info.Nombre)
                        .Select(group => new CategoriasDto
                        {
                            Nombre = group.Key,
                            Id = group.Select(s => s.Id).FirstOrDefault(),
                            Cantidad = group.Count()
                        }).OrderBy(x => x.Cantidad))
                    {
                        if (line.Cantidad > 0)
                        {
                            CategoriasDto categoriasDto = new CategoriasDto
                            {
                                Cantidad = line.Cantidad,
                                Nombre = line.Nombre,
                                Id = line.Id
                            };
                            categoriasDtos.Add(categoriasDto);
                        }
                    }
                    return categoriasDtos.OrderByDescending(o => o.Cantidad).ToList();
                }
                return null;
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
        /// <param name="estado"></param>
        /// <returns></returns>
        public List<BlogDto> ListarPostCategoria(string categoria, bool estado)
        {
            try
            {
                List<BlogDto> blogs = this.blogRepository.MostrarListadoEntradas("", estado)
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

        /// <summary>
        /// Lista las palabras clave disponibles
        /// </summary>
        /// <returns></returns>
        public List<KeyWordDto> ListarKeyWords()
        {
            try
            {
                List<KeyWords> keyWords = this.blogRepository.ListarKeyWords();
                List<KeyWordDto> keyWordDto = mapper.Map<List<KeyWordDto>>(keyWords);
                return keyWordDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Guaerda en la m to m de blogkey
        /// </summary>
        /// <param name="blogKeys"></param>
        public void GuardarKeyWords(List<KeyWordDto> keyDto, Blogs blogs)
        {
            try
            {
                List<int> key = keyDto.Select(s => s.Id).ToList();
                List<BlogKeyWordsDto> keyWordsDto = new List<BlogKeyWordsDto>();
                foreach (int item in key)
                {
                    BlogKeyWordsDto keyWord = new BlogKeyWordsDto()
                    {
                        IdBlog = blogs.Idblog,
                        IdKey = item
                    };
                    keyWordsDto.Add(keyWord);
                }
                List<BlogKey> keyWords = mapper.Map<List<BlogKey>>(keyWordsDto);
                this.blogRepository.GuardarKeyWords(keyWords);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Busca el id de una imagen por la ruta de ella
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        private int BuscarImagenPorRuta(string ruta)
        {
            try
            {
                return this.blogRepository.BuscarImagenPorRuta(ruta).Idimagen;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Guarda un post
        /// </summary>
        /// <param name="blogDto"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public ApiCallResult GuardarPost(BlogDetalleDto blogDto)
        {
            try
            {
                Blogs slug = this.blogRepository.ObtenerSlug(blogDto.Slug);
                if (slug != null)
                    throw new NegocioExecption($"Ya existe ha sido creado previamente un post con este SLUG {blogDto.Slug}", 202);
                BusquedaDto titulo = this.BuscarPost(blogDto.Titulo).FirstOrDefault();
                if (!string.IsNullOrEmpty(titulo.Slug))
                    throw new NegocioExecption($"Ya existe ha sido creado previamente un post con este Titulo {blogDto.Titulo}", 202);
                UsuarioDto usuario = this.usuarioService.VerificarUsuario(blogDto.Creador);
                if (usuario is null)
                    throw new NegocioExecption($"Error de logeo con {blogDto.Creador}", 401);

                #region Guardar imagenes
                this.GuardarImagenPost(blogDto.ImagenPost);
                #endregion
                #region Guardar post
                blogDto.Idcreador = usuario.Idusuario;
                blogDto.Idimagen = this.BuscarImagenPorRuta(blogDto.ImagenPost);
                blogDto.FechaCreacion = DateTime.Now;
                blogDto.Tipo = "PO";
                blogDto.Estado = true;
                Blogs blog = mapper.Map<Blogs>(blogDto);
                this.blogRepository.GuardarPost(blog);
                #endregion

                #region Guardar keywords
                Blogs blogCreado = this.blogRepository.ObtenerSlug(blogDto.Slug);
                this.GuardarKeyWords(blogDto.KeyWords, blogCreado);
                #endregion

                return new ApiCallResult
                {
                    Estado = true,
                    Mensaje = "Post guardado"
                };
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
        /// Guarda la imagen de un blog en el servidor
        /// </summary>
        /// <param name="formFile"></param>
        public string GuardarImagenServidor(IFormFile formFile)
        {
            try
            {
                string ruta = "";
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (formFile.Length > 0)
                {
                    string fileName = DateTime.Now.Millisecond.ToString() +
                        ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(pathToSave, fileName);
                    ruta = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }
                }
                return ruta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Guarda la ruta de la imagen en la bd
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        private void GuardarImagenPost(string ruta)
        {
            try
            {
                ImagenesDto imagenesDto = new ImagenesDto()
                {
                    Estado = true,
                    Fechacreacion = DateTime.Now,
                    Nombre = ruta,
                    Ruta = ruta
                };
                Imagenes imagenes = mapper.Map<Imagenes>(imagenesDto);
                this.blogRepository.GuardarImagenPost(imagenes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista todas las categorias
        /// </summary>
        /// <returns></returns>
        public List<CategoriasDto> ListarTodasCategorias()
        {
            try
            {
                List<Categorias> categorias = this.blogRepository.ListarTodasCategorias();
                List<CategoriasDto> categoriasDto = mapper.Map<List<CategoriasDto>>(categorias);
                return categoriasDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
