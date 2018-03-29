using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Common
{
    public static class TypeConversionUtils
    {

        public enum ConversionType
        {
            Safe,
            Unsafe
        }
        public static T ConvertTo<T>(object value, ConversionType convType)
        {
            //TODO add more sophisticated type convertion logic
            return (T)System.Convert.ChangeType(value, typeof(T));
        }
    }
}
