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
            Point<T> current = begin;
            while (current != null)
            {
                Point<T> next = current.Next;
                current.Prev = null;
                current.Next = null;
                current = next;
            }

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
        public void PrintList()
        {
            Point<T> current = begin;
            int count = 1;
            while (current != null)
            {
                var figure = current.Data;
                Console.WriteLine($"{count}: {figure?.Name ?? "[Без имени]"} ({figure?.GetType().Name ?? "Неизвестный тип"})");
                current = current.Next;
                count++;
            }
        }

        public object Clone()
        {
            MyList<T> newList = new MyList<T>();
            if (begin == null)
                return newList;

            Point<T> current = begin;
            Point<T> newPoint = new Point<T>((T)current.Data.Clone());
            newList.begin = newPoint;
            newList.end = newPoint;

            current = current.Next;
            while (current != null)
            {
                Point<T> temp = new Point<T>((T)current.Data.Clone());
                newList.end.Next = temp;
                temp.Prev = newList.end;
                newList.end = temp;
                current = current.Next;
            }

            newList.Count = this.Count;
            return newList;
        }

        public void AddAfter(string figureName, T newItem)
        {
            if (begin == null)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            if (newItem == null)
            {
                Console.WriteLine("Нельзя добавить null-элемент");
                return;
            }

            // Нормализация введённого имени
            figureName = figureName?.Trim();

            if (string.IsNullOrEmpty(figureName))
            {
                Console.WriteLine("Имя для поиска не может быть пустым");
                return;
            }

            Point<T> current = begin;
            bool found = false;

            while (current != null)
            {
                // Нормализация имени из списка и сравнение без учёта регистра
                var currentName = current.Data.Name?.Trim();
                if (string.Equals(currentName, figureName, StringComparison.OrdinalIgnoreCase))
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
                    found = true;
                }
                current = current.Next;
            }

            if (!found)
            {
                Console.WriteLine($"Фигура с именем '{figureName}' не найдена");
            }
        }

        public void RemoveAllByName(string figureName)
        {
            if (begin == null)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            // Нормализация введённого имени
            figureName = figureName?.Trim();

            if (string.IsNullOrEmpty(figureName))
            {
                Console.WriteLine("Имя для удаления не может быть пустым");
                return;
            }

            // Удаление элементов в начале списка
            while (begin != null &&
                   string.Equals(begin.Data.Name?.Trim(), figureName, StringComparison.OrdinalIgnoreCase))
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
                var currentName = current.Data.Name?.Trim();
                if (string.Equals(currentName, figureName, StringComparison.OrdinalIgnoreCase))
                {
                    if (current.Prev != null)
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