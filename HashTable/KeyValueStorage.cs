using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class KeyValueStorage<KType, VType>
    {
        private const int INITIAL_ARRAY_LENGTH = 10;
        private const double ARRAY_SCALE_THREHOLD_COEFFICIENT = 1.3;

        private int ArrayLength;
        private KeyValuePair<KType, VType>[] KeysArray = new KeyValuePair<KType, VType>[] { };

        public delegate void ElementProcessor(VType value);

        public KeyValueStorage()
        {
            ArrayLength = INITIAL_ARRAY_LENGTH;
            KeysArray = new KeyValuePair<KType, VType>[ArrayLength];
        }

        private int GetKeyHash(KType key)
        {
            return Math.Abs(key.GetHashCode());
        }

        private int GetSecondaryKeyHash(KType key)
        {
            return Math.Abs(key.GetHashCode()) + 1;
        }

        public void Set(KType key, VType value)
        {
            int tempIndex = GetKeyHash(key) % ArrayLength;

            int elementWithSameKeyIndex = -1;
            int emptyIndex = -1;

            while (KeysArray[tempIndex] != null)
            {
                if (KeysArray[tempIndex].Key.Equals(key))
                {
                    elementWithSameKeyIndex = tempIndex;
                    break;
                }

                if (KeysArray[tempIndex].WasDeleted)
                    emptyIndex = tempIndex;

                tempIndex = (tempIndex + GetSecondaryKeyHash(key)) % ArrayLength;
            }

            if (emptyIndex == -1)
                emptyIndex = tempIndex;

            if (elementWithSameKeyIndex > -1)
                KeysArray[elementWithSameKeyIndex] = new KeyValuePair<KType, VType>(key, value);
            else
                KeysArray[emptyIndex] = new KeyValuePair<KType, VType>(key, value);

            if (ShouldArrayBeScaled())
                ScaleArray();
        }

        public VType Get(KType key)
        {
            int tempIndex = GetKeyHash(key) % ArrayLength;

            while (KeysArray[tempIndex] != null)
            {
                if (KeysArray[tempIndex].Key.Equals(key) && !KeysArray[tempIndex].WasDeleted)
                    return KeysArray[tempIndex].Value;

                tempIndex = (tempIndex + GetSecondaryKeyHash(key)) % ArrayLength;
            }

            throw new NoElementWithSuchKeyException("There's no element with such key");
        }

        public bool TryGetValue(KType key)
        {
            int tempIndex = GetKeyHash(key) % ArrayLength;

            while (KeysArray[tempIndex] != null)
            {
                if (KeysArray[tempIndex].Key.Equals(key) && !KeysArray[tempIndex].WasDeleted)
                    return true;

                tempIndex = (tempIndex + GetSecondaryKeyHash(key)) % ArrayLength;
            }
            
            return false;
        }

        public void PickElement(KType key, ElementProcessor process)
        {
            int tempIndex = GetKeyHash(key) % ArrayLength;

            while (KeysArray[tempIndex] != null)
            {
                if (KeysArray[tempIndex].Key.Equals(key) && !KeysArray[tempIndex].WasDeleted)
                    process(KeysArray[tempIndex].Value);

                tempIndex = (tempIndex + GetSecondaryKeyHash(key)) % ArrayLength;
            }
        }

        public Optional<VType> PickElement(KType key)
        {
            int tempIndex = GetKeyHash(key) % ArrayLength;

            while (KeysArray[tempIndex] != null)
            {
                if (KeysArray[tempIndex].Key.Equals(key) && !KeysArray[tempIndex].WasDeleted)
                    return new Optional<VType>(KeysArray[tempIndex].Value);

                tempIndex = (tempIndex + GetSecondaryKeyHash(key)) % ArrayLength;
            }

            return null;
        }

        public void Delete(KType key)
        {
            int tempIndex = GetKeyHash(key) % ArrayLength;

            while (KeysArray[tempIndex] != null)
            {
                if (KeysArray[tempIndex].Key.Equals(key))
                {
                    KeysArray[tempIndex].WasDeleted = true;
                    break;
                }

                tempIndex = (tempIndex + GetSecondaryKeyHash(key)) % ArrayLength;
            }
        }

        private bool ShouldArrayBeScaled()
        {
            return KeysArray.Length / KeysArray.Count(s => s != null) < ARRAY_SCALE_THREHOLD_COEFFICIENT;
        }

        private void ScaleArray()
        {
            IncreaseArrayLength();
            Array.Resize(ref KeysArray, ArrayLength);
        }

        private void IncreaseArrayLength()
        {
            ArrayLength = (int)(ArrayLength * ARRAY_SCALE_THREHOLD_COEFFICIENT);
        }
    }
}
