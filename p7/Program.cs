using System;
using System.Collections.Generic;

namespace HomeworkTrackingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("School Homework Tracking System ");

            // Step 1: Students List
            Console.WriteLine("Creating Students list");

            List<Student> students = new List<Student>();
            students.Add(new Student("S001", "Imtiaz Rahman", "Class 10"));
            students.Add(new Student("S002", "Faria Islam", "Class 10"));
            students.Add(new Student("S003", "Sakib Ahmed", "Class 10"));
            students.Add(new Student("S004", "Nusrat Jahan", "Class 10"));
            students.Add(new Student("S005", "Rakib Hasan", "Class 10"));
            students.Add(new Student("S006", "Tasnim Akter", "Class 10"));

            Console.WriteLine($"Total {students.Count} students registered!\n");

            DisplayAllStudents(students);

            // Step 2: Creating Homeworks list

            List<Homework> homeworks = new List<Homework>();

            // Homework 1: Math
            DateTime mathDueDate = DateTime.Now.AddDays(7);
            Homework hw1 = new Homework(
                "HW001", 
                "Algebra Problems", 
                "Mathematics",
                "Solve all problems from Chapter 5 (Equations)",
                mathDueDate,
                100
            );
            homeworks.Add(hw1);
            hw1.AssignToStudents(students.Count);

            // Homework 2: English
            DateTime englishDueDate = DateTime.Now.AddDays(5);
            Homework hw2 = new Homework(
                "HW002",
                "Essay Writing",
                "English",
                "Write an essay on 'The Importance of Education' (500 words)",
                englishDueDate,
                100
            );
            homeworks.Add(hw2);
            hw2.AssignToStudents(students.Count);

            // Homework 3: Science
            DateTime scienceDueDate = DateTime.Now.AddDays(10);
            Homework hw3 = new Homework(
                "HW003",
                "Physics Lab Report",
                "Science",
                "Submit lab report on Newton's Laws of Motion",
                scienceDueDate,
                100
            );
            homeworks.Add(hw3);
            hw3.AssignToStudents(students.Count);

            Console.WriteLine($"\n Total {homeworks.Count} homework assigned");

            // Step 3: Students submitted homework 

            // Math Homework submissions
            Console.WriteLine("--- Math Homework Submissions ---");
            hw1.SubmitHomework("S001", "Imtiaz Rahman");
            hw1.SubmitHomework("S003", "Sakib Ahmed");
            hw1.SubmitHomework("S005", "Rakib Hasan");
            hw1.SubmitHomework("S006", "Tasnim Akter");

            // English Homework submissions
            Console.WriteLine("\n--- English Homework Submissions ---");
            hw2.SubmitHomework("S001", "Imtiaz Rahman");
            hw2.SubmitHomework("S002", "Faria Islam");
            hw2.SubmitHomework("S004", "Nusrat Jahan");
            hw2.SubmitHomework("S005", "Rakib Hasan");
            hw2.SubmitHomework("S006", "Tasnim Akter");

            // Science Homework submissions
            Console.WriteLine("\n--- Science Homework Submissions ---");
            hw3.SubmitHomework("S001", "Imtiaz Rahman");
            hw3.SubmitHomework("S002", "Faria Islam");
            hw3.SubmitHomework("S003", "Sakib Ahmed");

            // Duplicate submission attempt
            Console.WriteLine("\n--- Duplicate Submission Test ---");
            hw1.SubmitHomework("S001", "Imtiaz Rahman");

            // Step 4: Teacher is grading...

            // Grade Math homework
            Console.WriteLine("--- Grading Math Homework ---");
            hw1.GradeSubmission("S001", 95, "Excellent work!");
            hw1.GradeSubmission("S003", 87, "Good effort, minor mistakes");
            hw1.GradeSubmission("S005", 78, "Need to show more steps");
            hw1.GradeSubmission("S006", 92, "Very well done!");

            // Grade English homework
            Console.WriteLine("\n--- Grading English Homework ---");
            hw2.GradeSubmission("S001", 88, "Well structured essay");
            hw2.GradeSubmission("S002", 91, "Creative and insightful");
            hw2.GradeSubmission("S004", 85, "Good content, improve grammar");
            hw2.GradeSubmission("S005", 82, "Needs more examples");

            // Grade Science homework (partial)
            Console.WriteLine("\n--- Grading Science Homework ---");
            hw3.GradeSubmission("S001", 94, "Excellent lab report");
            hw3.GradeSubmission("S002", 89, "Good observations");

            // Step 5: Showing all submissions 

            hw1.ShowAllSubmissions();
            hw2.ShowAllSubmissions();
            hw3.ShowAllSubmissions();

            // Step 6: Pending submissions showing

            List<string> studentIds = new List<string>();
            List<string> studentNames = new List<string>();
            
            foreach (var student in students)
            {
                studentIds.Add(student.StudentId);
                studentNames.Add(student.Name);
            }

            hw1.ShowPendingStudents(studentIds, studentNames);
            Console.WriteLine();
            hw2.ShowPendingStudents(studentIds, studentNames);
            Console.WriteLine();
            hw3.ShowPendingStudents(studentIds, studentNames);

            // Step 7: Statistics and Analysis

            DisplayHomeworkStatistics(homeworks);

            // Step 8: performance of Individual student 

            ShowStudentPerformance("S001", "Imtiaz Rahman", homeworks);
            ShowStudentPerformance("S002", "Faria Islam", homeworks);

            // Step ৯: Homework overview

            for (int i = 0; i < homeworks.Count; i++)
            {
                Console.WriteLine($"{i + 1}.");
                homeworks[i].DisplayHomeworkInfo();
                Console.WriteLine();
            }

            
            
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        // Helper Method 1: Showing all students 
        static void DisplayAllStudents(List<Student> students)
        {
            Console.WriteLine(" Registered Students");


            for (int i = 0; i < students.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                students[i].DisplayShortInfo();
            }
        }

        // Helper Method 2: Homework statistics
        static void DisplayHomeworkStatistics(List<Homework> homeworks)
        {
            foreach (var hw in homeworks)
            {
                Console.WriteLine($"{hw.Title} ({hw.Subject})");
                Console.WriteLine($"   Submissions: {hw.SubmissionCount}");
                Console.WriteLine($"   Graded: {hw.GradedCount}/{hw.SubmissionCount}");
                
                double avg = hw.CalculateAverageGrade();
                if (hw.GradedCount > 0)
                {
                    Console.WriteLine($"   Average Grade: {avg:F2}/100");
                }
                else
                {
                    Console.WriteLine($"   Average Grade: N/A (not yet graded)");
                }

                int daysRemaining = hw.DaysRemaining();
                if (hw.IsOverdue())
                {
                    Console.WriteLine($"   Status: OVERDUE");
                }
                else
                {
                    Console.WriteLine($"   Days Remaining: {daysRemaining} days");
                }
                Console.WriteLine();
            }
        }

        // Helper Method 3: Individual student performance
        static void ShowStudentPerformance(string studentId, string studentName, 
                                          List<Homework> homeworks)
        {
            Console.WriteLine($"Student: {studentName} ({studentId})");

            int totalSubmitted = 0;
            int totalGraded = 0;
            double totalGrade = 0;

            foreach (var hw in homeworks)
            {
                Console.WriteLine($" {hw.Title}: Check submissions");
            }

            Console.WriteLine($"  Summary: {totalSubmitted} submitted, {totalGraded} graded");
            if (totalGraded > 0)
            {
                Console.WriteLine($"  Average: {totalGrade / totalGraded:F2}/100");
            }
            Console.WriteLine();
        }
    }
}