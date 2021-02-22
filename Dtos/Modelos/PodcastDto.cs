namespace Dtos
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PodcastDto
    {
        public string Slug { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fechacreacion { get; set; }
        public string Tipo { get; set; }
        public bool Estado { get; set; }
        public string Categoria { get; set; }
        public string Rutaaudio { get; set; }
        public string ImagenCreador { get; set; }
        public string Creador { get; set; }
        public List<KeyWordDto> KeyWords { get; set; }
    }
}
