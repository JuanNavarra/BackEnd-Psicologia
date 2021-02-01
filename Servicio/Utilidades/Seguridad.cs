namespace Servicio
{
    using Dtos;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public static class Seguridad
    {

        #region Propiedades

        #endregion
        #region Metodos y funciones
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="apiAuth"></param>
        /// <returns></returns>
        public static JwtSecurityToken GenerarToken(UsuarioDto login, string[] apiAuth)
        {
            string ValidIssuer = apiAuth[0];
            string ValidAudience = apiAuth[1];
            SymmetricSecurityKey IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiAuth[2]));

            DateTime dtFechaExpiraToken;
            DateTime now = DateTime.Now;
            //La fecha de expiracion sera el mismo dia a las 12 de la noche
            dtFechaExpiraToken = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59, 999);

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Email, login.Email)
            };

            return new JwtSecurityToken
            (
                issuer: ValidIssuer,
                audience: ValidAudience,
                claims: claims,
                expires: dtFechaExpiraToken,
                notBefore: now,
                signingCredentials: new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }
        #endregion
    }
}
