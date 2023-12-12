using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaSignosVitales.Exceptions
{
    public class ValidationEstudioException : ArgumentException
    {
        public ValidationEstudioException()
        {
        }

        public ValidationEstudioException(string message)
            : base(message)
        {
        }

    }
}
