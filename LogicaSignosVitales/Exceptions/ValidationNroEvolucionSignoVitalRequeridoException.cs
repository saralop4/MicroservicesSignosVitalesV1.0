using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaSignosVitales.Exceptions
{
    public class ValidationNroEvolucionSignoVitalRequeridoException : ValidationException
    {
        public ValidationNroEvolucionSignoVitalRequeridoException() { } 

        public ValidationNroEvolucionSignoVitalRequeridoException(string msg) : base(msg) { }   

    }
}
