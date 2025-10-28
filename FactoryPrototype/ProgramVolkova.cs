using System;
using System.Collections.Generic;

namespace VolkovaFactoryPrototype
{
    public abstract class FigureVolkova : ICloneable
    {
        public abstract string NameFigure { get; }
        public abstract int CellsCount { get; }

        public string Name { get; set; }

        public string FigureType
        {
            get { return CellsCount > 4 ? "Супер-фигура" : "Обычная фигура"; }
        }

        public abstract object Clone();

        public void PrintPropertiesVolkova()
        {
            Console.WriteLine($"{Name} ({NameFigure} (Число клеток: {CellsCount}) - {FigureType}");
        }
    }

    // Конкретные фигуры
    public class LineVolkova : FigureVolkova
    {
        public override int CellsCount => 4;
        public override string NameFigure => "Линия";

        public override object Clone()
        {
            return new LineVolkova() { Name = this.Name };
        }
    }

    public class SquareVolkova : FigureVolkova
    {
        public override int CellsCount => 4;
        public override string NameFigure => "Квадрат";

        public override object Clone()
        {
            return new SquareVolkova() { Name = this.Name };
        }
    }

    // Супер-фигуры
    public class CrossVolkova : FigureVolkova
    {
        public override int CellsCount => 5;
        public override string NameFigure => "Крест";

        public override object Clone()
        {
            return new CrossVolkova() { Name = this.Name };
        }
    }

    public class SuperLineVolkova : FigureVolkova
    {
        public override int CellsCount => 6;
        public override string NameFigure => "Супер-линия";

        public override object Clone()
        {
            return new SuperLineVolkova() { Name = this.Name };
        }
    }

    public class FigureFactoryVolkova
    {
        private readonly List<FigureVolkova> _prototypesVolkova;
        private readonly Random _rndVolkova;

        public FigureFactoryVolkova()
        {
            _rndVolkova = new Random();
            _prototypesVolkova = new List<FigureVolkova>
            {
                new LineVolkova() { Name = "Линия" },
                new SquareVolkova() { Name = "Квадрат" },
                new CrossVolkova() { Name = "Крест" },
                new SuperLineVolkova() { Name = "Супер-линия" }
            };
        }

        public FigureVolkova GenerateRandomFigureVolkova()
        {
            int index = _rndVolkova.Next(_prototypesVolkova.Count);
            return (FigureVolkova)_prototypesVolkova[index].Clone();
        }

        public FigureVolkova CreateFigureByNameVolkova(string name)
        {
            foreach (var prototype in _prototypesVolkova)
            {
                if (prototype.NameFigure.Contains(name))
                {
                    return (FigureVolkova)prototype.Clone();
                }
            }
            return null;
        }
    }

    internal class ProgramVolkova
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Генератор фигур ===\n");

            FigureFactoryVolkova factory = new FigureFactoryVolkova();

            // Генерация случайных фигур
            for (int i = 0; i < 3; i++)
            {
                FigureVolkova figure = factory.GenerateRandomFigureVolkova();
                FigureVolkova clonedFigure = (FigureVolkova)figure.Clone();

                Console.WriteLine($"Набор фигур #{i + 1}:");
                Console.WriteLine("Оригинал фигуры:");
                figure.PrintPropertiesVolkova();

                Console.WriteLine("Копия фигуры:");
                clonedFigure.PrintPropertiesVolkova();

                Console.WriteLine(new string('=', 40));
            }

            // Демонстрация создания конкретной фигуры
            Console.WriteLine("\n=== Создание конкретных фигур ===");
            FigureVolkova specificFigure = factory.CreateFigureByNameVolkova("Квадрат");
            if (specificFigure != null)
            {
                specificFigure.PrintPropertiesVolkova();
            }
        }
    }
}
