using lab1;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace lab2
{
    class Program
    {

        static void Main(string[] args)
        {
            var tanks = ReadJsonFile<Tank[]>("tanks.json");
            var units = ReadJsonFile<Unit[]>("units.json");
            var factories = ReadJsonFile<Factory[]>("factories.json");

            // Остальная часть программы...
        }


        public static T ReadJsonFile<T>(string filePath)
        {
            using StreamReader reader = new StreamReader(filePath);
            string jsonString = reader.ReadToEnd();
            return JsonSerializer.Deserialize<T>(jsonString);
        }

        public static int GetTotalVolumeUsingLINQ(Tank[] tanks)
        {
            return tanks.Sum(tank => tank.Volume);
        }

        public static Tank FindTankByNameUsingLINQ(Tank[] tanks, string name)
        {
            return tanks.FirstOrDefault(tank => tank.Name == name);
        }

        public static void SerializeToJson<T>(T obj, string filePath)
        {
            string jsonString = JsonSerializer.Serialize(obj);
            File.WriteAllText(filePath, jsonString);
        }
    }
}