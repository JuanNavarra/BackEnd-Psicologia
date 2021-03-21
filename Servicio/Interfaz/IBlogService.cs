namespace Servicio
{
    using Dtos;
    using Microsoft.AspNetCore.Http;
    using Modelos;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IBlogService
    {
        /// <summary>
        /// Listado de todos los entradas disponibles ordenadas de fecha mas reciente
        /// </summary>
        /// <param name="entrada"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public List<BlogDto> MostrarListadoEntradas(string entrada, bool estado);
        /// <summary>
        /// Obtiene una unica entrada dado un slug
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public BlogDetalleDto MostrarEntradaPorSlug(string slug, bool estado);
        /// <summary>
        /// Lista los 5 post mas recientes
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<PostRecienteDto> ListarRecientes(string page);
        /// <summary>
        /// Lista todos los comentarios de un post en especifico
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public List<ComentarioDto> MostrarComentarios(string slug);
        /// <summary>
        /// Guarda el comentario de un post en especifico
        /// </summary>
        /// <param name="comentarioDto"></param>
        /// <returns></returns>
        public ApiCallResult GuardarComentario(ComentarioSavedDto comentarioDto);
        /// <summary>
        /// Hace una busqueda de los posts con la coincidencia de busqueda
        /// </summary>
        /// <param name="busqueda"></param>
        /// <returns></returns>
        public List<BusquedaDto> BuscarPost(string busqueda);
        /// <summary>
        /// Lista todas las categorias con la catidad de post que tienen
        /// </summary>
        /// <returns></returns>
        public List<CategoriasDto> ListarCategorias();
        /// <summary>
        /// Lista los post que tiene una categoria especifica por ordern de creacion
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public List<BlogDto> ListarPostCategoria(string categoria, bool estado);
        /// <summary>
        /// Lista las palabras clave disponibles
        /// </summary>
        /// <returns></returns>
        public List<KeyWordDto> ListarKeyWords();
        /// <summary>
        /// Guarda un post
        /// </summary>
        /// <param name="blogDto"></param>
        /// <returns></returns>
        public ApiCallResult GuardarPost(BlogDetalleDto blogDto);
        /// <summary>
        /// Guarda la imagen de un blog
        /// </summary>
        /// <param name="formFile"></param>
        public string GuardarImagenServidor(IFormFile formFile, string folder);
        /// <summary>
        /// Lista todas las categorias
        /// </summary>
        /// <returns></returns>
        public List<CategoriasDto> ListarTodasCategorias();
        /// <summary>
        /// Guaerda en la m to m de blogkey
        /// </summary>
        /// <param name="blogKeys"></param>
        public void GuardarKeyWords(List<KeyWordDto> keyDto, int blogs);
        /// <summary>
        /// Actualiza un post
        /// </summary>
        /// <param name="blogDto"></param>
        /// <returns></returns>
        public ApiCallResult ActualizarEntradaPost(BlogDetalleDto blogDto);
        /// <summary>
        /// Elimina un post y todas sus relaciones
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ApiCallResult EliminarEntradaPost(string slug);
        /// <summary>
        /// Guarda una categoria
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public ApiCallResult GuardarCategoria(string categoria);
        /// <summary>
        /// Guardar palabra clave
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ApiCallResult GuardarKeyWord(string keyWord);
    }
}
