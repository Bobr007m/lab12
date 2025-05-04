using System;
using Geometryclass;

namespace lab12
{
    public class Point<T> where T : Geometryfigure1, ICloneable, new()
    {
        public T Data { get; set; }
        public Point<T> Next { get; set; }
        public Point<T> Prev { get; set; }

        // Конструктор по умолчанию
        public Point()
        {
            Data = new T();
            Data.RandomInit();
            Next = null;
            Prev = null;
        }

        // Конструктор с параметром
        public Point(T data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            // Проверяем, что data - действительно Geometryfigure1
            if (!(data is Geometryfigure1))
                throw new ArgumentException("Данные должны быть типа Geometryfigure1");

            Data = data;
            Next = null;
            Prev = null;
        }

        // Метод для глубокого копирования 
        public Point<T> DeepClone()
        {
            var newPoint = new Point<T>(Data);
            
            if (Next != null)
            {
                newPoint.Next = Next.DeepClone();
                newPoint.Next.Prev = newPoint;
            }
            
            return newPoint;
        }

        // Метод для сравнения по имени фигуры
        public bool CompareByName(string name)
        {
            return Data?.Name?.Equals(name, StringComparison.OrdinalIgnoreCase) ?? false;
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
            return Data?.Name?.Contains(criteria) ?? false;
        }
    }
}