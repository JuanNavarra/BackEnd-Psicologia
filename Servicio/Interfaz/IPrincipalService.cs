namespace Servicio
{
    using Dtos;
    using System.Collections.Generic;
    public interface IPrincipalService
    {
        /// <summary>
        /// Muestra el contenido de las faqs de la pagina
        /// </summary>
        /// <returns></returns>
        public FaqsDto MostrarFaq();
        /// <summary>
        /// Guarda el contenido de una seccion
        /// </summary>
        /// <param name="principalDto"></param>
        /// <returns></returns>
        public ApiCallResult GuardarContenido(PrincipalDto principalDto);
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
        /// Actualiza el contenido principal
        /// </summary>
        /// <param name="principalDto"></param>
        /// <returns></returns>
        public ApiCallResult ActualizarContenidoPrincipal(PrincipalDto principalDto);
    }
}
