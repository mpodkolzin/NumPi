using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Vectors.Construction
{
    public interface IVectorConstruction
    {
    }


    public class Return : IVectorConstruction
    {
        public Int64 VectorLocation;
        public Return(Int64 VecLocation)
        {
            VectorLocation = VecLocation;
        }
    }

    public class Empty : IVectorConstruction
    {
        Int64 Size;
    }

    public class Recocate : IVectorConstruction
    {
        Lazy<Int64> Size;
        List<IVectorConstruction> vectorConstruction;

    }

    public class Combine : IVectorConstruction
    {
        public Int64 Size;
        public List<IVectorConstruction> VectorConstruction;

        public Combine(Int64 size, List<IVectorConstruction> vectorConstruction)
        {
            Size = size;
            VectorConstruction = vectorConstruction;
        }

    }

}
