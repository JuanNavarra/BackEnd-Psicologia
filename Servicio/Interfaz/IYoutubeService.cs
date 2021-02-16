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
        /// <param name="estado"></param>
        /// <returns></returns>
        public List<YoutubeDto> MostrarListadoEntradas(string entrada, bool estado);
        /// <summary>
        /// Guarda las entradas con videos de youtube
        /// </summary>
        /// <param name="youtubeDto"></param>
        /// <returns></returns>
        public ApiCallResult GuardarEntradaYoutube(EntradaYoutubeDto youtubeDto);
        /// <summary>
        /// Elimina una entrada de youtube por el slug
        /// </summary>
        /// <param name="slug"></param>
        public ApiCallResult EliminarEntradaYoutube(string slug);
        /// <summary>
        /// Inhabilita o habilita una entrada de entrada
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ApiCallResult CambiarEstadoEntrada(string slug);
        /// <summary>
        /// Muestra el video detalle del video de youtube
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public YoutubeDto MostrarVideoYoutubePorSlug(string slug, bool estado);
        /// <summary>
        /// Actualiza la entrada de un video de youtube
        /// </summary>
        /// <param name="youtubeDto"></param>
        /// <returns></returns>
        public ApiCallResult ActualizarEntradaYoutube(YoutubeDto youtubeDto);
    }
}
