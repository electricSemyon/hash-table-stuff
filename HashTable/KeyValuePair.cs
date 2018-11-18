using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class KeyValuePair<KType, VType>
    {
        public KType Key { get; set; }
        public VType Value { get; set; }
        public bool WasDeleted = false;

        public KeyValuePair(KType key, VType value) {
            Key = key;
            Value = value;
        }
    }
}
