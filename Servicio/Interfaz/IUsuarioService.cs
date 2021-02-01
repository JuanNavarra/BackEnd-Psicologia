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
    }
}
