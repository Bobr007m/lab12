using System;
using Geometryclass;

namespace lab12
{
    public class Program
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            // 1. Сформировать двунаправленный список
            List<Geometryfigure1> figureList = new List<Geometryfigure1>();

            // Заполняем список 5 случайными фигурами
            for (int i = 0; i < 5; i++)
            {
                Geometryfigure1 figure = CreateRandomFigure();
                figureList.AddToEnd(figure);
            }

            // 2. Распечатать полученный список
            Console.WriteLine("Исходный список фигур:");
            figureList.PrintList();

            // 3. Выполнить обработку списка (добавление и удаление)
            Console.WriteLine("\nДобавляем новую фигуру после первого элемента:");
            Geometryfigure1 newFigure = new Rectangle1 { Name = "Новый прямоугольник", Width = 10, Length = 5 };
            figureList.AddAfter(figureList.begin.Data.Name, newFigure);
            figureList.PrintList();

            Console.WriteLine("\nУдаляем все окружности:");
            figureList.RemoveAllByName("Окружность");
            figureList.PrintList();

            // 4. Распечатать список после обработки
            Console.WriteLine("\nСписок после обработки:");
            figureList.PrintList();

            // 5. Выполнить глубокое клонирование списка
            Console.WriteLine("\nКлонируем список:");
            List<Geometryfigure1> clonedList = figureList.DeepClone();
            clonedList.PrintList();

            // Модифицируем клон для демонстрации глубокого копирования
            if (clonedList.begin != null)
            {
                clonedList.begin.Data.Name = "Модифицированная фигура";
            }

            Console.WriteLine("\nОригинальный список после модификации клона:");
            figureList.PrintList();

            Console.WriteLine("\nКлонированный список после модификации:");
            clonedList.PrintList();

            // 6. Удалить списки из памяти
            figureList.Clear();
            clonedList.Clear();
            Console.WriteLine("\nСписки удалены из памяти.");

            Console.ReadKey();
        }
    }
 }
