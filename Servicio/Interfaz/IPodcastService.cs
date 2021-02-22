namespace Servicio
{
    using Dtos;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IPodcastService
    {
        /// <summary>
        /// Listado de todos los entradas de youtube disponibles ordenadas de fecha mas reciente
        /// </summary>
        /// <param name="entrada"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public List<PodcastDto> MostrarListadoEntradas(string entrada, bool estado);
        /// <summary>
        /// Inhabilita o habilita una entrada de entrada
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ApiCallResult CambiarEstadoEntrada(string slug);
        /// <summary>
        /// Elimina el podcast y sus relaciones
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public ApiCallResult EliminarEntradaPodcast(string slug);
        /// <summary>
        /// Guarda el audio de un blog en el servidor
        /// </summary>
        /// <param name="formFile"></param>
        public string GuardarAudioServidor(IFormFile formFile);
        /// <summary>
        /// Metodo para guardar un podcast
        /// </summary>
        /// <param name="blogDetalleDto"></param>
        /// <returns></returns>
        public ApiCallResult GuardarPodcast(BlogDetalleDto blogDetalleDto);
        /// <summary>
        /// Muestra el podcast detalle
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public PodcastDto MostrarEntradaPodcast(string slug, bool estado);
        /// <summary>
        /// Actualiza un post
        /// </summary>
        /// <param name="blogDto"></param>
        /// <returns></returns>
        public ApiCallResult ActualizarEntradaPost(BlogDetalleDto blogDto);
    }
}
