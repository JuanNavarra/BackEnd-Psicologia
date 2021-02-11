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
    [Authorize]
    public class YoutubeController : Controller
    {
        #region Propiedades
        private readonly IYoutubeService service;
        #endregion
        #region Constructores
        public YoutubeController(IYoutubeService service)
        {
            this.service = service;
        }
        #endregion
        #region Metodos y funciones
        /// <summary>
        /// Listado de todos los entradas de youtube disponibles ordenadas de fecha mas reciente
        /// </summary>
        /// <param name="entrada"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        [HttpGet("youtube-psicologia/{estado}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult MostrarListadoEntradas(bool estado)
        {
            try
            {
                List<YoutubeDto> blogs = this.service.MostrarListadoEntradas("YO", estado);
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
        /// Guarda las entradas con videos de youtube
        /// </summary>
        /// <param name="youtubeDto"></param>
        /// <returns></returns>
        [HttpPost("guardar-entrada-youtube")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GuardarEntradaYoutube(EntradaYoutubeDto youtubeDto)
        {
            try
            {
                ApiCallResult result = this.service.GuardarEntradaYoutube(youtubeDto);
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
