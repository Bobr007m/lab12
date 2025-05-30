
using System;
using Geometryclass;

namespace lab12
{
    public class Program
    {
        public static MyList<Geometryfigure1> figureList = new MyList<Geometryfigure1>();
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Меню для работы с двунаправленным списком фигур");
                Console.WriteLine("1. Создать новый список со случайными фигурами");
                Console.WriteLine("2. Добавить фигуру в конец списка");
                Console.WriteLine("3. Добавить фигуру после указанной");
                Console.WriteLine("4. Удалить фигуры по имени");
                Console.WriteLine("5. Вывести список на экран");
                Console.WriteLine("6. Выполнить глубокое клонирование списка");
                Console.WriteLine("7. Удалить список из памяти");
                Console.WriteLine("8. Выход");
                Console.Write("Выберите действие: ");

                var input = Console.ReadLine();
                try
                {
                    switch (input)
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
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }

                if (!exit)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            }
        }

        public static void CreateRandomList()
        {
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
                Console.WriteLine("Некорректный ввод! Должно быть положительное число.");
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
            var choice = Console.ReadLine();
            switch (choice)
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

            figure.RandomInit();
            figureList.AddToEnd(figure);
            Console.WriteLine("Фигура добавлена в конец списка.");
        }

        static void AddFigureAfter()
        {
            if (figureList.Count == 0)
            {
                Console.WriteLine("Список пуст!");
                return;
            }

            Console.Write("Введите название фигуры, после которой нужно добавить новую: ");
            string searchName = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(searchName))
            {
                Console.WriteLine("Имя не может быть пустым!");
                return;
            }

            Console.WriteLine("Выберите тип новой фигуры:");
            Console.WriteLine("1. Прямоугольник");
            Console.WriteLine("2. Окружность");
            Console.WriteLine("3. Параллелепипед");
            Console.Write("Ваш выбор: ");

            Geometryfigure1 newFigure = null;
            var choice = Console.ReadLine();
            switch (choice)
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

            newFigure.RandomInit();

            figureList.AddAfter(searchName, newFigure);
        }

        static void RemoveFiguresByName()
        {
            Console.Write("Введите название фигур для удаления: ");
            string name = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Имя не может быть пустым!");
                return;
            }

            figureList.RemoveAllByName(name);
            Console.WriteLine($"Фигуры с именем '{name}' удалены.");
        }
        static void PrintList()
        {
            if (figureList.Count == 0)
            {
                Console.WriteLine("Список пуст!");
                return;
            }

            Console.WriteLine("Текущий список фигур:");
            figureList.PrintList();
        }


        static void CloneList()
        {
            if (figureList.Count == 0)
            {
                Console.WriteLine("Список пуст, нечего клонировать!");
                return;
            }

            var clonedList = new MyList<Geometryfigure1>();
            var current = figureList.begin;

            while (current != null)
            {
                try
                {
                    var cloned = current.Data.Clone();
                    if (cloned is Geometryfigure1 figure)
                    {
                        clonedList.AddToEnd(figure);
                    }
                    else
                    {
                        throw new Exception($"Некорректный тип при клонировании: {cloned?.GetType().Name}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при клонировании элемента: {ex.Message}");
                    Console.WriteLine($"Тип элемента: {current.Data.GetType().Name}");
                    return;
                }

                current = current.Next;
            }

            Console.WriteLine("Список успешно клонирован. Элементов: " + clonedList.Count);
            clonedList.PrintList();
        }

        static void ClearList()
        {
            figureList.Clear();
            Console.WriteLine("Список очищен и удален из памяти.");
        }

        public static Geometryfigure1 CreateRandomFigure()
        {
            Geometryfigure1 figure;
            switch (rnd.Next(0, 3))
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
                    throw new InvalidOperationException("Неизвестный тип фигуры");
            }

            figure.RandomInit();
            figure.Name = $"Фигура_{rnd.Next(1000, 9999)}";
            return figure;
        }
    }
}