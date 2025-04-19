using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Geometryclass;

namespace lab12
{
    class List<T> : ICloneable where T : IIni, new()
    {
        public Point<T> begin; // начало списка
        public int Count // счетчик количества элементов
        {
            get
            {
                int count = 0;
                if (begin == null) return 0;
                Point<T> current = begin;
                while (current != null)
                {
                    count++;
                    current = current.Next;
                }
                return count;
            }
        }

        public List()
        {
            begin = null;
        }

        public List(int Length)
        {
            for (int i = 0; i < Length; i++)
            {
                T Data = new T(); // конструктор без параметра
                Data.RandomInit();
                Point<T> item = new Point<T>(Data);
                Add(item.Data);
            }
        }

        public void Add(T item)
        {
            Point<T> newPoint = new Point<T>(item);
            if (begin == null)
                begin = new Point<T>();
            else
                AddToEnd(newPoint);
        }
        public void Add(int number, T item)
        {
            Point<T> newPoint = new Point<T>(item);
            if (number > Count + 1)
                throw new Exception("номер больше, чем количество элементов в списке");
            if (number == 0) // в начало
            {
                AddToBegin(newPoint);
                return;
            }
            int count = 1;
            Point<T> current = begin;
            while (current.Next != null)
            {
                if (count + 1 == number)
                    break;
                count++;
                current = current.Next;
            }
            if (current.Next != null)
                AddToEnd(newPoint);
            else
            {
                newPoint.Next = current.Next;
                current.Next = newPoint;
            }
        }
        public void AddToEnd(Point<T> item)
        {
            Point<T> ptr = begin;
            while (ptr.Next != null)
                ptr = ptr.Next;
            ptr.Next = item;
        }
        public void AddToBegin(Point<T> item)
        {
            item.Next = begin;
            begin = item;
        }

        public void Clear()
        {
            begin = null;
        }

        public bool Contains(T item)
        {
            Point<T> current = begin;
            while (current != null && !current.Data.Equals(item))
                current = current.Next;
            return current != null;
        }

        public void CopyTo(T[] array, int arrayIndex) // копирует элементы из коллекции в массив, начиная с определенного индекса
        {
            if (array == null) throw new Exception("Массив не может быть null");
            if (arrayIndex < 0) throw new Exception("Индекс не может быть меньше 0");
            if (array.Length - arrayIndex < Count) throw new Exception("Список не помещается в массив");
            Point<T> current = begin;
            int i = arrayIndex;
            while (current != null && i < array.Length) {
                array[i++] = current.Data;
                current = current.Next;
            }

            bool Remove(T item)
            {
                if (begin.Data.Equals(item))
                {
                    begin = begin.Next;
                    return true;
                }
                else
                {
                     current = begin;
                    while (current.Next != null && !current.Next.Data.Equals(item))
                    {
                        current = current.Next;
                    }
                    if (current.Next == null)
                        return false;
                    else {
                        current.Next = current.Next.Next;
                        return true;
                    }
                }
            }
        }
        public void PrintList()
        {
            Point<T> current = begin;
            int count = 1;
            while (current != null) {
                Console.WriteLine($"{count}: {current.Data}");
                current = current.Next;
                count++;
            }
        }
        public object Clone()
        {
            List<T> newlist = new List<T>();
            if (begin == null) return null;
            newlist.begin = new Point<T>(begin.Data);
            Point<T> current = begin.Next;
            while (current != null)
            {
                Point<T> newPoint = new Point<T>(current.Data);
                newlist.AddToEnd(newPoint);
                current = current.Next;
            }
            return newlist;
        }
        // Добавление элемента после элемента с заданным информационным полем
        public void AddAfter(T searchitem, T newitem)
        {
            if (begin == null)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            Point<T> current = begin;
            while (current != null)
            {
                if (current.Data.CompareTo(searchitem) == 0)
                {
                    Point<T> newPoint = new Point<T>(newitem);
                    newPoint.Next = current.Next;
                    current.Next = newPoint;
                    return;
                }
                current = current.Next;
            }
            Console.WriteLine($"Элемент {searchitem} не найден в списке");
        }
        // Удаление всех элементов с заданным информационным полем
        public void RemoveAll(T itemToRemove)
        {
            if (begin == null) return;

            // Удаление элементов в начале списка
            while (begin != null && begin.Data.CompareTo(itemToRemove) == 0)
            {
                begin = begin.Next;
            }

            if (begin == null) return;

            Point<T> current = begin;
            while (current.Next != null)
            {
                if (current.Next.Data.CompareTo(itemToRemove) == 0)
                {
                    current.Next = current.Next.Next;
                }
                else
                {
                    current = current.Next;
                }
            }
        }

    }
}
