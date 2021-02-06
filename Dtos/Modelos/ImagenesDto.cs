namespace Dtos
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ImagenesDto
    {
        public int Idimagen { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public bool Estado { get; set; }
        public DateTime Fechacreacion { get; set; }
    }
}
