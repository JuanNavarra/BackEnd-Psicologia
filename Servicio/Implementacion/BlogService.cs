namespace Servicio
{
    using AutoMapper;
    using Dtos;
    using Repositorio;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BlogService : IBlogService
    {
        #region Propiedades
        private readonly IBlogRepository blogRepository;
        private readonly IMapper mapper;
        #endregion
        #region Constructores
        public BlogService(IBlogRepository blogRepository, IMapper mapper)
        {
            this.blogRepository = blogRepository;
            this.mapper = mapper;
        }
        #endregion
        #region Metodos y funciones

        /// <summary>
        /// Listado de todos los entradas disponibles ordenadas de fecha mas reciente
        /// </summary>
        /// <returns></returns>
        public List<BlogDto> MostrarListadoEntradas()
        {
            try
            {
                List<BlogDto> blogs = this.blogRepository.MostrarListadoEntradas();
                return blogs;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
