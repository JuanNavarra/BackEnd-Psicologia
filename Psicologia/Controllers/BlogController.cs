namespace Psicologia
{
    using Dtos;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Servicio;
    using System;
    using System.Collections.Generic;


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
        /// Obtiene una entrada unica dado un slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("blog-entrada/{slug}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult MostrarListadoEntradas(string slug)
        {
            try
            {
                BlogDetalleDto blogs = this.blogService.MostrarEntradaPorSlug(slug);
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
        /// Listado de todos los entradas disponibles ordenadas de fecha mas reciente
        /// </summary>
        /// <returns></returns>
        [HttpGet("blogs-psicologia")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult MostrarListadoEntradas()
        {
            try
            {
                List<BlogDto> blogs = this.blogService.MostrarListadoEntradas();
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
        /// Lista los ultimos 5 post mas recientes
        /// </summary>
        /// <returns></returns>
        [HttpGet("ultimos-post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ListarRecientes()
        {
            try
            {
                List<PostRecienteDto> posts = this.blogService.ListarRecientes();
                return Json(posts);
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
        /// Lista todos los comentarios de un post especifico por orden de creacion
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("comentarios-post/{slug}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult MostrarComentarios(string slug)
        {
            try
            {
                List<ComentarioDto> comentarios = this.blogService.MostrarComentarios(slug);
                return Json(comentarios);
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
        /// Guarda el comentario de un post en especifico
        /// </summary>
        /// <param name="comentarioDto"></param>
        /// <returns></returns>
        [HttpPost("guardar-comentario")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GuardarComentario(ComentarioSavedDto comentarioDto)
        {
            try
            {
                ApiCallResult result = this.blogService.GuardarComentario(comentarioDto);
                return Json(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Hace una busqueda de los posts con la coincidencia de busqueda
        /// </summary>
        /// <param name="busqueda"></param>
        /// <returns></returns>
        [HttpGet("buscar-post/{busqueda}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BuscarPost(string busqueda)
        {
            try
            {
                List<BusquedaDto> busquedaDto = this.blogService.BuscarPost(busqueda);
                return Json(busquedaDto);
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
        /// Lista todas las categorias con la catidad de post que tienen
        /// </summary>
        /// <returns></returns>
        [HttpGet("listar-categorias")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ListarCategorias()
        {
            try
            {
                List<CategoriasDto> categoriasDto = this.blogService.ListarCategorias();
                return Json(categoriasDto);
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
        /// Lista los post que tiene una categoria especifica por ordern de creacion
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        [HttpGet("listar-detalle-categorias/{categoria}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ListarCategorias(string categoria)
        {
            try
            {
                List<BlogDto> entradas = this.blogService.ListarPostCategoria(categoria);
                return Json(entradas);
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
