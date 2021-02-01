namespace Repositorio
{
    using Dtos;
    using Modelos;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    public class UsuarioRepository : IUsuarioRepository
    {
        #region Propiedades
        private readonly PsicologiaContext contexto;
        #endregion
        #region Constructores
        public UsuarioRepository(PsicologiaContext context)
        {
            this.contexto = context;
        }
        #endregion
        #region Metodos y funciones

        /// <summary>
        /// Retorna true si existe el usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool Login(Usuarios usuario)
        {
            try
            {
                return contexto.Usuarios
                    .Where(w => w.Email == usuario.Email && w.Pass == usuario.Pass)
                    .Any();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
