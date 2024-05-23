using System.Globalization;
using System.Xml.Linq;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            var tanks = GetTanks();
            var units = GetUnits();
            var factories = GetFactories();
            Console.WriteLine($"Количество резервуаров: {tanks.Length}, установок: {units.Length}");
            var foundUnit = FindUnit(units, tanks, "Резервуар 2");
            var factory = FindFactory(factories, foundUnit);

            Console.WriteLine($"Резервуар 2 принадлежит установке {foundUnit.Name} и заводу {factory.Name}");

            var totalVolume = GetTotalVolume(tanks);
            Console.WriteLine($"Общий объем резервуаров: {totalVolume}");

            PrintTanksWithUnitsAndFactories(tanks, units, factories);
            PrintTotalTankLoad();
            SearchByName(tanks, units, factories);
        }

        // Задание 4
        public static void PrintTanksWithUnitsAndFactories(Tank[] tanks, Unit[] units, Factory[] factories)
        {
            foreach (Tank tank in tanks)
            {
                Unit unit = Array.Find(units, u => u.Id == tank.UnitId);
                Factory factory = Array.Find(factories, f => f.Id == unit.FactoryId);

                Console.WriteLine($"Резервуар: {tank.Name}, Тип: {tank.Description}, Цех: {unit.Name}, Завод: {factory.Name}");
            }
        }

        // Задание 5
        public static void PrintTotalTankLoad()
        {
            Tank[] tanks = GetTanks();
            int totalLoad = 0;

            foreach (Tank tank in tanks)
            {
                totalLoad += tank.Volume;
            }

            Console.WriteLine($"Общая сумма загрузки всех резервуаров: {totalLoad}");
        }

        // Задание 6
        public static void SearchByName(Tank[] tanks, Unit[] units, Factory[] factories)
        {
            Console.Write("Введите наименование для поиска: ");
            string name = Console.ReadLine();

            var foundTanks = tanks.Where(t => t.Name.Contains(name)).ToList();
            var foundUnits = units.Where(u => u.Name.Contains(name)).ToList();
            var foundFactories = factories.Where(f => f.Name.Contains(name)).ToList();

            if (foundTanks.Count > 0)
            {
                Console.WriteLine("Найденные резервуары:");
                foreach (var tank in foundTanks)
                {
                    Console.WriteLine(tank.Name);
                }
            }

            if (foundUnits.Count > 0)
            {
                Console.WriteLine("Найденные цеха:");
                foreach (var unit in foundUnits)
                {
                    Console.WriteLine(unit.Name);
                }
            }

            if (foundFactories.Count > 0)
            {
                Console.WriteLine("Найденные заводы:");
                foreach (var factory in foundFactories)
                {
                    Console.WriteLine(factory.Name);
                }
            }

            if (foundTanks.Count == 0 && foundUnits.Count == 0 && foundFactories.Count == 0)
            {
                Console.WriteLine("Ничего не найдено.");
            }
        }


        // реализуйте этот метод, чтобы он возвращал массив резервуаров, согласно приложенным таблицам
        // можно использовать создание объектов прямо в C# коде через new, или читать из файла (на своё усмотрение)
        public static Tank[] GetTanks()
        {
            // ваш код здесь
            Tank[] tanks = new Tank[6];
            Tank tank_1 = new(1, "Резервуар 1", "Надземный - вертикальный", 1500, 2000, 1);
            Tank tank_2 = new(2, "Резервуар 2", "Надземный - горизонтальный", 2500, 3000, 1);
            Tank tank_3 = new(3, "Дополнительный резервуар 24", "Надземный - горизонтальный", 3000, 3000, 2);
            Tank tank_4 = new(4, "Резервуар 35", "Надземный - вертикальный", 3000, 3000, 2);
            Tank tank_5 = new(5, "Резервуар 47", "Подземный - двустенный", 4000, 5000, 2);
            Tank tank_6 = new(6, "Резервуар 256", "Подводный", 500, 500, 3);
            tanks[0] = tank_1;
            tanks[1] = tank_2;
            tanks[2] = tank_3;
            tanks[3] = tank_4;
            tanks[4] = tank_5;
            tanks[5] = tank_6;
            return tanks;
        }
        // реализуйте этот метод, чтобы он возвращал массив установок, согласно приложенным таблицам
        public static Unit[] GetUnits()
        {
            // ваш код здесь
            Unit[] units = new Unit[3];
            Unit unit_1 = new(1, "ГФУ-2", "Газофракционирующая установка", 1);
            Unit unit_2 = new(2, "АВТ-6", "Атмосферно-вакуумная трубчатка", 1);
            Unit unit_3 = new(3, "АВТ-10", "Атмосферно-вакуумная трубчатка", 2);
            units[0] = unit_1;
            units[1] = unit_2;
            units[2] = unit_3;
            return units;
        }
        // реализуйте этот метод, чтобы он возвращал массив заводов, согласно приложенным таблицам
        public static Factory[] GetFactories()
        {
            // ваш код здесь
            Factory[] factories = new Factory[2];
            Factory factory_1 = new(1, "НПЗ№1", "Первый нефтеперерабатывающий завод");
            Factory factory_2 = new(2, "НПЗ№2", "Второй нефтеперерабатывающий завод");
            factories[0] = factory_1;
            factories[1] = factory_2;
            return factories;
        }

        // реализуйте этот метод, чтобы он возвращал установку (Unit), которой
        // принадлежит резервуар (Tank), найденный в массиве резервуаров по имени
        // учтите, что по заданному имени может быть не найден резервуар
        public static Unit FindUnit(Unit[] units, Tank[] tanks, string unitName)
        {
            // ваш код здесь
            foreach (var tank in tanks)
            {
                if (tank.Name == unitName)
                {
                    foreach (var unit in units)
                    {
                        if (tank.UnitId == unit.Id)
                        {
                            return unit;
                        }
                    }
                }
            }
            return null;
        }

        // реализуйте этот метод, чтобы он возвращал объект завода, соответствующий установке
        public static Factory FindFactory(Factory[] factories, Unit unit)
        {
            // ваш код здесь
            foreach (var factory in factories)
            {
                if (factory.Id == unit.FactoryId)
                {
                    return factory;
                }
            }
            return null;
        }

        // реализуйте этот метод, чтобы он возвращал суммарный объем резервуаров в массиве
        public static int GetTotalVolume(Tank[] units)
        {
            // ваш код здесь
            int totalVolume = 0;
            foreach (var tank in units)
            {
                totalVolume += tank.Volume;
            }
            return totalVolume;
        }
    }

    /// <summary>
    /// Установка
    /// </summary>
    public class Unit
    {
        //..
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FactoryId { get; set; }
        public Unit()
        {
            Id = 0;
            Name = "";
            Description = "";
            FactoryId = 0;
        }
        public Unit(int id, string name, string description, int factory_id)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.FactoryId = factory_id;
        }
    }

    /// <summary>
    /// Завод
    /// </summary>
    public class Factory
    {
        //..
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Factory()
        {
            this.Id = 1;
            this.Name = "";
            this.Description = "";
        }
        public Factory(int id, string name, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
        }
    }

    /// <summary>
    /// Резервуар
    /// </summary>
    public class Tank
    {
        //..
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Volume { get; set; }
        public int MaxVolume { get; set; }
        public int UnitId { get; set; }

        public Tank() 
        {
            Id = 1;
            Name = "";
            Description = "";
            Volume = 1;
            MaxVolume = 1;
            UnitId = 1;
        }
        public Tank(int id, string name, string description, int volume, int max_volume, int unit_id)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Volume = volume;
            this.MaxVolume = max_volume;
            this.UnitId = unit_id;
        }
    }
}
