using System;
using System.Collections.Generic;

namespace VolkovaSingleton
{
    public sealed class BuildingVolkova
    {
        private static readonly Lazy<BuildingVolkova> _instance =
            new Lazy<BuildingVolkova>(() => new BuildingVolkova());

        public static BuildingVolkova Instance => _instance.Value;

        public List<FloorVolkova> Floors { get; private set; }
        public ElevatorVolkova Elevator { get; private set; }

        private BuildingVolkova()
        {
            Floors = new List<FloorVolkova>();
            Elevator = ElevatorVolkova.Instance;
            InitializeBuildingVolkova();
        }

        private void InitializeBuildingVolkova()
        {
            for (int i = 1; i <= 5; i++)
            {
                Floors.Add(new FloorVolkova(i, $"Этаж {i}"));
            }

            Console.WriteLine("Здание инициализировано");
        }

        public void DisplayBuildingInfoVolkova()
        {
            Console.WriteLine($"\n=== Здание ===");
            Console.WriteLine($"Количество этажей: {Floors.Count}");
            Console.WriteLine($"Текущий этаж лифта: {Elevator.CurrentFloorVolkova}");
            Console.WriteLine($"Состояние лифта: {Elevator.StateVolkova}");
        }
    }

    public sealed class ElevatorVolkova
    {
        private static readonly Lazy<ElevatorVolkova> _instance =
            new Lazy<ElevatorVolkova>(() => new ElevatorVolkova());

        public static ElevatorVolkova Instance => _instance.Value;

        public int CurrentFloorVolkova { get; private set; }
        public string StateVolkova { get; private set; }

        private ElevatorVolkova()
        {
            CurrentFloorVolkova = 1;
            StateVolkova = "Ожидание";
            Console.WriteLine("Лифт создан и находится на 1 этаже");
        }

        public void MoveToFloorVolkova(int targetFloor)
        {
            if (targetFloor < 1 || targetFloor > 5)
            {
                Console.WriteLine("Ошибка: неверный номер этажа");
                return;
            }

            StateVolkova = "Движение";
            Console.WriteLine($"Лифт движется с {CurrentFloorVolkova} на {targetFloor} этаж");

            CurrentFloorVolkova = targetFloor;
            StateVolkova = "Ожидание";

            Console.WriteLine($"Лифт прибыл на {CurrentFloorVolkova} этаж");
        }

        public void CallElevatorVolkova(int fromFloor)
        {
            Console.WriteLine($"\nВызов лифта с {fromFloor} этажа");
            MoveToFloorVolkova(fromFloor);
        }
    }

    public class FloorVolkova
    {
        public int Number { get; }
        public string Name { get; }
        public List<RoomVolkova> Rooms { get; }

        public FloorVolkova(int number, string name)
        {
            Number = number;
            Name = name;
            Rooms = new List<RoomVolkova>();
            InitializeRoomsVolkova();
        }

        private void InitializeRoomsVolkova()
        {
            for (int i = 1; i <= 3; i++)
            {
                Rooms.Add(new RoomVolkova($"Помещение {Number}-{i}"));
            }
        }
    }

    public class RoomVolkova
    {
        public string Name { get; }

        public RoomVolkova(string name)
        {
            Name = name;
        }
    }

    public class BuildingManagementSystemVolkova
    {
        private BuildingVolkova _buildingVolkova;

        public BuildingManagementSystemVolkova()
        {
            _buildingVolkova = BuildingVolkova.Instance;
        }

        public void SimulateBuildingOperationVolkova()
        {
            Console.WriteLine("\n=== Симуляция работы системы ===");

            _buildingVolkova.Elevator.CallElevatorVolkova(3);
            _buildingVolkova.Elevator.MoveToFloorVolkova(5);
            _buildingVolkova.Elevator.MoveToFloorVolkova(1);

            _buildingVolkova.DisplayBuildingInfoVolkova();
        }
    }

    internal class ProgramVolkova
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Система 'Лифт в зданииа' ===\n");

            var building1 = BuildingVolkova.Instance;
            var elevator1 = ElevatorVolkova.Instance;

            var building2 = BuildingVolkova.Instance;
            var elevator2 = ElevatorVolkova.Instance;

            Console.WriteLine($"\nПроверка синглтонов:");
            Console.WriteLine($"building1 == building2: {building1 == building2}");
            Console.WriteLine($"elevator1 == elevator2: {elevator1 == elevator2}");

            var managementSystem = new BuildingManagementSystemVolkova();
            managementSystem.SimulateBuildingOperationVolkova();

            Console.WriteLine("\n=== Дополнительная демонстрация ===");
            building1.Elevator.CallElevatorVolkova(2);
            building1.DisplayBuildingInfoVolkova();

            Console.WriteLine("\nПрограмма завершена!");
        }

    }
}
