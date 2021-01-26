using System;
using System.Collections.Generic;

namespace Modelos
{
    public partial class BlogKey
    {
        public int Idblogkey { get; set; }
        public int Idkey { get; set; }
        public int Idblog { get; set; }

        public virtual Blogs IdblogNavigation { get; set; }
        public virtual KeyWords IdkeyNavigation { get; set; }
    }
}
