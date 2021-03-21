namespace Repositorio
{
    using Dtos;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    public class PrincipalRepository : IPrincipalRepository
    {
        #region Propiedades
        private readonly PsicologiaContext context;
        #endregion
        #region Constructores
        public PrincipalRepository(PsicologiaContext context)
        {
            this.context = context;
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
                FaqsDto faqs = context.Faqs.Where(w => w.Estado)
                    .Select(s => new FaqsDto
                    {
                        Titulo = s.Titulo,
                        Detalle = (context.FaqDetalle.Where(w => w.Idfaq == s.Idfaq && w.Estado)
                                    .Select(d => new FaqsDetalleDto
                                    {
                                        Contenido = d.Contenido,
                                        Titulo = d.Titulo
                                    })).ToList()
                    }).FirstOrDefault();
                return faqs;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
