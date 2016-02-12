using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuliaHayward.Common.Environment
{
    public class EnvironmentNotSetException : Exception
    {
        public EnvironmentNotSetException(string message) : base(message) { }
    }
}
