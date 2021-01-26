namespace Repositorio
{
    using Dtos;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IBlogRepository
    {
        /// <summary>
        /// Listado de todos los entradas disponibles ordenadas de fecha mas reciente
        /// </summary>
        /// <returns></returns>
        public List<BlogDto> MostrarListadoEntradas();
    }
}
