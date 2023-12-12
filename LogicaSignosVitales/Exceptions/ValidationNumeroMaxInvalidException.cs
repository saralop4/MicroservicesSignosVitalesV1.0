using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaSignosVitales.Exceptions
{
    public class ValidationNumeroMaxInvalidException : InvalidOperationException
    {

        public ValidationNumeroMaxInvalidException() { }    

        public ValidationNumeroMaxInvalidException(string msg) : base(msg) { }   
    }
}
