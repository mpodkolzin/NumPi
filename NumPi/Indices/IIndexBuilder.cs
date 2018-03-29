using NumPi.Sequence.Construction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Indices
{
    public interface IIndexBuilder
    {
        IIndex<T> Create<T>(IEnumerable<T> from, bool ordered);
        IIndex<T> Create<T>(IReadOnlyCollection<T> from);
        IIndex<T> Merge<T>(IEnumerable<SequenceConstruction<T>> sequenceConstructions);

    }
}
