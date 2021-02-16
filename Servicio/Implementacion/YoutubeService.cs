namespace Servicio
{
    using AutoMapper;
    using Dtos;
    using Modelos;
    using Repositorio;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Transactions;

    public class YoutubeService : IYoutubeService
    {
        #region Propiedades
        private readonly IBlogService blogService;
        private readonly IBlogRepository repository;
        private readonly IUsuarioService usuarioService;
        private readonly IMapper mapper;
        #endregion
        #region Constructores
        public YoutubeService(IBlogService blogService, IMapper mapper, IBlogRepository repository, IUsuarioService usuarioService)
        {
            this.blogService = blogService;
            this.mapper = mapper;
            this.repository = repository;
            this.usuarioService = usuarioService;
        }
        #endregion
        #region Metodos y funciones
        /// <summary>
        /// Listado de todos los entradas de youtube disponibles ordenadas de fecha mas reciente
        /// </summary>
        /// <param name="entrada"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public List<YoutubeDto> MostrarListadoEntradas(string entrada, bool estado)
        {
            try
            {
                List<BlogDto> blogDto = this.blogService.MostrarListadoEntradas(entrada, estado);
                List<YoutubeDto> youtubeDto = mapper.Map<List<YoutubeDto>>(blogDto);
                return youtubeDto;
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
        /// Guarda la ruta de un video en youtube
        /// </summary>
        /// <param name="rutaYoutube"></param>
        private int GuardarRutaYoutube(string rutaYoutube)
        {
            try
            {
                string videoId = "";
                string[] rutaArray = rutaYoutube.Split("watch?v=");
                if (rutaArray != null)
                {
                    if (rutaArray.Length >= 1)
                    {
                        rutaArray = rutaArray[1].Split('&');
                        videoId = rutaArray[0];
                    }
                    else
                        throw new NegocioExecption("Ha ocurrido un error en el servidor al guardar el video", 500);
                }
                else
                    throw new NegocioExecption("Haz copiado mal la url del video, revisala bien", 500);

                ImagenesDto videosDto = new ImagenesDto()
                {
                    Estado = true,
                    Fechacreacion = DateTime.Now,
                    Nombre = videoId,
                    Ruta = rutaYoutube
                };
                Imagenes video = mapper.Map<Imagenes>(videosDto);
                this.repository.GuardarImagenPost(video);

                return video.Idimagen;
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
        /// Guarda las entradas con videos de youtube
        /// </summary>
        /// <param name="youtubeDto"></param>
        /// <returns></returns>
        public ApiCallResult GuardarEntradaYoutube(EntradaYoutubeDto youtubeDto)
        {
            try
            {
                #region Validaciones
                Blogs slug = this.repository.ObtenerSlug(youtubeDto.Slug);
                if (slug != null)
                    throw new NegocioExecption($"Ya existe ha sido creado previamente un post con este SLUG {youtubeDto.Slug}", 202);
                BusquedaDto titulo = this.blogService.BuscarPost(youtubeDto.Titulo).FirstOrDefault();
                if (!string.IsNullOrEmpty(titulo.Slug))
                    throw new NegocioExecption($"Ya existe ha sido creado previamente un post con este Titulo {youtubeDto.Titulo}", 202);
                UsuarioDto usuario = this.usuarioService.VerificarUsuario(youtubeDto.Creador);
                if (usuario is null)
                    throw new NegocioExecption($"Error de logeo con {youtubeDto.Creador}", 401);
                #endregion

                #region Guardar Entrada
                youtubeDto.IdVideo = this.GuardarRutaYoutube(youtubeDto.Rutavideo);
                youtubeDto.Tipo = "YO";
                youtubeDto.Estado = true;
                youtubeDto.Fechacreacion = DateTime.Now;
                youtubeDto.Idcreador = usuario.Idusuario;
                Blogs blog = mapper.Map<Blogs>(youtubeDto);
                this.repository.GuardarPost(blog);
                #endregion

                #region Guardar Keywords
                Blogs blogCreado = this.repository.ObtenerSlug(youtubeDto.Slug);
                this.blogService.GuardarKeyWords(youtubeDto.KeyWords, blogCreado.Idblog);
                #endregion
                return new ApiCallResult
                {
                    Estado = true,
                    Mensaje = "Video guardado correctamente"
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
        /// Elimina la relacion mtm de blog y keywords
        /// </summary>
        /// <param name="idBlog"></param>
        private void EliminarBlogKey(int idBlog)
        {
            try
            {
                List<BlogKey> blogKeys = this.repository.ListarBlogKeys(idBlog);
                if (blogKeys.Count > 0)
                    this.repository.ElimniarBlogKeys(blogKeys);
                else
                    throw new NegocioExecption("No se encontro palabras clave para este post", 500);
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
        /// Elimina el contenido multimedia de una entrada
        /// </summary>
        /// <param name="idMedia"></param>
        private void EliminarMultiMediaEntrada(int? idMedia)
        {
            try
            {
                Imagenes video = this.repository.BuscarMultimedia(idMedia);
                if (video != null)
                    this.repository.EliminarMultiMediaEntrada(video);
                else
                    throw new NegocioExecption("No se encontro contenido multimedia para este post", 500);
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
        /// Elimina una entrada de youtube por el slug
        /// </summary>
        /// <param name="slug"></param>
        public ApiCallResult EliminarEntradaYoutube(string slug)
        {
            try
            {
                #region Validaciones
                Blogs blog = this.repository.ObtenerSlug(slug);
                if (blog is null)
                    throw new NegocioExecption($"No existe ninguna entrada con este SLUG {slug}", 202);
                #endregion

                using (TransactionScope scope = new TransactionScope())
                {
                    #region Eliminar blogkey
                    this.EliminarBlogKey(blog.Idblog);
                    this.repository.EliminarEntrada(blog);
                    this.EliminarMultiMediaEntrada(blog.Idimagen);
                    scope.Complete();
                    #endregion
                }
                return new ApiCallResult
                {
                    Estado = true,
                    Mensaje = "Video de youtube eliminado con exito"
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
        /// Inhabilita o habilita una entrada de entrada
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ApiCallResult CambiarEstadoEntrada(string slug)
        {
            try
            {
                #region Validaciones
                Blogs blog = this.repository.ObtenerSlug(slug);
                if (blog is null)
                    throw new NegocioExecption($"No existe ninguna entrada con este SLUG {slug}", 202);
                #endregion
                blog.Estado = !blog.Estado;
                this.repository.ActualizarEntrada(blog);
                return new ApiCallResult
                {
                    Estado = true,
                    Mensaje = "Video inhabilitado exitosamente"
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
        /// Muestra el video detalle del video de youtube
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public YoutubeDto MostrarVideoYoutubePorSlug(string slug, bool estado)
        {
            try
            {
                Blogs blog = this.repository.ObtenerSlug(slug);
                if (blog is null)
                    throw new NegocioExecption("No existe el slug", 404);
                BlogDetalleDto blogDto = this.repository.MostrarEntradaPorSlug(slug, estado);
                YoutubeDto youtubeDto = mapper.Map<YoutubeDto>(blogDto);
                return youtubeDto;
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
        /// Actualiza la entrada de un video de youtube
        /// </summary>
        /// <param name="youtubeDto"></param>
        /// <returns></returns>
        public ApiCallResult ActualizarEntradaYoutube(YoutubeDto youtubeDto)
        {
            try
            {
                Blogs blogs = this.repository.ObtenerSlug(youtubeDto.Slug);
                blogs.Titulo = youtubeDto.Titulo ?? blogs.Titulo;
                blogs.Descripcion = youtubeDto.Descripcion ?? blogs.Descripcion;
                blogs.Idcategoria = youtubeDto.Idcategoria ?? blogs.Idcategoria;
                blogs.Fechaactualizacion = DateTime.Now;
                if (youtubeDto.RutaVideo != null)
                    ActualizarVideo(youtubeDto.RutaVideo, blogs.Idimagen);
                if (youtubeDto.KeyWords != null)
                    this.ActualizarBlogKey(youtubeDto.KeyWords, blogs.Idblog);
                this.repository.ActualizarEntrada(blogs);

                return new ApiCallResult
                {
                    Estado = true,
                    Mensaje = "Video actualizado con extido"
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
        /// Actualiza la entrada mtm de blogs
        /// </summary>
        /// <param name="keyWordDtos"></param>
        /// <param name="idBlog"></param>
        private void ActualizarBlogKey(List<KeyWordDto> keyWordDtos, int idBlog)
        {
            try
            {
                List<BlogKey> blogKeys = this.repository.ListarBlogKeys(idBlog);
                if (blogKeys is null)
                    throw new NegocioExecption("No existen keys para actualizar", 500);
                this.repository.ElimniarBlogKeys(blogKeys);
                this.blogService.GuardarKeyWords(keyWordDtos, idBlog);
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
        /// Actualiza un video de youtube
        /// </summary>
        /// <param name="rutaYoutube"></param>
        /// <param name="idImagen"></param>
        private void ActualizarVideo(string rutaYoutube, int? idImagen)
        {
            try
            {
                Imagenes video = this.repository.BuscarMultimedia(idImagen);
                string videoId = "";
                string[] rutaArray = rutaYoutube.Split("watch?v=");
                if (rutaArray != null)
                {
                    if (rutaArray.Length >= 1)
                    {
                        rutaArray = rutaArray[1].Split('&');
                        videoId = rutaArray[0];
                    }
                    else
                        throw new NegocioExecption("Ha ocurrido un error en el servidor al guardar el video", 500);
                }
                else
                    throw new NegocioExecption("Haz copiado mal la url del video, revisala bien", 500);

                video.Nombre = videoId;
                video.Ruta = rutaYoutube;

                this.repository.ActualizarMultimedia(video);
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
        #endregion
    }
}
