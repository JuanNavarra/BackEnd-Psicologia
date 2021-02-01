namespace Psicologia
{
    using Dtos;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Servicio;
    using System;

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : Controller
    {
        #region Propiedades
        private readonly IUsuarioService usuarioService;
        #endregion
        #region Constructores
        public UsuarioController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }
        #endregion
        /// <summary>
        /// Retorna el token del usuario si se logeo correctamente
        /// </summary>
        /// <param name="usuarioDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login(UsuarioDto usuarioDto)
        {
            try
            {
                string token = this.usuarioService.Login(usuarioDto);
                return Ok(new
                {
                    response = StatusCode((int)System.Net.HttpStatusCode.OK, token),
                });
            }
            catch (NegocioExecption e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.Unauthorized, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
