using System;
using System.Collections.Generic;

namespace Modelos
{
    public partial class KeyWords
    {
        public KeyWords()
        {
            BlogKey = new HashSet<BlogKey>();
        }

        public int Idkey { get; set; }
        public string Nombre { get; set; }
        public DateTime Fechacreacion { get; set; }
        public DateTime? Fechaactualizacion { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<BlogKey> BlogKey { get; set; }
    }
}
