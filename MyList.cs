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
    public class MyList<T> : ICloneable where T : Geometryfigure1, IIni, ICloneable, new()
    {
        public Point<T> begin; // начало списка
        public Point<T> end;    // конец списка
        public int Count;       // счетчик количества элементов

        public MyList()
        {
            begin = null;
            end = null;
            Count = 0;
        }

        public MyList(int Length)
        {
            begin = null;
            end = null;
            Count = 0;

            for (int i = 0; i < Length; i++)
            {
                T Data = new T(); // конструктор без параметра
                Data.RandomInit();
                AddToEnd(Data);
            }
        }

        public void Add(T item)
        {
            AddToEnd(item);
        }

        public void Add(int number, T item)
        {
            if (number > Count + 1)
                throw new Exception("номер больше, чем количество элементов в списке");

            if (number == 0 || begin == null) // в начало
            {
                AddToBegin(item);
                return;
            }

            if (number == Count) // в конец
            {
                AddToEnd(item);
                return;
            }

            // Вставка в середину
            int count = 1;
            Point<T> current = begin;
            while (current != null && count < number)
            {
                current = current.Next;
                count++;
            }

            if (current != null)
            {
                Point<T> newPoint = new Point<T>(item);
                newPoint.Next = current.Next;
                newPoint.Prev = current;
                if (current.Next != null)
                    current.Next.Prev = newPoint;
                current.Next = newPoint;
                Count++;
            }
        }

        public void AddToEnd(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var newPoint = new Point<T>(item);

            if (begin == null)
            {
                begin = newPoint;
                end = newPoint;
            }
            else
            {
                end.Next = newPoint;
                newPoint.Prev = end;
                end = newPoint;
            }
            Count++;
        }

        public void AddToBegin(T item)
        {
            Point<T> newPoint = new Point<T>(item);

            if (begin == null)
            {
                begin = newPoint;
                end = newPoint;
            }
            else
            {
                newPoint.Next = begin;
                begin.Prev = newPoint;
                begin = newPoint;
            }
            Count++;
        }

        public void Clear()
        {
            begin = null;
            end = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            Point<T> current = begin;
            while (current != null && !current.Data.Equals(item))
                current = current.Next;
            return current != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new Exception("Массив не может быть null");
            if (arrayIndex < 0) throw new Exception("Индекс не может быть меньше 0");
            if (array.Length - arrayIndex < Count) throw new Exception("Список не помещается в массив");

            Point<T> current = begin;
            int i = arrayIndex;
            while (current != null && i < array.Length)
            {
                array[i++] = current.Data;
                current = current.Next;
            }
        }

        public bool Remove(T item)
        {
            if (begin == null) return false;

            // Удаление первого элемента
            if (begin.Data.Equals(item))
            {
                begin = begin.Next;
                if (begin != null)
                    begin.Prev = null;
                else
                    end = null;
                Count--;
                return true;
            }

            // Удаление последнего элемента
            if (end.Data.Equals(item))
            {
                end = end.Prev;
                end.Next = null;
                Count--;
                return true;
            }

            // Удаление из середины
            Point<T> current = begin.Next;
            while (current != null && !current.Data.Equals(item))
            {
                current = current.Next;
            }

            if (current != null)
            {
                current.Prev.Next = current.Next;
                if (current.Next != null)
                    current.Next.Prev = current.Prev;
                Count--;
                return true;
            }

            return false;
        }

        public void PrintList()
        {
            Point<T> current = begin;
            int count = 1;
            while (current != null)
            {
                Console.WriteLine($"{count}: {current.Data}");
                current = current.Next;
                count++;
            }
        }

        public void PrintListReverse()
        {
            Point<T> current = end;
            int count = Count;
            while (current != null)
            {
                Console.WriteLine($"{count}: {current.Data}");
                current = current.Prev;
                count--;
            }
        }

        public object Clone()
        {
            MyList<T> newlist = new MyList<T>();
            if (begin == null) return newlist;

            Point<T> current = begin;
            while (current != null)
            {
                newlist.AddToEnd((T)current.Data.Clone());
                current = current.Next;
            }
            return newlist;
        }

        public void AddAfter(string figureName, T newItem)
        {
            if (begin == null)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            Point<T> current = begin;
            while (current != null)
            {
                if (current.Data.Name == figureName)
                {
                    Point<T> newPoint = new Point<T>(newItem);
                    newPoint.Next = current.Next;
                    newPoint.Prev = current;

                    if (current.Next != null)
                        current.Next.Prev = newPoint;
                    else
                        end = newPoint;

                    current.Next = newPoint;
                    Count++;
                    return;
                }
                current = current.Next;
            }
            Console.WriteLine($"Фигура с именем '{figureName}' не найдена");
        }

        public void RemoveAllByName(string figureName)
        {
            if (begin == null) return;

            // Удаление элементов в начале списка
            while (begin != null && begin.Data.Name == figureName)
            {
                begin = begin.Next;
                if (begin != null)
                    begin.Prev = null;
                else
                    end = null;
                Count--;
            }

            if (begin == null) return;

            // Удаление элементов в середине и конце
            Point<T> current = begin;
            while (current != null)
            {
                if (current.Data.Name == figureName)
                {
                    current.Prev.Next = current.Next;
                    if (current.Next != null)
                        current.Next.Prev = current.Prev;
                    else
                        end = current.Prev;
                    Count--;
                }
                current = current.Next;
            }
        }
    }
}