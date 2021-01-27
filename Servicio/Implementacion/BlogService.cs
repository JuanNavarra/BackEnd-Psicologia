namespace Servicio
{
    using AutoMapper;
    using Dtos;
    using Modelos;
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
        /// Obtiene una unica entrada dado un slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public BlogDetalleDto MostrarEntradaPorSlug(string slug)
        {
            try
            {
                Blogs blog = this.blogRepository.ObtenerSlug(slug);
                if (blog is null)
                    throw new Exception("No existe la entrada");
                BlogDetalleDto blogDto = this.blogRepository.MostrarEntradaPorSlug(slug);
                return blogDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

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

        /// <summary>
        /// Lista los 5 post mas recientes
        /// </summary>
        /// <returns></returns>
        public List<PostRecienteDto> ListarRecientes()
        {
            try
            {
                List<PostRecienteDto> posts = this.blogRepository.ListarRecientes();
                return posts;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
