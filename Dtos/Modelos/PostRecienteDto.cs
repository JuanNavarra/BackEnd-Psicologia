﻿namespace Dtos
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PostRecienteDto
    {
        public string Slug { get; set; }
        public string Titulo { get; set; }
        public string Imagen { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdBlog { get; set; }
        public List<KeyWordDto> keyWords { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
    }
}
