using Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicio;
using System;
using System.Collections.Generic;

namespace Psicologia
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PrincipalController : Controller
    {
        #region Propiedades
        private readonly IPrincipalService principalService;
        #endregion
        #region Constructores
        public PrincipalController(IPrincipalService principalService)
        {
            this.principalService = principalService;
        }
        #endregion
        #region Metodos y funciones
        /// <summary>
        /// Muestra el contenido de las faqs de la pagina
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("faqs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult MostrarFaq()
        {
            try
            {
                FaqsDto faqs = this.principalService.MostrarFaq();
                return Json(faqs);
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
        /// Guarda el de la pagina del home
        /// </summary>
        /// <param name="comentarioDto"></param>
        /// <returns></returns>
        [HttpPost("guardar-contenido-principal")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GuardarContenido(PrincipalDto principalDto)
        {
            try
            {
                ApiCallResult result = this.principalService.GuardarContenido(principalDto);
                return Json(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Retorna el listado de la pagina principal
        /// </summary>
        /// <returns></returns>
        [HttpGet("listar-contenido-principal")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ListarContenidoPrincipal()
        {
            try
            {
                List<PrincipalDto> principalDtos = this.principalService.ListarContenidoPrincipal();
                return Json(principalDtos);
            }
            catch (Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Retorna el listado de la pagina principal
        /// </summary>
        /// <returns></returns>
        [HttpGet("mostrar-contenido-principal")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult MostrarContenidoPrincipal()
        {
            try
            {
                PrincipalDto principalDto = this.principalService.MostrarContenidoPrincipal();
                return Json(principalDto);
            }
            catch (Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Actualiza el contenido principal
        /// </summary>
        /// <returns></returns>
        [HttpPut("actualizar-contenido-principal")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarContenidoPrincipal(PrincipalDto principalDto)
        {
            try
            {
                ApiCallResult result = this.principalService.ActualizarContenidoPrincipal(principalDto);
                return Json(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }
        #endregion
    }
}
