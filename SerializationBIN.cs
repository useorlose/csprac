using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    [Serializable]
    class Program
    {
        public string Model { get; set; }
        public string Company { get; set; }
        public string Price { get; set; }
        public string Acessories { get; set; }
        public override string ToString()
        {
            return string.Format($"Model: {Model}\nCompany: {Company}\nPrice: {Price}\nAccesories: {Acessories}");
        }
    }
    class SerializationBIN
    {
        static void Main(string[] args)
        {
            BinarySerialization();
        }
        private static void BinarySerialization()
        {
            Console.Write("Serialize or Deserialize? (S/D): ");
            string choice = Console.ReadLine();
            if (choice.ToLower() == "s")
                serialize();
            else
                deserialize();
        }

        private static void serialize()
        {
            Program s = new Program { Model = "HonorPlay", Company = "Huwaei", Price = "20000", Acessories = "Headphones" };
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream("Demo.bin", FileMode.OpenOrCreate, FileAccess.Write);
            bf.Serialize(fs, s);
            fs.Close();
        }

        private static void deserialize()
        {
            FileStream fs = new FileStream("Demo.bin", FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            Program s = bf.Deserialize(fs) as Program;
            Console.WriteLine(s.Model);
            Console.WriteLine(s.Company);
            Console.WriteLine(s.Price);
            Console.WriteLine(s.Acessories);
            fs.Close();
        }
    }
}