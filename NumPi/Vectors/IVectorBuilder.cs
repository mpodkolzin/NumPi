using NumPi.Vectors.Construction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Vectors
{
    public interface IVectorBuilder
    {
        IVector<T> Create<T>(T[] from);

        IVector<T> Build<T>(IVecConstructionCmd vectorConstruction, IVector<T>[] vectors);
        Task<IVector<T>> AsyncBuild<T>();

    }
}
