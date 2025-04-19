using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometryclass;

namespace lab12
{
    internal class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
           
            List<Geometryfigure1> ListOfCeometryFigure = new List<Geometryfigure1>;
            for (int i = 0; i < 5; i++)
            {
                Geometryfigure1 g = new Geometryfigure1();
                g.RandomInit();
                ListOfCeometryFigure.Add(g);
            }
            ListOfCeometryFigure.PrintList();
            Geometryfigure1[] array = new Geometryfigure1[ListOfCeometryFigure.Count];
            ListOfCeometryFigure.CopyTo(array, 0);
            foreach (var figure in array) {
                Console.WriteLine(figure + " ");
            }
            !!!array[0] = new Geometryfigure1();
            Console.WriteLine("List");
            ListOfCeometryFigure.PrintList;
            Console.WriteLine("Array");
            foreach (var figure in array) {
                Console.WriteLine(figure + " ");
            }
            Console.WriteLine("Clone");
            List<Geometryfigure1> list3 = (List<Geometryfigure1>)ListOfCeometryFigure.Clone();
            !!! list3.begin.Data = new Geometryfigure1();
            Console.WriteLine("List");
            list3.PrintList();
            Console.WriteLine("List");
            ListOfCeometryFigure.PrintList;

        }
    }
}
