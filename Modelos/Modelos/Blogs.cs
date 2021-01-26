using System;
using System.Collections.Generic;

namespace Modelos
{
    public partial class Blogs
    {
        public Blogs()
        {
            BlogKey = new HashSet<BlogKey>();
        }

        public int Idblog { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fechacreacion { get; set; }
        public DateTime? Fechaactualizacion { get; set; }
        public bool Estado { get; set; }
        public int? Idcreador { get; set; }
        public int? Idcategoria { get; set; }
        public int? Idimagen { get; set; }

        public virtual Categorias IdcategoriaNavigation { get; set; }
        public virtual Usuarios IdcreadorNavigation { get; set; }
        public virtual Imagenes IdimagenNavigation { get; set; }
        public virtual ICollection<BlogKey> BlogKey { get; set; }
    }
}
