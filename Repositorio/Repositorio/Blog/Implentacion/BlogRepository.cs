namespace Repositorio
{
    using Dtos;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    public class BlogRepository : IBlogRepository
    {
        #region Propiedades
        private readonly PsicologiaContext context;
        #endregion
        #region Constructores
        public BlogRepository(PsicologiaContext context)
        {
            this.context = context;
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
                List<BlogDto> blogs = (from t0 in context.Blogs
                                       join t1 in context.BlogKey on t0.Idblog equals t1.Idblog
                                       join t2 in context.KeyWords on t1.Idkey equals t2.Idkey
                                       join t3 in context.Categorias on t0.Idcategoria equals t3.Idcategoria
                                       join t4 in context.Usuarios on t0.Idcreador equals t4.Idusuario
                                       join t5 in context.Imagenes on t4.Idimagen equals t5.Idimagen
                                       where t0.Estado
                                       select new BlogDto
                                       {
                                           SubTitulo = t0.Subtitulo,
                                           Categoria = t3.Nombre,
                                           Creador = t4.Nombre + t4.Apellido,
                                           Descripcion = t0.Descripcion,
                                           FechaCreacion = t0.Fechacreacion,
                                           Imagen = t5.Ruta,
                                           Titulo = t0.Titulo
                                       }).ToList();
                return blogs.OrderByDescending(o => o.FechaCreacion).ToList();
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion
    }
}
