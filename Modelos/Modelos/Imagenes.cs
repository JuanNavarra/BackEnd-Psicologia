using System;
using System.Collections.Generic;

namespace Modelos
{
    public partial class Imagenes
    {
        public Imagenes()
        {
            Blogs = new HashSet<Blogs>();
            Usuarios = new HashSet<Usuarios>();
        }

        public int Idimagen { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public bool Estado { get; set; }
        public DateTime Fechacreacion { get; set; }

        public virtual ICollection<Blogs> Blogs { get; set; }
        public virtual ICollection<Usuarios> Usuarios { get; set; }
    }
}
