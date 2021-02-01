namespace Servicio
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Serializable]
    public class NegocioExecption: Exception
    {
        #region Propiedades
        public int Codigo { get; set; }
        public string Error { get; set; }
        #endregion
        #region Constructores

        public NegocioExecption() { }

        public NegocioExecption(string message)
            : base(message) { }

        public NegocioExecption(string message, Exception inner)
            : base(message, inner) { }

        public NegocioExecption(string message, int Codigo)
            : this(message)
        {
            this.Codigo = Codigo;
        }
        #endregion
        #region Metodos y funciones

        #endregion
    }
}
