using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumPi.Sequence;

namespace NumPi.Sequence.Extensions
{
    public static class Sequence
    {

        public static Sequence<int, ValT> OfValues<ValT>(IEnumerable<ValT> values)
        {
            var keys = values.Select((val, idx) => idx);
            return new Sequence<int, ValT>(keys, values);
        }
    }
}
