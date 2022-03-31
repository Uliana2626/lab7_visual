using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace lab7_visual.Models
{
    public class FIleInOut
    {
        public static List<Student> ReadFile(string path, int itemsNum)
        {
            List<Student> students = new List<Student>();

            StreamReader file = new StreamReader(path);
            try
            {
                while (!file.EndOfStream)
                {
                    List<Item> studentItems = new List<Item>();
                    string studentName = file.ReadLine();

                    for (int i = 0; i < itemsNum; i++)
                    {
                        string itemName = file.ReadLine();
                        string itemScore = file.ReadLine();
                        studentItems.Add(new Item(itemName, itemScore));
                    }

                    string studentAverage = file.ReadLine();

                    students.Add(new Student(studentItems, studentName, Convert.ToDouble(studentAverage)));
                }
                file.Close();
                return students;
            }
            catch
            {
                file.Close();
                return new List<Student>();
            }
        }

        public static void WriteFile(string path, List<Student> content)
        {
            File.WriteAllText(path, "");
            List<string> data = new List<string>();
            foreach (Student student in content)
            {
                data.Add(student.Fio);
                foreach (Item item in student.ItemList)
                {
                    data.Add(item.Name);
                    data.Add(item.Score);
                }
                data.Add(Convert.ToString(student.Average));
            }
            File.WriteAllLines(path, data);
        }
    }
}
