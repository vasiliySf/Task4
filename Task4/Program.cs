//Исходный файл Students.dat при открытии дает ошибку, поэтому создал свой, ну а дальше все по плану задачи 

using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using FinalTask;

class Task4
{
    const string fileName = $"e:/Students.dat";

    public static void CreateTestFile(string path)
    {
        List<Student> students = new List<Student>();
        //Student[] student = new Student[6];
        students.Add(new Student("Василий", "G - 151", new DateTime(2004, 3, 11, 0, 0, 0)));
        students.Add(new Student("Петр", "G - 150", new DateTime(2004, 5, 22, 0, 0, 0)));
        students.Add(new Student("Евгений", "G - 152", new DateTime(2004, 1, 12, 0, 0, 0)));
        students.Add(new Student("Анна", "G - 151", new DateTime(2004, 11, 15, 0, 0, 0)));
        students.Add(new Student("Эдуард", "G - 150", new DateTime(2004, 9, 6, 0, 0, 0)));
        students.Add(new Student("Полина", "G - 151", new DateTime(2004, 12, 10, 0, 0, 0)));

        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {

                formatter.Serialize(fs, students);

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка сериализации: {0}", ex.Message);
        }


    }


    static void Main()
    {

        if (!File.Exists(fileName))
            CreateTestFile(fileName);

        ReadValues();

    }

    public static void ReadValues()
    {
        string path = @"e:\Students";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        List<string> groups = new List<string>();

        if (File.Exists(fileName))
        {
            List<Student> students = new List<Student>();

            FileStream fs;
            try
            {
                using (fs = new FileStream(fileName, FileMode.Open))  //
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    students = (List<Student>)formatter.Deserialize(fs);

                }
                fs.Close();

            }
            catch (SerializationException e)
            {
                Console.WriteLine("Ошибка десериализации, причина: " + e.Message);

            }
            foreach (Student s in students)
            {
                if (!groups.Contains(s.Group))
                    groups.Add(s.Group);
            }
            foreach (string g in groups)
            {
                var groupstudents = students.Where(s => s.Group == g);

                string groupfile = Path.Combine(path, g + ".txt");
                if (File.Exists(groupfile))
                    File.Delete(groupfile); //чистим мусор от прежних запусков

                string strggroup = "";



                StreamWriter w;
                try
                {
                    var textfile = new FileInfo(groupfile);
                    using (w = textfile.AppendText())
                    {
                        foreach (Student st in groupstudents)
                        {
                            w.WriteLine(st.Name + " " + st.DateOfBirth.ToString("D"));
                            //   strggroup +=  + "/";
                        }
                        w.Close();
                    }

                }
                catch (Exception ex)
                { Console.WriteLine("Не получается добавить запись в файл {0}: {1}", groupfile, ex.Message); }


            }

        }
    }
}


