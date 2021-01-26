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
    [AllowAnonymous]
    public class BlogController : Controller
    {
        #region Propiedades
        private readonly IBlogService blogService;
        #endregion
        #region Constructores
        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }
        #endregion
        #region Metodos y funciones
        /// <summary>
        /// Listado de todos los entradas disponibles ordenadas de fecha mas reciente
        /// </summary>
        /// <returns></returns>
        [HttpGet("blogs-psicologia")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult MostrarListadoEntradas()
        {
            try
            {
                List<BlogDto> blogs = this.blogService.MostrarListadoEntradas();
                return Json(blogs);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
