using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace DateSerializationBinary
{
    [Serializable]
    class DateOfBirth1:IDeserializationCallback
    {
        public string Name { get; set; }
        public int YearOfBirth { get; set; }
      
        public int Year = DateTime.Now.Year;
        [NonSerialized]
        public int age;
        public DateOfBirth1(string n,int year)
        {
            Name = n;
            YearOfBirth = year;
        }
        public void OnDeserialization(object sender)
        {
           age = Year - YearOfBirth;
        }
        public void ShowAge()
        {
            Console.WriteLine($"{Name} age is: {age}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your name:");
            string name1 = Console.ReadLine();
            Console.WriteLine("Enter the  Year of birth :");
            int y = int.Parse(Console.ReadLine());
            Console.WriteLine($"Today's date is {DateTime.Now}");


            DateOfBirth1 db = new DateOfBirth1(name1, y);
            FileStream fs = new FileStream(@"Age.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, db);
            

            fs.Seek(0, SeekOrigin.Begin);
            DateOfBirth1 res = (DateOfBirth1)bf.Deserialize(fs);
            res.ShowAge();
        }
    }
}
