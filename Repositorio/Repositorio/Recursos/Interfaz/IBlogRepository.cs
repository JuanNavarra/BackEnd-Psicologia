namespace Repositorio
{
    using Dtos;
    using Modelos;
    using System.Collections.Generic;

    public interface IBlogRepository
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
        /// Verifica si el slug existe
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public Blogs ObtenerSlug(string slug);
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
        /// Guarda el comentario de post
        /// </summary>
        /// <param name="comentario"></param>
        public void GuardarComentario(Comentarios comentario);
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
    }
}
