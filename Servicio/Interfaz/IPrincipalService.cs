namespace Servicio
{
    using Dtos;

    public interface IPrincipalService
    {
        /// <summary>
        /// Muestra el contenido de las faqs de la pagina
        /// </summary>
        /// <returns></returns>
        public FaqsDto MostrarFaq();
    }
}
