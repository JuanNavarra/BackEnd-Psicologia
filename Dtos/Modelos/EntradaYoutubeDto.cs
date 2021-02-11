namespace Dtos
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EntradaYoutubeDto
    {
        public List<KeyWordDto> KeyWords { get; set; }
        public string Titulo { get; set; }
        public string Slug { get; set; }
        public string Rutavideo { get; set; }
        public int Idcategoria { get; set; }
        public string Descripcion { get; set; }
        public string Creador { get; set; }
        public int IdVideo { get; set; }
        public bool Estado { get; set; }
        public DateTime Fechacreacion { get; set; }
        public int Idcreador { get; set; }
        public string Tipo { get; set; }
    }
}
