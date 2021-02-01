using Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicio;
using System;

namespace Psicologia
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
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
        #endregion
    }
}
