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
    public class BlogController : Controller
    {
        #region Propiedades
        private readonly IBlogService blogService;
        private readonly IYoutubeService youtubeService;
        #endregion
        #region Constructores
        public BlogController(IBlogService blogService, IYoutubeService youtubeService)
        {
            this.blogService = blogService;
            this.youtubeService = youtubeService;
        }
        #endregion
        #region Metodos y funciones
        /// <summary>
        /// Obtiene una entrada unica dado un slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("blog-entrada/{slug}/{estado}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult MostrarListadoEntradasPorSlug(string slug, bool estado)
        {
            try
            {
                BlogDetalleDto blogs = this.blogService.MostrarEntradaPorSlug(slug, estado);
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
        /// <param name="estado"></param>
        /// <returns></returns>
        [HttpGet("blogs-psicologia/{estado}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult MostrarListadoEntradas(bool estado)
        {
            try
            {
                List<BlogDto> blogs = this.blogService.MostrarListadoEntradas("PO", estado);
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
        [AllowAnonymous]
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
        [HttpGet("comentarios-mostrar/{slug}")]
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        /// Lista todas las categorias
        /// </summary>
        /// <returns></returns>
        [HttpGet("listar-todas-categorias")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ListarTodasCategorias()
        {
            try
            {
                List<CategoriasDto> categoriasDto = this.blogService.ListarTodasCategorias();
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
        /// <param name="estado"></param>
        /// <returns></returns>
        [HttpGet("listar-detalle-categorias/{categoria}/{estado}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ListarPostCategoria(string categoria, bool estado)
        {
            try
            {
                List<BlogDto> entradas = this.blogService.ListarPostCategoria(categoria, estado);
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

        /// <summary>
        /// Lista las palabras clave disponibles
        /// </summary>
        /// <returns></returns>
        [HttpGet("key-words")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ListarKeyWords()
        {
            try
            {
                List<KeyWordDto> entradas = this.blogService.ListarKeyWords();
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

        /// <summary>
        /// Metodo para guardar un post
        /// </summary>
        /// <param name="blogDto"></param>
        /// <returns></returns>
        [HttpPost("guardar-post")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GuardarPost(BlogDetalleDto blogDto)
        {
            try
            {
                ApiCallResult result = this.blogService.GuardarPost(blogDto);
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
        /// Metodo para guardar un post
        /// </summary>
        /// <returns></returns>
        [HttpPost("guardar-image-post")]
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
                string ruta = this.blogService.GuardarImagenServidor(formFile, "Images");
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
        /// Habilita o inhabilita una entrada de un post por el slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpPut("cambiar-estado-entrada-post/{slug}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CambiarEstadoEntradaYoutube(string slug)
        {
            try
            {
                ApiCallResult result = this.youtubeService.CambiarEstadoEntrada(slug);
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
        /// Actualizar una entrada de post
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        [HttpPut("actualizar-entrada-post")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarEntradaPost(BlogDetalleDto blogDto)
        {
            try
            {
                ApiCallResult result = this.blogService.ActualizarEntradaPost(blogDto);
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
        /// Elimina un post y todas sus entradas
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        [HttpDelete("eliminar-entrada-post/{slug}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult EliminarEntradaPost(string slug)
        {
            try
            {
                ApiCallResult result = this.blogService.EliminarEntradaPost(slug);
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
        /// Guarda las categorias
        /// </summary>
        /// <returns></returns>
        [HttpPost("guardar-categoria")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GuardarCategorias(CategoriasDto categoria)
        {
            try
            {
                ApiCallResult result = this.blogService.GuardarCategoria(categoria.Nombre);
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
        /// Guarda las palabras clave
        /// </summary>
        /// <returns></returns>
        [HttpPost("guardar-keyWord")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GuardarKeyWords(KeyWordDto keyWordDto)
        {
            try
            {
                ApiCallResult result = this.blogService.GuardarKeyWord(keyWordDto.Nombre);
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
