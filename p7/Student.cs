using System;

namespace HomeworkTrackingSystem
{
    public class Student
    {
        // Properties
        public string StudentId{get;private set;}
        public string Name{get;private set;}
        public string ClassName{get;private set;}

        // Constructor
        public Student(string studentId, string name, string className)
        {
            this.StudentId = studentId;
            this.Name = name;
            this.ClassName = className;
        }

        // Method: Showing Student information
        public void DisplayInfo()
        {
            Console.WriteLine($"  {Name}");
            Console.WriteLine($"   ID: {StudentId}");
            Console.WriteLine($"   Class: {ClassName}");
        }

        // Method: Short info
        public void DisplayShortInfo()
        {
            Console.WriteLine($"  [{StudentId}] {Name} - {ClassName}");
        }
    }
}