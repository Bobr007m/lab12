using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab12
{
    internal class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 10; i++) 
            {
                list.Add(rnd.Next(100));
            }
           list.PrintList();
            list.Add(100);
            Console.WriteLine(list.Contains(100));
            int[] arr = new int[list.Count + 5];
            list.CopyTo(arr, 1);
            foreach (var item in arr)
            {
                Console.WriteLine(item+ " ");
            }
            Console.WriteLine();
            Console.WriteLine("delete last");
            list.Remove(100);
            list.PrintList();
            Console.WriteLine(" add first");
            list.AddToBegin(new Point<int>(100));
            list.PrintList();
            Console.WriteLine("delete first");
            list.Remove(100);
            list.PrintList();
            List<int> list2 = new List<int>();
            list2.Add(1);
            list2.Add(2);
            list2.Add(3);
            list2.Remove(2);
            list2.PrintList();


        }
    }
}
