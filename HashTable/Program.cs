using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            KeyValueStorage<string, int> exampleHashTable = new KeyValueStorage<string, int>();

            Console.WriteLine("So, firstly, adding some elements.");
            exampleHashTable.Set("Kek", 1);
            exampleHashTable.Set("Kek", 2);


            Console.WriteLine("Then delete some.");
            exampleHashTable.Delete("Kek");

            exampleHashTable.Set("Kek", 1);
            exampleHashTable.Set("Kek1", 2);
            exampleHashTable.Set("Kek2", 1);
            exampleHashTable.Set("Kek3", 2);

            exampleHashTable.Delete("Kek1");
            exampleHashTable.Delete("Kek2");
            exampleHashTable.Delete("Kek3");

            Console.WriteLine("Trying to dangerously access the value by key.");
            Console.WriteLine(exampleHashTable.Get("Kek"));
            
            Console.WriteLine("Trying to access the value safely.");
            bool isKekqPresent = exampleHashTable.TryGetValue("Kekq");
            if (isKekqPresent)
                Console.WriteLine(exampleHashTable.Get("Kekq"));

            Console.WriteLine("Accessing a value by key and passing callback, that will be called, if element by this key is present.");
            exampleHashTable.PickElement("Kek", value => Console.WriteLine(value));

            Console.WriteLine("Returning optional type");
            Console.WriteLine($"exampleHashTable.PickElement(\"Kek1q\") == null == { exampleHashTable.PickElement("Kek1q") == null }");

            Console.ReadLine();
        }
    }
}
