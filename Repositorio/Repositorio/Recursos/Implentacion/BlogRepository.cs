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
                Blogs blog = this.context.Blogs.Where(w => w.Slug == slug).FirstOrDefault();
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
        /// <param name="estado"></param>
        /// <returns></returns>
        public BlogDetalleDto MostrarEntradaPorSlug(string slug, bool estado)
        {
            try
            {
                BlogDetalleDto blog = (from t0 in context.Blogs
                                       join t6 in context.Imagenes on t0.Idimagen equals t6.Idimagen
                                       join t3 in context.Categorias on t0.Idcategoria equals t3.Idcategoria
                                       join t4 in context.Usuarios on t0.Idcreador equals t4.Idusuario
                                       join t5 in context.Imagenes on t4.Idimagen equals t5.Idimagen
                                       where (estado ? t0.Estado : null == null) && t0.Slug == slug
                                       select new BlogDetalleDto
                                       {
                                           SubTitulo = t0.Subtitulo,
                                           Categoria = t3.Nombre,
                                           Creador = t4.Nombre + " " + t4.Apellido,
                                           Descripcion = t0.Descripcion,
                                           FechaCreacion = t0.Fechacreacion,
                                           IdVideo = t6.Nombre,
                                           ImagenCreador = t5.Ruta,
                                           ImagenPost = t6.Ruta,
                                           NombreImagen = t6.Nombre,
                                           IdBlog = t0.Idblog,
                                           Estado = t0.Estado,
                                           Titulo = t0.Titulo,
                                           Slug = t0.Slug,
                                           Tipo = t0.Tipo,
                                           Cita = t0.Cita,
                                           Idcategoria = t0.Idcategoria,
                                           AutorCita = t0.Autorcita,
                                           KeyWords = (from t7 in context.Blogs
                                                       join t1 in context.BlogKey on t0.Idblog equals t1.Idblog
                                                       join t2 in context.KeyWords on t1.Idkey equals t2.Idkey
                                                       where t7.Idblog == t0.Idblog
                                                       select new KeyWordDto
                                                       {
                                                           Id = t2.Idkey,
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
        /// <param name="entrada"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public List<BlogDto> MostrarListadoEntradas(string entrada, bool estado)
        {
            try
            {
                List<BlogDto> blogs = (from t0 in context.Blogs
                                       join t6 in context.Imagenes on t0.Idimagen equals t6.Idimagen
                                       join t3 in context.Categorias on t0.Idcategoria equals t3.Idcategoria
                                       join t4 in context.Usuarios on t0.Idcreador equals t4.Idusuario
                                       join t5 in context.Imagenes on t4.Idimagen equals t5.Idimagen
                                       where (estado ? t0.Estado : null == null)
                                       && (entrada == "" ? null == null : t0.Tipo.Equals(entrada))
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
                                           Estado = t0.Estado,
                                           NombreImagen = t6.Nombre,
                                           Titulo = t0.Titulo,
                                           Slug = t0.Slug,
                                           KeyWords = (from t7 in context.Blogs
                                                       join t1 in context.BlogKey on t0.Idblog equals t1.Idblog
                                                       join t2 in context.KeyWords on t1.Idkey equals t2.Idkey
                                                       where t0.Idblog == t7.Idblog
                                                       select new KeyWordDto
                                                       {
                                                           Id = t2.Idkey,
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
        /// <param name="page"></param>
        /// <returns></returns>
        public List<PostRecienteDto> ListarRecientes(string page)
        {
            try
            {
                List<PostRecienteDto> posts = (from t0 in context.Blogs
                                               join t1 in context.Imagenes on t0.Idimagen equals t1.Idimagen
                                               join t3 in context.Categorias on t0.Idcategoria equals t3.Idcategoria
                                               where t0.Estado &&
                                               (page == "PR" ? t0.Tipo == "PO" || t0.Tipo == "PC" : page != "YO" || t0.Tipo == "YO")
                                               select new PostRecienteDto
                                               {
                                                   FechaCreacion = t0.Fechacreacion,
                                                   Slug = t0.Slug,
                                                   Categoria = t3.Nombre,
                                                   Imagen = t1.Ruta,
                                                   Titulo = t0.Titulo,
                                                   Descripcion = t0.Descripcion.Length > 200 ? t0.Descripcion.Substring(0, 199) : t0.Descripcion,
                                                   Tipo = t0.Tipo,
                                                   IdBlog = t0.Idblog,
                                                   keyWords = (from t7 in context.Blogs
                                                               join t1 in context.BlogKey on t0.Idblog equals t1.Idblog
                                                               join t2 in context.KeyWords on t1.Idkey equals t2.Idkey
                                                               where t7.Idblog == t0.Idblog
                                                               select new KeyWordDto
                                                               {
                                                                   Id = t2.Idkey,
                                                                   Nombre = t2.Nombre
                                                               }).ToList()
                                               }).OrderByDescending(o => o.IdBlog).Take(5).ToList();
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
                                                      Id = t1.Idcategoria,
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

        /// <summary>
        /// Lista las palabras clave disponibles
        /// </summary>
        /// <returns></returns>
        public List<KeyWords> ListarKeyWords()
        {
            try
            {
                return context.KeyWords
                    .Where(w => w.Estado)
                    .OrderBy(o => o.Nombre)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// guarda un post
        /// </summary>
        /// <param name="blog"></param>
        public void GuardarPost(Blogs blog)
        {
            try
            {
                context.Blogs.Add(blog);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Guaerda en la m to m de blogkey
        /// </summary>
        /// <param name="keyWords"></param>
        public void GuardarKeyWords(List<BlogKey> blogKeys)
        {
            try
            {
                context.BlogKey.AddRange(blogKeys);
                context.SaveChanges();
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
        public void GuardarImagenPost(Imagenes imagen)
        {
            try
            {
                context.Imagenes.Add(imagen);
                context.SaveChanges();
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
        public Imagenes BuscarImagenPorRuta(string ruta)
        {
            try
            {
                return context.Imagenes.Where(w => w.Ruta == ruta).FirstOrDefault();
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
        public List<Categorias> ListarTodasCategorias()
        {
            try
            {
                return context.Categorias.Where(w => w.Estado).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista toda la tabla mtm de blog y key
        /// </summary>
        /// <param name="idBlog"></param>
        /// <returns></returns>
        public List<BlogKey> ListarBlogKeys(int idBlog)
        {
            try
            {
                return context.BlogKey.Where(w => w.Idblog == idBlog).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Elimina la relacion mtm de blog y keywords
        /// </summary>
        /// <param name="blogKeys"></param>
        public void ElimniarBlogKeys(List<BlogKey> blogKeys)
        {
            try
            {
                context.BlogKey.RemoveRange(blogKeys);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Elimina el contenido multimedia de una entrada
        /// </summary>
        /// <param name="media"></param>
        public void EliminarMultiMediaEntrada(Imagenes media)
        {
            try
            {
                context.Imagenes.Remove(media);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Busca el contenido multimedia la tabla imagenes
        /// </summary>
        /// <param name="idMedia"></param>
        /// <returns></returns>
        public Imagenes BuscarMultimedia(int? idMedia)
        {
            try
            {
                return context.Imagenes
                    .Where(w => w.Idimagen == idMedia)
                    .FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Elimina una entrada
        /// </summary>
        /// <param name="blog"></param>
        public void EliminarEntrada(Blogs blog)
        {
            try
            {
                context.Blogs.Remove(blog);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Actualiza la tabla de blogs
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        public void ActualizarEntrada(Blogs blog)
        {
            try
            {
                context.Blogs.Update(blog);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Actualiza la tabla imagenes
        /// </summary>
        /// <param name="multimedia"></param>
        public void ActualizarMultimedia(Imagenes multimedia)
        {
            try
            {
                context.Imagenes.Update(multimedia);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene una categoria
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public Categorias ObtenerCategoria(string categoria)
        {
            try
            {
                return this.context.Categorias.Where(w => w.Nombre.Trim() == categoria.Trim()).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Guarda una categoria
        /// </summary>
        /// <param name="categoria"></param>
        public void GuardarCategoria(Categorias categoria)
        {
            try
            {
                this.context.Add(categoria);
                this.context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene una keyWord
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public KeyWords ObtenerKeywords(string keyWord)
        {
            try
            {
                return this.context.KeyWords.Where(w => w.Nombre.Trim() == keyWord.Trim()).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Guarda una categoria
        /// </summary>
        /// <param name="keyWord"></param>
        public void GuardarKeyWords(KeyWords keyWord)
        {
            try
            {
                this.context.Add(keyWord);
                this.context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
