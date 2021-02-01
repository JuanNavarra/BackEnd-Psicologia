using Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult MostrarFaq()
        {
            try
            {
                FaqsDto faqs = this.principalService.MostrarFaq();
                return Json(faqs);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
