using System;
using System.Collections.Generic;

namespace Modelos
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Blogs = new HashSet<Blogs>();
        }

        public int Idusuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Descripcion { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public int? Idimagen { get; set; }

        public virtual Imagenes IdimagenNavigation { get; set; }
        public virtual ICollection<Blogs> Blogs { get; set; }
    }
}
