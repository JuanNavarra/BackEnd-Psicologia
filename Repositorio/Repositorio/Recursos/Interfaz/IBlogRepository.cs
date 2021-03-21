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
        /// Verifica si el slug existe
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public Blogs ObtenerSlug(string slug);
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
        /// <summary>
        /// Lista las palabras clave disponibles
        /// </summary>
        /// <returns></returns>
        public List<KeyWords> ListarKeyWords();
        /// <summary>
        /// guarda un post
        /// </summary>
        /// <param name="blog"></param>
        public void GuardarPost(Blogs blog);
        /// <summary>
        /// Guaerda en la m to m de blogkey
        /// </summary>
        /// <param name="keyWords"></param>
        public void GuardarKeyWords(List<BlogKey> blogKeys);
        /// <summary>
        /// Guarda la ruta de la imagen en la bd
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        public void GuardarImagenPost(Imagenes imagen);
        /// <summary>
        /// Busca el id de una imagen por la ruta de ella
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        public Imagenes BuscarImagenPorRuta(string ruta);
        /// <summary>
        /// Lista todas las categorias
        /// </summary>
        /// <returns></returns>
        public List<Categorias> ListarTodasCategorias();
        /// <summary>
        /// Lista toda la tabla mtm de blog y key
        /// </summary>
        /// <param name="idBlog"></param>
        /// <returns></returns>
        public List<BlogKey> ListarBlogKeys(int idBlog);
        /// <summary>
        /// Elimina la relacion mtm de blog y keywords
        /// </summary>
        /// <param name="blogKeys"></param>
        public void ElimniarBlogKeys(List<BlogKey> blogKeys);
        /// <summary>
        /// Elimina el contenido multimedia de una entrada
        /// </summary>
        /// <param name="media"></param>
        public void EliminarMultiMediaEntrada(Imagenes media);
        /// <summary>
        /// Busca el contenido multimedia la tabla imagenes
        /// </summary>
        /// <param name="idMedia"></param>
        /// <returns></returns>
        public Imagenes BuscarMultimedia(int? idMedia);
        /// <summary>
        /// Elimina una entrada
        /// </summary>
        /// <param name="blog"></param>
        public void EliminarEntrada(Blogs blog);
        /// <summary>
        /// Actualiza la tabla de blogs
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        public void ActualizarEntrada(Blogs blog);
        /// <summary>
        /// Actualiza la tabla imagenes
        /// </summary>
        /// <param name="multimedia"></param>
        public void ActualizarMultimedia(Imagenes multimedia);
        /// <summary>
        /// Obtiene una categoria
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public Categorias ObtenerCategoria(string categoria);
        /// <summary>
        /// Guarda una categoria
        /// </summary>
        /// <param name="categoria"></param>
        public void GuardarCategoria(Categorias categoria);
        /// <summary>
        /// Obtiene una keyWord
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public KeyWords ObtenerKeywords(string keyWord);
        /// <summary>
        /// Guarda una categoria
        /// </summary>
        /// <param name="keyWord"></param>
        public void GuardarKeyWords(KeyWords keyWord);
    }
}
