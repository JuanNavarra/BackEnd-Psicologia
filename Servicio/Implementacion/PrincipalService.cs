namespace Servicio
{
    using Dtos;
    using Repositorio;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PrincipalService : IPrincipalService
    {
        #region Propiedades
        private readonly IPrincipalRepository principalRepository;
        #endregion
        #region Constructores
        public PrincipalService(IPrincipalRepository principalRepository)
        {
            this.principalRepository = principalRepository;
        }
        #endregion
        #region Metodos y funciones
        /// <summary>
        /// Muestra el contenido de las faqs de la pagina
        /// </summary>
        /// <returns></returns>
        public FaqsDto MostrarFaq()
        {
            try
            {
                FaqsDto faqsDto = principalRepository.MostrarFaq();
                return faqsDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
