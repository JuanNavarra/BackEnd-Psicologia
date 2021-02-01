using System;
using System.Collections.Generic;

namespace Modelos
{
    public partial class FaqDetalle
    {
        public int Idfaqdetalle { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public bool Estado { get; set; }
        public DateTime Fechacreacion { get; set; }
        public DateTime? Fechaactualizacion { get; set; }
        public int Idfaq { get; set; }

        public virtual Faqs IdfaqNavigation { get; set; }
    }
}
