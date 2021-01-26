using System;
using System.Collections.Generic;

namespace Modelos
{
    public partial class Categorias
    {
        public Categorias()
        {
            Blogs = new HashSet<Blogs>();
        }

        public int Idcategoria { get; set; }
        public string Nombre { get; set; }
        public DateTime Fechacreacion { get; set; }
        public DateTime? Fechaactualizacion { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<Blogs> Blogs { get; set; }
    }
}
