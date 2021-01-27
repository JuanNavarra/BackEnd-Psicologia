namespace Dtos
{
    using Modelos;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BlogDetalleDto
    {
        public string Slug { get; set; }
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Creador { get; set; }
        public string Categoria { get; set; }
        public string ImagenCreador { get; set; }
        public string ImagenPost { get; set; }
        public List<KeyWordDto> KeyWords { get; set; }
        public int IdBlog { get; set; }
        public string Cita { get; set; }
        public string AutorCita { get; set; }
    }
}
