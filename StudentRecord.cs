using Proj_SampleConApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
                               //FileSystem As CSV For Student Records
namespace Assessments.Assignments
{
    interface IStudentRecord
    {
        void AddStudent();
        void DisplayStudent();
    }
    class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int StudentClass { get; set; }
        public int TotalScore { get; set; }

    }
    class StudentRecord : IStudentRecord
    {
        public static List<Student> students = new List<Student>();
        const string filename = "studentrecords.csv";
        public void AddStudent()
        {
            int Sid = Utilities.GetInteger("enter the id");
            string Sname = Utilities.GetString("enter the name");
            int Sclass = Utilities.GetInteger("enter the class");
            int Sscore = Utilities.GetInteger("enter the score");
            
            Student newStudent = new Student { StudentId=Sid,StudentName = Sname, StudentClass = Sclass, TotalScore= Sscore };
            students.Add(newStudent);
            Console.WriteLine("Student added successfully!");
            writestudentToFile(newStudent);
        }
        public void DisplayStudent()
        {
            Console.WriteLine("Student Records:");
            ReadfromStudentFile(filename);

           
        }
        public static void ReadfromStudentFile(string filename)
        {
             var content = File.ReadAllText(filename);
             Console.WriteLine(content);
        }
        
        private static void writestudentToFile(Student s)
        {

            var line = $"{s.StudentId},{s.StudentName},{s.StudentClass},{s.TotalScore}\n";
            File.AppendAllText(filename, line);
            Console.WriteLine("Data Saved to the file");

        }
        

        private static void bulkInsertRecords(List<Student> students)
        {
            foreach (var student in students)
            {
                writestudentToFile(student);
            }
        }
        private static List<Student> ReadAllRecords(string filename)
        {
          List<Student> students = new List<Student>();
          string[] lines = File.ReadAllLines(filename);
            
            foreach (string line in lines)
            {
                string[] words = line.Split(',');
                Student s = new Student
                {
                    StudentId = int.Parse(words[0]),
                    StudentName = words[1],
                    StudentClass = int.Parse(words[2]),
                    TotalScore = int.Parse(words[3])
                };

                students.Add(s);
            }
            return students;
        }

        public static void deleteRecordFromFile()
        {
            
            int id = Utilities.GetInteger("Enter the id to delete");
            List<Student> students=ReadAllRecords(filename);
           
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].StudentId == id)
                {
                    students.RemoveAt(i);
                    break;
                }
            }
            File.Delete(filename);
            bulkInsertRecords(students);

            Console.WriteLine("Student Deleted");

        }
    }
    class Program
    {
        const string filename = "studentrecords.csv";
        static void Main(string[] args)
        {
            StudentRecord recordSystem = new StudentRecord();
            string file = @"C:\Users\thrupthig\source\repos\Assessments\Assignments\menuStudent.txt";
            string[] lines = File.ReadAllLines(file);
            

            while (true)
            {
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }

                Console.Write("Select an option: ");
                int choice = Utilities.GetInteger("enter the choice in number");

                switch (choice)
                {
                    case 1:
                        recordSystem.AddStudent();
                        break;

                    case 2:
                        recordSystem.DisplayStudent();
                        break;
                        
                    case 3:
                        StudentRecord.deleteRecordFromFile();
                       break;



                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
           
        }
      
    }
    
}
