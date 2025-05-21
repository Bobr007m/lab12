using System;
using Geometryclass;

namespace lab12
{
    public class Point<T> where T : Geometryfigure1, ICloneable, IIni, new()
    {
        public T Data { get; set; }
        public Point<T> Next { get; set; }
        public Point<T> Prev { get; set; }

        public Point(T data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (!(data is Geometryfigure1))
                throw new ArgumentException("Data должен быть типа Geometryfigure1");

            Data = data;
            Next = null;
            Prev = null;
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

       
    }
}