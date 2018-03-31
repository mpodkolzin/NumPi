using NumPi.Indices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Vectors.Construction
{
    public interface IVecConstructionCmd
    {
    }


    public class Return : IVecConstructionCmd
    {
        public Int64 VectorLocation;
        public Return(Int64 VecLocation)
        {
            VectorLocation = VecLocation;
        }
    }

    public class Empty : IVecConstructionCmd
    {
        Int64 Size;
        public Empty(Int64 size)
        {
            //TODO add error handling here
            Size = size;
        }
    }

    public class Recocate : IVecConstructionCmd
    {
        Lazy<Int64> Size;
        List<IVecConstructionCmd> vecConstructionCmd;

    }

    public class Combine : IVecConstructionCmd
    {
        public Int64 Size;
        public List<IVecConstructionCmd> VecConstructionCmds;

        public Combine(Int64 size, List<IVecConstructionCmd> vectorConstruction)
        {
            Size = size;
            VecConstructionCmds = vectorConstruction;
        }

    }

    public class GetRange : IVecConstructionCmd
    {
        public IVecConstructionCmd VecConstructionCmd;
        public IRangeBoundary<Int64> RangeBoundary;

        public GetRange(IVecConstructionCmd vecConstructionCmd, IRangeBoundary<Int64> rangeBoundary)
        {
            //TODO add error handling here
            VecConstructionCmd = vecConstructionCmd;
            RangeBoundary = rangeBoundary;

        }
    }

}
