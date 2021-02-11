namespace Dtos
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class YoutubeDto
    {
        public string Slug { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Creador { get; set; }
        public string Categoria { get; set; }
        public string ImagenCreador { get; set; }
        public string IdVideo { get; set; }
        public string RutaVideo { get; set; }
        public List<KeyWordDto> KeyWords { get; set; }
        public int IdBlog { get; set; }
    }
}
