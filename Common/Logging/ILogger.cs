using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JuliaHayward.Common.Logging
{
    public interface ILogger
    {
        void Error(string appName, string message, string detail);
        void Error(string appName, Exception exception);
    }
}
