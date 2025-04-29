using System;
using System.Collections.Generic;
using Geometryclass;
using lab12;

namespace lab12
{
    public class Program
    {
        static MyList<Geometryfigure1> figureList = new MyList<Geometryfigure1>();
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== Меню работы с двунаправленным списком фигур ===");
                Console.WriteLine("1. Создать новый список со случайными фигурами");
                Console.WriteLine("2. Добавить фигуру в конец списка");
                Console.WriteLine("3. Добавить фигуру после указанной");
                Console.WriteLine("4. Удалить фигуры по имени");
                Console.WriteLine("5. Вывести список на экран");
                Console.WriteLine("6. Выполнить глубокое клонирование списка");
                Console.WriteLine("7. Удалить список из памяти");
                Console.WriteLine("8. Выход");
                Console.Write("Выберите действие: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        CreateRandomList();
                        break;
                    case "2":
                        AddFigureToEnd();
                        break;
                    case "3":
                        AddFigureAfter();
                        break;
                    case "4":
                        RemoveFiguresByName();
                        break;
                    case "5":
                        PrintList();
                        break;
                    case "6":
                        CloneList();
                        break;
                    case "7":
                        ClearList();
                        break;
                    case "8":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный ввод!");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            }
        }

        static void CreateRandomList()
        {
            figureList.Clear();
            Console.Write("Введите количество фигур: ");
            if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    figureList.AddToEnd(CreateRandomFigure());
                }
                Console.WriteLine($"Создан список из {count} фигур.");
            }
            else
            {
                Console.WriteLine("Некорректный ввод!");
            }
        }

        static void AddFigureToEnd()
        {
            Console.WriteLine("Выберите тип фигуры:");
            Console.WriteLine("1. Прямоугольник");
            Console.WriteLine("2. Окружность");
            Console.WriteLine("3. Параллелепипед");
            Console.Write("Ваш выбор: ");

            Geometryfigure1 figure = null;
            switch (Console.ReadLine())
            {
                case "1":
                    figure = new Rectangle1();
                    break;
                case "2":
                    figure = new Circle1();
                    break;
                case "3":
                    figure = new Parallelepiped1();
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    return;
            }

            Console.Write("Введите название фигуры: ");
            figure.Name = Console.ReadLine();
            figureList.AddToEnd(figure);
            Console.WriteLine("Фигура добавлена в конец списка.");
        }

        static void AddFigureAfter()
        {
            Console.Write("Введите название фигуры, после которой нужно добавить новую: ");
            string searchName = Console.ReadLine();

            Console.WriteLine("Выберите тип новой фигуры:");
            Console.WriteLine("1. Прямоугольник");
            Console.WriteLine("2. Окружность");
            Console.WriteLine("3. Параллелепипед");
            Console.Write("Ваш выбор: ");

            Geometryfigure1 newFigure = null;
            switch (Console.ReadLine())
            {
                case "1":
                    newFigure = new Rectangle1();
                    break;
                case "2":
                    newFigure = new Circle1();
                    break;
                case "3":
                    newFigure = new Parallelepiped1();
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    return;
            }

            Console.Write("Введите название новой фигуры: ");
            newFigure.Name = Console.ReadLine();
            figureList.AddAfter(searchName, newFigure);
        }

        static void RemoveFiguresByName()
        {
            Console.Write("Введите название фигур для удаления: ");
            string name = Console.ReadLine();
            figureList.RemoveAllByName(name);
            Console.WriteLine($"Фигуры с именем '{name}' удалены.");
        }

        static void PrintList()
        {
            Console.WriteLine("Текущий список фигур:");
            figureList.PrintList();
        }

        static void CloneList()
        {
            var clonedList = (MyList<Geometryfigure1>)figureList.Clone();
            Console.WriteLine("Список успешно клонирован.");
            Console.WriteLine("Содержимое клона:");
            clonedList.PrintList();
        }

        static void ClearList()
        {
            figureList.Clear();
            Console.WriteLine("Список очищен и удален из памяти.");
        }

        static Geometryfigure1 CreateRandomFigure()
        {
            int type = rnd.Next(0, 3);
            Geometryfigure1 figure;

            switch (type)
            {
                case 0:
                    figure = new Rectangle1();
                    break;
                case 1:
                    figure = new Circle1();
                    break;
                case 2:
                    figure = new Parallelepiped1();
                    break;
                default:
                    figure = new Rectangle1();
                    break;
            }

            figure.RandomInit();
            return figure;
        }
    }
}