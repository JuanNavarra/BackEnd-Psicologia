namespace Servicio
{
    using AutoMapper;
    using Dtos;
    using Microsoft.Extensions.Configuration;
    using Modelos;
    using Repositorio;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;

    public class UsuarioService : IUsuarioService
    {
        #region Propiedades
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration _configuration;
        #endregion
        #region Constructores
        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, IConfiguration configuration)
        {
            this.usuarioRepository = usuarioRepository;
            this.mapper = mapper;
            this._configuration = configuration;
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
                Usuarios input = mapper.Map<Usuarios>(usuarioDto);
                bool usuario = this.usuarioRepository.Login(input);
                if (usuario)
                {
                    throw new NegocioExecption("El usuario no existe", 401);
                }

                //Genera el token

                string[] login = new string[]
                {
                    _configuration["ApiAuth:Issuer"],
                    _configuration["ApiAuth:Audience"],
                    _configuration["ApiAuth:SecretKey"]
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
        #endregion
    }
}