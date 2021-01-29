using System;
using System.Collections.Generic;

namespace Modelos
{
    public partial class Comentarios
    {
        public int Idcomentario { get; set; }
        public string Creador { get; set; }
        public DateTime Fechacreaciion { get; set; }
        public string Comentario { get; set; }
        public int Idblog { get; set; }
        public string Email { get; set; }
    }
}
