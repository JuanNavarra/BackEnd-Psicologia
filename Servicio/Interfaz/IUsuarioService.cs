namespace Servicio
{
    using Dtos;

    public interface IUsuarioService
    {
        /// <summary>
        /// Genera el token para logearse
        /// </summary>
        /// <param name="usuarioDto"></param>
        /// <returns></returns>
        public string Login(UsuarioDto usuarioDto);

        /// <summary>
        /// Metodo para combrabar si el nombre del usuario existe
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public UsuarioDto VerificarUsuario(string usuario);
    }
}
