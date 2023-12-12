using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaSignosVitales.Exceptions
{
    public class ValidationCodigoEnfermeraExcepcion : Exception
    {
        public ValidationCodigoEnfermeraExcepcion() { } 

        public ValidationCodigoEnfermeraExcepcion(string message) : base(message) { }
    }
}
