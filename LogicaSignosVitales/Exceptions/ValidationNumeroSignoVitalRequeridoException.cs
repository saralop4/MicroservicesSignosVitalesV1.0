using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaSignosVitales.Exceptions
{
    public class ValidationNumeroSignoVitalRequeridoException : ValidationException
    {
        public ValidationNumeroSignoVitalRequeridoException() { } 

        public ValidationNumeroSignoVitalRequeridoException(string msg) : base(msg) { }   

    }
}
