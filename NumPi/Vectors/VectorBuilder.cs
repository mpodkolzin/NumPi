using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Vectors
{
    public static class VectorBuilder
    {
        public static IVectorBuilder Instance = new ArrayVectorBuilder();
    }
}
