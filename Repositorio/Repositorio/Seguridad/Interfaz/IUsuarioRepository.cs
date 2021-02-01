namespace Repositorio
{
    using Modelos;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IUsuarioRepository
    {
        /// <summary>
        /// Retorna true si existe el usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool Login(Usuarios usuario);
    }
}
