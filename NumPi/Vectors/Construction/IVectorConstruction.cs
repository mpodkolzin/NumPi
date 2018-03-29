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

        Int64 Size;
    }

    public class Empty : IVectorConstruction
    {
        Int64 Size;
    }

    public class Recocate : IVectorConstruction
    {

    }

}
