using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaSignosVitales.Exceptions
{
    public class ValidationArgumentsEntityNullException : ArgumentNullException
    {

        public ValidationArgumentsEntityNullException() { }     

        public ValidationArgumentsEntityNullException(string message) : base(message) { }   
    }
}
