using NumPi.Vectors.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumPi.Vectors.Helpers
{
    //helper class for initial vector building
    public interface IElasticVector<T>
    {
        void Add(T value);
        IVector ToVector();
        
    }

    public class ListVector<T> : IElasticVector<T>
    {
        private const int DEFAULT_CAPACITY = 500;

        private List<T> _data;

        public ListVector()
        {
            _data = new List<T>(DEFAULT_CAPACITY);
        }
        public void Add(T value)
        {
            //TODO add error handling here
            _data.Add(value);
        }

        public IVector ToVector()
        {
            return Vector.ofValues(_data.ToArray());
        }
    }
}
