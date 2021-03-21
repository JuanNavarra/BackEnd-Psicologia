namespace Repositorio
{
    using Dtos;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using Modelos;

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

        /// <summary>
        /// Guarda la seccion principal de la pagina
        /// </summary>
        /// <param name="principal"></param>
        public void GuardarSeccionPrincipal(Principal principal)
        {
            try
            {
                this.context.Add(principal);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Busca el contenido de la pagina principal por el id
        /// </summary>
        /// <param name="principalId"></param>
        /// <returns></returns>
        public Principal BuscarContenidoPrincipal(string descripcion)
        {
            try
            {
                Principal principal = this.context.Principal
                    .Where(w => w.Descripcion == descripcion)
                    .OrderByDescending(o => o.Idprincipal)
                    .FirstOrDefault();
                return principal;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista el contendido principal
        /// </summary>
        /// <returns></returns>
        public List<PrincipalDto> ListarContenidoPrincipal()
        {
            try
            {
                List<PrincipalDto> principalDtos = (from t0 in context.Principal
                                                    join t1 in context.Imagenes on t0.Idimagen equals t1.Idimagen
                                                    select new PrincipalDto
                                                    {
                                                        Descripcion = t0.Descripcion,
                                                        Estado = t0.Estado,
                                                        Id = t0.Idprincipal,
                                                        RutaImagen = t1.Ruta,
                                                        Texto = t1.Nombre
                                                    }).ToList();
                return principalDtos.OrderByDescending(o => o.Estado).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Muestra el contendio principal
        /// </summary>
        /// <returns></returns>
        public PrincipalDto MostrarContenidoPrincipal()
        {
            try
            {
                return (from t0 in context.Principal
                        join t1 in context.Imagenes on t0.Idimagen equals t1.Idimagen
                        where t0.Estado
                        select new PrincipalDto
                        {
                            Descripcion = t0.Descripcion,
                            Estado = t0.Estado,
                            Id = t0.Idprincipal,
                            RutaImagen = t1.Ruta,
                            Texto = t1.Nombre
                        }).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Buscar principal por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Principal BuscarPorId(int id)
        {
            try
            {
                return this.context.Principal.Where(w => w.Idprincipal == id).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Actualiza el principal
        /// </summary>
        /// <param name="principal"></param>
        public void ActualizarSeccion(Principal principal)
        {
            try
            {
                this.context.Principal.Update(principal);
                this.context.SaveChanges();
            }
            catch (Exception eS)
            {
                throw;
            }
        }
        #endregion
    }
}
