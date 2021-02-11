namespace Servicio
{
    using Dtos;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IYoutubeService
    {
        /// <summary>
        /// Listado de todos los entradas de youtube disponibles ordenadas de fecha mas reciente
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        public List<YoutubeDto> MostrarListadoEntradas(string entrada);
        /// <summary>
        /// Guarda las entradas con videos de youtube
        /// </summary>
        /// <param name="youtubeDto"></param>
        /// <returns></returns>
        public ApiCallResult GuardarEntradaYoutube(EntradaYoutubeDto youtubeDto);
    }
}
