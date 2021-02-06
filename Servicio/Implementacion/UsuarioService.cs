namespace Servicio
{
    using AutoMapper;
    using Dtos;
    using Microsoft.Extensions.Configuration;
    using Modelos;
    using Repositorio;
    using System;
    using System.IdentityModel.Tokens.Jwt;

    public class UsuarioService : IUsuarioService
    {
        #region Propiedades
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        #endregion
        #region Constructores
        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, IConfiguration configuration)
        {
            this.usuarioRepository = usuarioRepository;
            this.mapper = mapper;
            this.configuration = configuration;
        }
        #endregion
        #region Metodos y funciones
        /// <summary>
        /// Genera el token para logearse
        /// </summary>
        /// <param name="usuarioDto"></param>
        /// <returns></returns>
        public string Login(UsuarioDto usuarioDto)
        {
            try
            {
                Usuarios crendenciales = mapper.Map<Usuarios>(usuarioDto);
                crendenciales.Pass = Seguridad.Encrypt(crendenciales.Pass, configuration["ApiAuth:ClaveIV"]);
                bool usuario = this.usuarioRepository.Login(crendenciales);
                if (!usuario)
                {
                    throw new NegocioExecption("El usuario no existe", 401);
                }              

                string[] login = new string[]
                {
                    configuration["ApiAuth:Issuer"],
                    configuration["ApiAuth:Audience"],
                    configuration["ApiAuth:SecretKey"]
                };
                string response = new JwtSecurityTokenHandler().WriteToken(Seguridad.GenerarToken(usuarioDto, login));
                return response;
            }
            catch (NegocioExecption)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Metodo para combrabar si el nombre del usuario existe
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public UsuarioDto VerificarUsuario(string usuario)
        {
            try
            {
                Usuarios input = this.usuarioRepository.VerificarUsuario(usuario);
                UsuarioDto usuarioDto = mapper.Map<UsuarioDto>(input);
                return usuarioDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}