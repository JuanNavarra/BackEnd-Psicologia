namespace Dtos
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BlogDto
    {
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Creador { get; set; }
        public string Categoria { get; set; }
        public string Imagen { get; set; }
    }
}
