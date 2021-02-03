namespace Servicio
{
    using Dtos;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IBlogService
    {
        /// <summary>
        /// Listado de todos los entradas disponibles ordenadas de fecha mas reciente
        /// </summary>
        /// <returns></returns>
        public List<BlogDto> MostrarListadoEntradas();
        /// <summary>
        /// Obtiene una unica entrada dado un slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public BlogDetalleDto MostrarEntradaPorSlug(string slug);
        /// <summary>
        /// Lista los 5 post mas recientes
        /// </summary>
        /// <returns></returns>
        public List<PostRecienteDto> ListarRecientes();
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
        /// <returns></returns>
        public List<BlogDto> ListarPostCategoria(string categoria);
        /// <summary>
        /// Lista las palabras clave disponibles
        /// </summary>
        /// <returns></returns>
        public List<KeyWordDto> ListarKeyWords();
    }
}
