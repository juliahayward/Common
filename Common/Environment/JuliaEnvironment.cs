using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuliaHayward.Common.Environment
{
    public enum EnvironmentType
    {
        Undefined,
        Dev,
        Staging,
        Production,
        Test
    };


    public static class EnvironmentTypeExtensions
    {
        public static bool IsDebug(this EnvironmentType type)
        {
            return (type == EnvironmentType.Dev || type == EnvironmentType.Test);
        }
    }


    public class JuliaEnvironment
    {
        public static EnvironmentType CurrentEnvironment
        {
            get
            {
                var envType = System.Environment.GetEnvironmentVariable("Julia_Environment");
                if (envType != null)
                {
                    if (envType.Equals("production", StringComparison.InvariantCultureIgnoreCase))
                        return EnvironmentType.Production;
                    if (envType.Equals("staging", StringComparison.InvariantCultureIgnoreCase))
                        return EnvironmentType.Staging;
                    if (envType.Equals("dev", StringComparison.InvariantCultureIgnoreCase))
                        return EnvironmentType.Dev;
                    if (envType.Equals("test", StringComparison.InvariantCultureIgnoreCase))
                        return EnvironmentType.Test;

                    throw new EnvironmentNotSetException(string.Format("Julia_Environment system variable has unrecognised value {0}", envType));
                }

                return EnvironmentType.Production;
                // Change once AWS has been set up
                // throw new EnvironmentNotSetException("Julia_Environment system variable has not been set.");
            }
        }
    }
}
