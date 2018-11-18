using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    [Serializable()]
    class NoElementWithSuchKeyException : Exception
    {
        public string ErrorMessage;
        protected NoElementWithSuchKeyException() : base() { }

        public NoElementWithSuchKeyException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
