namespace Dtos
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PrincipalDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Texto { get; set; }
        public string RutaImagen { get; set; }
        public bool Estado { get; set; }
    }
}
