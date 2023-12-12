using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaSignosVitales.Exceptions
{
    public class ValidationPabellonNullException : NullReferenceException
    {

        public ValidationPabellonNullException () { }   

        public ValidationPabellonNullException(string message) : base(message) { }  
    }
}
