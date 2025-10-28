using System;
using System.Collections.Generic;

namespace VolkovaFactoryPrototype
{
    internal class ProgramVolkova
    {
        public abstract class ShapeVolkova : ICloneable
        {
            public abstract string Name { get; }
            public abstract int Cells { get; }
            public abstract object Clone();
        }

        public class LineShapeVolkova : ShapeVolkova
        {
            public override string Name => "Линия";
            public override int Cells => 4;

            public override object Clone()
            {
                return new LineShapeVolkova();
            }
        }

        public class SquareShapeVolkova : ShapeVolkova
        {
            public override string Name => "Квадрат";
            public override int Cells => 4;

            public override object Clone()
            {
                return new SquareShapeVolkova();
            }
        }

        public class TShapeVolkova : ShapeVolkova
        {
            public override string Name => "Т-фигура";
            public override int Cells => 4;

            public override object Clone()
            {
                return new TShapeVolkova();
            }
        }

        public class CrossShapeVolkova : ShapeVolkova
        {
            public override string Name => "Крест";
            public override int Cells => 5;

            public override object Clone()
            {
                return new CrossShapeVolkova();
            }
        }

        public class ShapeFactoryVolkova
        {
            private readonly List<ShapeVolkova> _prototypesVolkova;
            private readonly Random _randomVolkova;

            public ShapeFactoryVolkova()
            {
                _randomVolkova = new Random();
                _prototypesVolkova = new List<ShapeVolkova>
            {
                new LineShapeVolkova(),
                new SquareShapeVolkova(),
                new TShapeVolkova(),
                new CrossShapeVolkova()
            };
            }

            public ShapeVolkova CreateRandomShapeVolkova()
            {
                int index = _randomVolkova.Next(_prototypesVolkova.Count);
                return (ShapeVolkova)_prototypesVolkova[index].Clone();
            }

            public ShapeVolkova CreateSpecificShapeVolkova(string shapeType)
            {
                switch (shapeType.ToLower())
                {
                    case "линия":
                        return new LineShapeVolkova();
                    case "квадрат":
                        return new SquareShapeVolkova();
                    case "т":
                        return new TShapeVolkova();
                    case "крест":
                        return new CrossShapeVolkova();
                    default:
                        return new LineShapeVolkova();
                }
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Фигуры\n");

            ShapeFactoryVolkova factoryVolkova = new ShapeFactoryVolkova();

            // Создание случайных фигур
            Console.WriteLine("Случайные фигуры:");
            for (int i = 0; i < 3; i++)
            {
                ShapeVolkova shape = factoryVolkova.CreateRandomShapeVolkova();
                ShapeVolkova clonedShape = (ShapeVolkova)shape.Clone();

                Console.WriteLine($"Оригинал: {shape.Name} (клеток: {shape.Cells})");
                Console.WriteLine($"Клон: {clonedShape.Name} (клеток: {clonedShape.Cells})");
                Console.WriteLine("---");
            }

            // Создание специфических фигур
            Console.WriteLine("\nСпецифические фигуры:");
            string[] specificShapes = { "квадрат", "крест" };

            foreach (string shapeType in specificShapes)
            {
                ShapeVolkova specificShape = factoryVolkova.CreateSpecificShapeVolkova(shapeType);
                Console.WriteLine($"Создана: {specificShape.Name}");
            }
        }
    }
}
