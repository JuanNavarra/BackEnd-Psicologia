namespace Servicio
{
    using Dtos;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IBlogService
    {
        /// <summary>
        /// Listado de todos los entradas disponibles ordenadas de fecha mas reciente
        /// </summary>
        /// <returns></returns>
        public List<BlogDto> MostrarListadoEntradas();
    }
}
