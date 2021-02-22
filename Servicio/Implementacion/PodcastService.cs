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
    using System.Text;
    using System.Transactions;

    public class PodcastService : IPodcastService
    {
        #region Propiedades
        private readonly IBlogService blogService;
        private readonly IBlogRepository repository;
        private readonly IUsuarioService usuarioService;
        private readonly IMapper mapper;
        #endregion
        #region Constructores
        public PodcastService(IBlogService blogService, IMapper mapper, IBlogRepository repository, IUsuarioService usuarioService)
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
        public List<PodcastDto> MostrarListadoEntradas(string entrada, bool estado)
        {
            try
            {
                List<BlogDto> blogDto = this.blogService.MostrarListadoEntradas(entrada, estado);
                List<PodcastDto> podcastDto = mapper.Map<List<PodcastDto>>(blogDto);
                return podcastDto;
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
        /// Elimina el podcast y sus relaciones
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ApiCallResult EliminarEntradaPodcast(string slug)
        {
            try
            {
                return blogService.EliminarEntradaPost(slug);
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
        /// Guarda el audio de un blog en el servidor
        /// </summary>
        /// <param name="formFile"></param>
        public string GuardarAudioServidor(IFormFile formFile)
        {
            try
            {
                return this.blogService.GuardarImagenServidor(formFile, "Podcast");
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
                return this.repository.BuscarImagenPorRuta(ruta).Idimagen;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo para guardar un podcast
        /// </summary>
        /// <param name="blogDetalleDto"></param>
        /// <returns></returns>
        public ApiCallResult GuardarPodcast(BlogDetalleDto blogDetalleDto)
        {
            try
            {
                Blogs slug = this.repository.ObtenerSlug(blogDetalleDto.Slug);
                if (slug != null)
                    throw new NegocioExecption($"Ya existe ha sido creado previamente un post con este SLUG {blogDetalleDto.Slug}", 202);
                BusquedaDto titulo = this.blogService.BuscarPost(blogDetalleDto.Titulo).FirstOrDefault();
                if (!string.IsNullOrEmpty(titulo.Slug))
                    throw new NegocioExecption($"Ya existe ha sido creado previamente un post con este Titulo {blogDetalleDto.Titulo}", 202);
                UsuarioDto usuario = this.usuarioService.VerificarUsuario(blogDetalleDto.Creador);
                if (usuario is null)
                    throw new NegocioExecption($"Error de logeo con {blogDetalleDto.Creador}", 401);
                using (TransactionScope scope = new TransactionScope())
                {
                    #region Guardar imagenes
                    this.GuardarAudioPodcast(blogDetalleDto.Rutaaudio);
                    #endregion
                    #region Guardar post
                    blogDetalleDto.Idcreador = usuario.Idusuario;
                    blogDetalleDto.Idimagen = this.BuscarImagenPorRuta(blogDetalleDto.Rutaaudio);
                    blogDetalleDto.FechaCreacion = DateTime.Now;
                    blogDetalleDto.Tipo = "PC";
                    blogDetalleDto.Estado = true;
                    Blogs blog = mapper.Map<Blogs>(blogDetalleDto);
                    this.repository.GuardarPost(blog);
                    #endregion

                    #region Guardar keywords
                    Blogs blogCreado = this.repository.ObtenerSlug(blogDetalleDto.Slug);
                    this.blogService.GuardarKeyWords(blogDetalleDto.KeyWords, blogCreado.Idblog);
                    #endregion
                    scope.Complete();
                }

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
        /// Guarda la ruta del podcast en la bd
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        private void GuardarAudioPodcast(string ruta)
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
                this.repository.GuardarImagenPost(imagenes);
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
        /// Muestra el podcast detalle
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public PodcastDto MostrarEntradaPodcast(string slug, bool estado)
        {
            try
            {
                Blogs blog = this.repository.ObtenerSlug(slug);
                if (blog is null)
                    throw new NegocioExecption("No existe el slug", 404);
                BlogDetalleDto blogDto = this.repository.MostrarEntradaPorSlug(slug, estado);
                PodcastDto podcastDto = mapper.Map<PodcastDto>(blogDto);
                return podcastDto;
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
        /// Actualiza un post
        /// </summary>
        /// <param name="blogDto"></param>
        /// <returns></returns>
        public ApiCallResult ActualizarEntradaPost(BlogDetalleDto blogDto)
        {
            try
            {
                Blogs blogs = this.repository.ObtenerSlug(blogDto.Slug);
                blogs.Titulo = blogDto.Titulo ?? blogs.Titulo;
                blogs.Descripcion = blogDto.Descripcion ?? blogs.Descripcion;
                blogs.Fechaactualizacion = DateTime.Now;
                blogs.Subtitulo = blogDto.SubTitulo ?? blogs.Subtitulo;
                if (blogDto.Rutaaudio != null)
                {
                    Imagenes audio = this.repository.BuscarMultimedia(blogs.Idimagen);
                    if (audio is null)
                        throw new NegocioExecption("Error al guardar el audio, contacta con el admin, " +
                            "ningun dato se actualizo", 500);
                    this.EliminarAudioervidor(audio.Ruta);
                    this.ActualizarAudioBaseDatos(blogDto.Rutaaudio, blogs.Idimagen);
                }
                if (blogDto.KeyWords != null)
                    this.ActualizarBlogKey(blogDto.KeyWords, blogs.Idblog);
                this.repository.ActualizarEntrada(blogs);

                return new ApiCallResult
                {
                    Estado = true,
                    Mensaje = "Post actualizado con extido"
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
        /// Elimina un archivo del servidor
        /// </summary>
        /// <param name="fileName"></param>
        private void EliminarAudioervidor(string fileName)
        {
            try
            {
                fileName = fileName[17..];
                string file = Path.Combine("Resources", "Audio", fileName);
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Actualiza un video de youtube por la base de datos
        /// </summary>
        /// <param name="rutaAudio"></param>
        /// <param name="idImagen"></param>
        private void ActualizarAudioBaseDatos(string rutaAudio, int? idImagen)
        {
            try
            {
                Imagenes imagen = this.repository.BuscarMultimedia(idImagen);
                if (imagen is null)
                {
                    throw new NegocioExecption("No se ha podido guardar bien el audio", 500);
                }

                imagen.Nombre = rutaAudio;
                imagen.Ruta = rutaAudio;

                this.repository.ActualizarMultimedia(imagen);
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
