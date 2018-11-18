using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class Optional<T>
    {
        T Value;
        bool HasValue;

        public Optional() { }

        public Optional(T value)
        {
            Value = value;
            HasValue = true;
        }

        public Optional<T> WithDefault(T value)
        {
            if (!HasValue)
            {
                Value = value;
                HasValue = true;
            }

            return this;
        }
    }
}
