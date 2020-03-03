using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection

    // Алексей Петриленков

   //2. Дана коллекция List<T>, требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:
   //а) для целых чисел;
   //б) * для обобщенной коллекции;
   //в) * используя Linq.
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Здравствуйте! Это программа для подсчёта элементов в коллекции!");

            MyList<int>.list.Add(1);
            MyList<int>.list.Add(2);
            MyList<int>.list.Add(1);
            MyList<int>.list.Add(7);
            MyList<int>.list.Add(1);
            MyList<int>.list.Add(9);
            MyList<int>.list.Add(7);
            MyList<int>.list.Add(2);
            MyList<int>.list.Add(9);

            Console.WriteLine("\nСоздан следующий список элементов:");
            MyList<int>.Print();

            Console.WriteLine("\nКоличество элементов в нём состовляет:");

            foreach (KeyValuePair<int, int> item in MyList<int>.GetStatistic())
            {
                Console.WriteLine($"{item.Key} : {item.Value}");
            }

            Console.WriteLine("\nКоличество элементов при помощи linq:");

            var orderedItems = MyList<int>.list.GroupBy(l => l);

            foreach (var item in orderedItems)
                Console.WriteLine($"{item.Key} : {item.Count()}");

            Console.ReadLine();
        }
    }
}
