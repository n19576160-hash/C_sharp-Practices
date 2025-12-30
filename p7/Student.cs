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

// Method: Short info
        public string GetStudentInfo()
        {
            return $"[{StudentId}] {Name} - {ClassName}";
        }

        // Method: Get detailed info as string
        public string GetDetailedInfo()
        {
            return $"Student ID: {StudentId}\nName: {Name}\nClass: {ClassName}";
        }
    }
}
