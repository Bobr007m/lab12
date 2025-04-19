using Geometryclass;
using System;

namespace lab12
{
    class Point<T> where T : Geometryfigure1, ICloneable, new()
    {
        public T Data { get; set; }
        public Point<T> Next { get; set; }
        public Point<T> Prev { get; set; }  //  для двунаправленного списка

        // Конструктор по умолчанию
        public Point()
        {
            Data = new T();  // новый объект типа T
            Data.RandomInit();  
            Next = null;
            Prev = null;
        }

        // Конструктор с параметром
        public Point(T data)
        {
            Data = (T)data.Clone();  // Глубокое копирование данных
            Next = null;
            Prev = null;
        }

        // Метод для глубокого копирования 
        public Point<T> DeepCopy()
        {
            Point<T> newPoint = new Point<T>((T)Data.Clone());
            newPoint.Next = Next != null ? Next.DeepCopy() : null;
            newPoint.Prev = Prev != null ? Prev.DeepCopy() : null;
            return newPoint;
        }

        // Метод для сравнения по имени фигуры
        public bool CompareByName(string name)
        {
            return Data.Name.Equals(name, StringComparison.OrdinalIgnoreCase);
        }

        // Переопределение ToString()
        public override string ToString()
        {
            return Data?.ToString() ?? "null";
        }

        // Метод для вывода информации о фигуре
        public void Show()
        {
            Data?.Show();
        }

        // Метод для проверки соответствия критерию (по имени)
        public bool MatchesCriteria(string criteria)
        {
            return Data != null && Data.Name.Contains(criteria);
        }
    }
}
