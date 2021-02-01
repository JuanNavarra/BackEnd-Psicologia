namespace Repositorio
{
    using Dtos;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using Modelos;

    public class BlogRepository : IBlogRepository
    {
        #region Propiedades
        private readonly PsicologiaContext context;
        #endregion
        #region Constructores
        public BlogRepository(PsicologiaContext context)
        {
            this.context = context;
        }
        #endregion
        #region Metodos y funciones
        /// <summary>
        /// Verifica si el slug existe
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public Blogs ObtenerSlug(string slug)
        {
            try
            {
                Blogs blog = this.context.Blogs.Where(w => w.Estado && w.Slug == slug).FirstOrDefault();
                return blog;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene una unica entrada dado un slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public BlogDetalleDto MostrarEntradaPorSlug(string slug)
        {
            try
            {
                BlogDetalleDto blog = (from t0 in context.Blogs
                                       join t6 in context.Imagenes on t0.Idimagen equals t6.Idimagen
                                       join t3 in context.Categorias on t0.Idcategoria equals t3.Idcategoria
                                       join t4 in context.Usuarios on t0.Idcreador equals t4.Idusuario
                                       join t5 in context.Imagenes on t4.Idimagen equals t5.Idimagen
                                       where t0.Estado && t0.Slug == slug
                                       select new BlogDetalleDto
                                       {
                                           SubTitulo = t0.Subtitulo,
                                           Categoria = t3.Nombre,
                                           Creador = t4.Nombre + " " + t4.Apellido,
                                           Descripcion = t0.Descripcion,
                                           FechaCreacion = t0.Fechacreacion,
                                           ImagenCreador = t5.Ruta,
                                           ImagenPost = t6.Ruta,
                                           IdBlog = t0.Idblog,
                                           Titulo = t0.Titulo,
                                           Slug = t0.Slug,
                                           Cita = t0.Cita,
                                           AutorCita = t0.Autorcita,
                                           KeyWords = (from t7 in context.Blogs
                                                       join t1 in context.BlogKey on t0.Idblog equals t1.Idblog
                                                       join t2 in context.KeyWords on t1.Idkey equals t2.Idkey
                                                       where t7.Idblog == t0.Idblog
                                                       select new KeyWordDto
                                                       {
                                                           Nombre = t2.Nombre
                                                       }).ToList()
                                       }).FirstOrDefault();
                return blog;
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
                List<BlogDto> blogs = (from t0 in context.Blogs
                                       join t6 in context.Imagenes on t0.Idimagen equals t6.Idimagen
                                       join t3 in context.Categorias on t0.Idcategoria equals t3.Idcategoria
                                       join t4 in context.Usuarios on t0.Idcreador equals t4.Idusuario
                                       join t5 in context.Imagenes on t4.Idimagen equals t5.Idimagen
                                       where t0.Estado
                                       select new BlogDto
                                       {
                                           SubTitulo = t0.Subtitulo,
                                           Categoria = t3.Nombre,
                                           Creador = t4.Nombre + " " + t4.Apellido,
                                           Descripcion = t0.Descripcion.Length > 500 ? t0.Descripcion.Substring(0, 499) : t0.Descripcion,
                                           FechaCreacion = t0.Fechacreacion,
                                           ImagenCreador = t5.Ruta,
                                           ImagenPost = t6.Ruta,
                                           IdBlog = t0.Idblog,
                                           Titulo = t0.Titulo,
                                           Slug = t0.Slug,
                                           KeyWords = (from t7 in context.Blogs
                                                       join t1 in context.BlogKey on t0.Idblog equals t1.Idblog
                                                       join t2 in context.KeyWords on t1.Idkey equals t2.Idkey
                                                       where t0.Idblog == t7.Idblog
                                                       select new KeyWordDto
                                                       {
                                                           Nombre = t2.Nombre
                                                       }).ToList()
                                       }).ToList();
                return blogs.OrderByDescending(o => o.FechaCreacion).ToList();
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
            List<PostRecienteDto> posts = (from t0 in context.Blogs
                                           join t1 in context.Imagenes on t0.Idimagen equals t1.Idimagen
                                           where t0.Estado
                                           select new PostRecienteDto
                                           {
                                               FechaCreacion = t0.Fechacreacion,
                                               Slug = t0.Slug,
                                               Imagen = t1.Ruta,
                                               Titulo = t0.Titulo,
                                           }).Take(5).ToList();
            return posts.OrderByDescending(o => o.FechaCreacion).ToList(); ;
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
                Blogs blog = this.ObtenerSlug(slug);
                if (blog is null)
                    throw new Exception("No existe el blog");
                List<ComentarioDto> comentarios = context.Comentarios
                    .Where(w => w.Idblog == blog.Idblog).Select(s => new ComentarioDto
                    {
                        Comentario = s.Comentario,
                        Creador = s.Creador,
                        Fechacreacion = s.Fechacreaciion
                    }).ToList();
                return comentarios.OrderByDescending(o => o.Fechacreacion).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Guarda el comentario de post
        /// </summary>
        /// <param name="comentario"></param>
        public void GuardarComentario(Comentarios comentario)
        {
            try
            {
                context.Comentarios.Add(comentario);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
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
                List<BusquedaDto> busquedas = (from t0 in context.Blogs
                                               join t1 in context.BlogKey on t0.Idblog equals t1.Idblog
                                               join t2 in context.KeyWords on t1.Idkey equals t2.Idkey
                                               join t3 in context.Categorias on t0.Idcategoria equals t3.Idcategoria
                                               where t0.Titulo.Contains(busqueda) || t0.Titulo.StartsWith(busqueda)
                                               || t0.Titulo.EndsWith(busqueda) || t2.Nombre.Contains(busqueda)
                                               || t2.Nombre.StartsWith(busqueda) || t2.Nombre.EndsWith(busqueda)
                                               || t3.Nombre.Contains(busqueda) || t3.Nombre.StartsWith(busqueda)
                                               || t3.Nombre.EndsWith(busqueda)
                                               select new BusquedaDto
                                               {
                                                   Slug = t0.Slug,
                                                   Titulo = t0.Titulo
                                               }).ToList();

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
                List<CategoriasDto> categorias = (from t0 in context.Blogs
                                                  join t1 in context.Categorias on t0.Idcategoria equals t1.Idcategoria
                                                  where t0.Estado && t1.Estado
                                                  select new CategoriasDto
                                                  {
                                                      Cantidad = t0.Idblog,
                                                      Nombre = t1.Nombre
                                                  }).ToList();
                return categorias;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
