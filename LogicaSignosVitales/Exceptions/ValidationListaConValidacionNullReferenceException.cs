using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaSignosVitales.Exceptions
{
    public class ValidationListaConValidacionNullReferenceException : NullReferenceException
    {

        public ValidationListaConValidacionNullReferenceException() { } 

        public ValidationListaConValidacionNullReferenceException(string message) : base(message) { }
    }
}
