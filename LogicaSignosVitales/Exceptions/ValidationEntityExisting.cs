using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaSignosVitales.Exceptions
{
    public class ValidationEntityExisting : Exception
    {

        public ValidationEntityExisting() { }   

        public ValidationEntityExisting(string message) : base(message) { }
    }
}
