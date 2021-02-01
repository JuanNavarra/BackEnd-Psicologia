namespace Dtos
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FaqsDto
    {
        public string Titulo { get; set; }
        public List<FaqsDetalleDto> Detalle { get; set; }
    }
}
