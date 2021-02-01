using System;
using System.Collections.Generic;

namespace Modelos
{
    public partial class Faqs
    {
        public Faqs()
        {
            FaqDetalle = new HashSet<FaqDetalle>();
        }

        public int Idfaq { get; set; }
        public string Titulo { get; set; }
        public bool Estado { get; set; }
        public DateTime Fechacreacion { get; set; }
        public DateTime? Fechaactualizacion { get; set; }

        public virtual ICollection<FaqDetalle> FaqDetalle { get; set; }
    }
}
