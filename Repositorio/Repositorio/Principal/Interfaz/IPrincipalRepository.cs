namespace Repositorio
{
    using Dtos;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IPrincipalRepository
    {
        /// <summary>
        /// Muestra el contenido de las faqs de la pagina
        /// </summary>
        /// <returns></returns>
        public FaqsDto MostrarFaq();
    }
}
