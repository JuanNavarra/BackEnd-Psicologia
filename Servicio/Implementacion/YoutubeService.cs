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
                List<BlogDto> blogDto = this.blogService.MostrarListadoEntradas(entrada,estado);
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
                this.blogService.GuardarKeyWords(youtubeDto.KeyWords, blogCreado);
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
        #endregion
    }
}
