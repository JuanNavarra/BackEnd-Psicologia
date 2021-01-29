﻿namespace Psicologia
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult MostrarListadoEntradas(string slug)
        {
            try
            {
                BlogDetalleDto blogs = this.blogService.MostrarEntradaPorSlug(slug);
                return Json(blogs);
            }
            catch (Exception)
            {
                throw;
            }
        }

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

        /// <summary>
        /// Lista los ultimos 5 post mas recientes
        /// </summary>
        /// <returns></returns>
        [HttpGet("ultimos-post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ListarRecientes()
        {
            try
            {
                List<PostRecienteDto> posts = this.blogService.ListarRecientes();
                return Json(posts);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista todos los comentarios de un post especifico por orden de creacion
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("comentarios-post/{slug}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult MostrarComentarios(string slug)
        {
            try
            {
                List<ComentarioDto> comentarios = this.blogService.MostrarComentarios(slug);
                return Json(comentarios);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Guarda el comentario de un post en especifico
        /// </summary>
        /// <param name="comentarioDto"></param>
        /// <returns></returns>
        [HttpPost("guardar-comentario")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GuardarComentario(ComentarioSavedDto comentarioDto)
        {
            try
            {
                ApiCallResult result = this.blogService.GuardarComentario(comentarioDto);
                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
