namespace Repositorio
{
    using Dtos;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Modelos;
    public interface IPrincipalRepository
    {
        /// <summary>
        /// Muestra el contenido de las faqs de la pagina
        /// </summary>
        /// <returns></returns>
        public FaqsDto MostrarFaq();
        /// <summary>
        /// Guarda la seccion principal de la pagina
        /// </summary>
        /// <param name="principal"></param>
        public void GuardarSeccionPrincipal(Principal principal);
        /// <summary>
        /// Busca el contenido de la pagina principal por el id
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public Principal BuscarContenidoPrincipal(string descripcion);
        /// <summary>
        /// Lista el contendido principal
        /// </summary>
        /// <returns></returns>
        public List<PrincipalDto> ListarContenidoPrincipal();
        /// <summary>
        /// Muestra el contendio principal
        /// </summary>
        /// <returns></returns>
        public PrincipalDto MostrarContenidoPrincipal();
        /// <summary>
        /// Buscar principal por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Principal BuscarPorId(int id);
        /// <summary>
        /// Actualiza el principal
        /// </summary>
        /// <param name="principal"></param>
        public void ActualizarSeccion(Principal principal);
    }
}
