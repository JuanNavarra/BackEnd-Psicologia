namespace Psicologia
{
    using Dtos;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Servicio;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PodcastController : Controller
    {
        #region Propiedades
        private readonly IPodcastService podcastService;
        #endregion
        #region Constructores
        public PodcastController(IPodcastService podcastService)
        {
            this.podcastService = podcastService;
        }
        #endregion
        #region Metodos y funciones
        /// <summary>
        /// Listado de todos los entradas de youtube disponibles ordenadas de fecha mas reciente
        /// </summary>
        /// <param name="entrada"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        [HttpGet("podcast-psicologia/{estado}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult MostrarListadoEntradas(bool estado)
        {
            try
            {
                List<PodcastDto> blogs = this.podcastService.MostrarListadoEntradas("PC", estado);
                return Json(blogs);
            }
            catch (NegocioExecption e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Habilita o inhabilita una entrada de podcast por el slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpPut("cambiar-estado-entrada-podcast/{slug}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CambiarEstadoEntradaPodcast(string slug)
        {
            try
            {
                ApiCallResult result = this.podcastService.CambiarEstadoEntrada(slug);
                return StatusCode((int)System.Net.HttpStatusCode.OK, result);
            }
            catch (NegocioExecption e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Elimina una entrada de youtune por el slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpDelete("eliminar-entrada-podcast/{slug}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult EliminarEntradaYoutube(string slug)
        {
            try
            {
                ApiCallResult result = this.podcastService.EliminarEntradaPodcast(slug);
                return StatusCode((int)System.Net.HttpStatusCode.OK, result);
            }
            catch (NegocioExecption e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Metodo para guardar audio
        /// </summary>
        /// <returns></returns>
        [HttpPost("guardar-audio-podcast")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GuardarImagePost(IFormFile formFile)
        {
            try
            {
                formFile = Request.Form.Files[0];
                string ruta = this.podcastService.GuardarAudioServidor(formFile);
                return StatusCode((int)System.Net.HttpStatusCode.OK, ruta);
            }
            catch (NegocioExecption e)
            {
                return StatusCode(e.Codigo, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Metodo para guardar un podcast
        /// </summary>
        /// <param name="blogDto"></param>
        /// <returns></returns>
        [HttpPost("guardar-podcast")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GuardarPodcast(BlogDetalleDto blogDto)
        {
            try
            {
                ApiCallResult result = this.podcastService.GuardarPodcast(blogDto);
                return Ok(new
                {
                    response = result,
                });
            }
            catch (NegocioExecption e)
            {
                return StatusCode(e.Codigo, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Muestra el video detalle del video de youtube
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        [HttpGet("mostrar-entrada-podcast/{slug}/{estado}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult MostrarEntradaPodcast(string slug, bool estado)
        {
            try
            {
                PodcastDto result = this.podcastService.MostrarEntradaPodcast(slug, estado);
                return StatusCode((int)System.Net.HttpStatusCode.OK, result);
            }
            catch (NegocioExecption e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Actualizar una entrada de podcast
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        [HttpPut("actualizar-entrada-podcast")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarEntradaPost(BlogDetalleDto blogDto)
        {
            try
            {
                ApiCallResult result = this.podcastService.ActualizarEntradaPost(blogDto);
                return StatusCode((int)System.Net.HttpStatusCode.OK, result);
            }
            catch (NegocioExecption e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }
        #endregion
    }
}
