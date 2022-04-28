using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

class ApplicationTask4
{
    const string fileName = $"e:/Students.dat";

    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Student(string name, string group, DateTime dateofbirth)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateofbirth;
        }
    }

    static void Main()
    {
        ReadValues();
       // DisplayValues();
    }

    public static void ReadValues()
    {
        string File1= $"e:/Test.txt";
        if (File.Exists(fileName))
        {
            List<Student> students = new List<Student>();

            //var str =File.ReadAllText(fileName);
            //string str1 = "";
            //str1 = str.Substring(319);
            //StreamWriter sw = new StreamWriter(File1);
            ////Write a line of text
            //sw.WriteLine(str1);            
            //sw.Close();

            ////Console.WriteLine(str);

            //using (StreamReader reader = new StreamReader(File1)) //  fileName
            ////using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open))) ;
            //{
            //    int i = 0;
            //    int Len = 39;
            //    str1 = "";
            //    char[] str2 = new char[Len]; ;
            //    while (!reader.EndOfStream)
            //    {

            //        reader.ReadBlock(str2, i, Len);
            //        Console.WriteLine(str2.ToString());
            //        //var newStudent = (Student) str2.ToString();
            //        i += Len;
            //    }



            FileStream fs;
            try
            {
                using (fs = new FileStream(fileName, FileMode.Open))  //
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    //var newStudent = (Student) formatter.Deserialize(fs);
                    students = (List<Student>)formatter.Deserialize(fs);
                    // students.Add(newStudent);
                }
                fs.Close();

                //    Console.WriteLine($"Имя: {newPerson.Name} Группа: {newPerson.Group} Дата рождения: {newPerson.DateOfBirth}");
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                //throw;
            }
        
        }

    }
}
