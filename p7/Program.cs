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
            DisplayHomeworkAssigned(hw1, students.Count);
            

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
            DisplayHomeworkAssigned(hw2, students.Count);
            

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
            DisplayHomeworkAssigned(hw3, students.Count);

            Console.WriteLine($"\n Total {homeworks.Count} homework assigned");

            // Step 3: Students submitted homework 

            // Math Homework submissions
            Console.WriteLine("--- Math Homework Submissions ---");
            HandleSubmission(hw1, "S001", "Imtiaz Rahman");
            HandleSubmission(hw1, "S003", "Sakib Ahmed");
            HandleSubmission(hw1, "S005", "Rakib Hasan");
            HandleSubmission(hw1, "S006", "Tasnim Akter");

             // English Homework submissions
            Console.WriteLine("\n--- English Homework Submissions ---");
            HandleSubmission(hw2, "S001", "Imtiaz Rahman");
            HandleSubmission(hw2, "S002", "Faria Islam");
            HandleSubmission(hw2, "S004", "Nusrat Jahan");
            HandleSubmission(hw2, "S005", "Rakib Hasan");
            HandleSubmission(hw2, "S006", "Tasnim Akter");

            // Science Homework submissions
            Console.WriteLine("\n--- Science Homework Submissions ---");
            HandleSubmission(hw3, "S001", "Imtiaz Rahman");
            HandleSubmission(hw3, "S002", "Faria Islam");
            HandleSubmission(hw3, "S003", "Sakib Ahmed");

            // Duplicate submission attempt
            Console.WriteLine("\n--- Duplicate Submission Test ---");
            HandleSubmission(hw1, "S001", "Imtiaz Rahman");
            

            // Step 4: Teacher is grading...

            // Grade Math homework
            Console.WriteLine("--- Grading Math Homework ---");
            HandleGrading(hw1, "S001", 95, "Excellent work!");
            HandleGrading(hw1, "S003", 87, "Good effort, minor mistakes");
            HandleGrading(hw1, "S005", 78, "Need to show more steps");
            HandleGrading(hw1, "S006", 92, "Very well done!");

            // Grade English homework
            Console.WriteLine("\n--- Grading English Homework ---");
            HandleGrading(hw2, "S001", 88, "Well structured essay");
            HandleGrading(hw2, "S002", 91, "Creative and insightful");
            HandleGrading(hw2, "S004", 85, "Good content, improve grammar");
            HandleGrading(hw2, "S005", 82, "Needs more examples");

            // Grade Science homework (partial)
            Console.WriteLine("\n--- Grading Science Homework ---");
            HandleGrading(hw3, "S001", 94, "Excellent lab report");
            HandleGrading(hw3, "S002", 89, "Good observations");


            // Invalid grading attempt
            Console.WriteLine("\n--- Invalid Grading Test ---");
            HandleGrading(hw1, "S001", 150, "Too high!");  // Invalid grade
            HandleGrading(hw1, "S999", 85, "Good");  // Non-existent submission


            // Step 5: Showing all submissions 

            DisplayAllSubmissions(hw1);
            DisplayAllSubmissions(hw2);
            DisplayAllSubmissions(hw3);

            // Step 6: Pending submissions showing

            DisplayPendingSubmissions(hw1, students);
            Console.WriteLine();
            DisplayPendingSubmissions(hw2, students);
            Console.WriteLine();
            DisplayPendingSubmissions(hw3, students);

            // Step 7: Statistics and Analysis

             DisplayHomeworkStatistics(homeworks, students.Count);

            // Step 8: performance of Individual student 

            ShowStudentPerformance(students[0], homeworks);
            ShowStudentPerformance(students[1], homeworks);

            // Step ৯: Homework overview

            DisplayHomeworkOverview(homeworks);

            
            
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }



        // Helper Method 1: Showing all students 
        static void DisplayAllStudents(List<Student> students)
        {
            Console.WriteLine(" Registered Students");


            for (int i = 0; i < students.Count; i++)
            {
                Console.Write($"{i + 1}. {students[i].GetStudentInfo()} ");
            }
        }

        // Display homework assignment
        static void DisplayHomeworkAssigned(Homework hw, int studentCount)
        {
            Console.WriteLine($"\nHomework '{hw.Title}' assigned");
            Console.WriteLine($"  Subject: {hw.Subject}");
            Console.WriteLine($"  Assign Date: {hw.AssignDate:dd MMM yyyy}");
            Console.WriteLine($"  Due Date: {hw.DueDate:dd MMM yyyy}");
            Console.WriteLine($"  Total Students: {studentCount}");
            Console.WriteLine($"  Total Marks: {hw.TotalMarks}");
        }

        // Handle submission 
        static void HandleSubmission(Homework hw, string studentId, string studentName)
        {
            SubmissionResult result = hw.SubmitHomework(studentId, studentName);

            switch (result)
            {
                case SubmissionResult.Success:
                    Console.WriteLine($"{studentName} successfully submitted homework!");
                    
                    // Check if late
                    if (hw.IsSubmissionLate(studentId))
                    {
                        Console.WriteLine($" LATE submission! (Due: {hw.DueDate:dd MMM yyyy})");
                    }
                    break;

                case SubmissionResult.AlreadySubmitted:
                    Console.WriteLine($"{studentName} already submitted this homework!");
                    break;

                case SubmissionResult.InvalidStudent:
                    Console.WriteLine($"Invalid student ID!");
                    break;
            }
        }


        // Handle grading 
        static void HandleGrading(Homework hw, string studentId, double grade, string comments)
        {
            GradingResult result = hw.GradeSubmission(studentId, grade, comments);

            switch (result)
            {
                case GradingResult.Success:
                    Submission sub = hw.GetSubmissionByStudentId(studentId);
                    Console.WriteLine($"{sub.StudentName}'s homework graded: {grade}/{hw.TotalMarks}");
                    if (!string.IsNullOrEmpty(comments))
                    {
                        Console.WriteLine($"  Comment: \"{comments}\"");
                    }
                    break;

                case GradingResult.InvalidGrade:
                    Console.WriteLine($"Invalid grade! Must be between 0 and {hw.TotalMarks}");
                    break;

                case GradingResult.SubmissionNotFound:
                    Console.WriteLine($"No submission found for student ID: {studentId}");
                    break;
            }
        }




        // Helper Method 2: Homework statistics
        static void DisplayHomeworkStatistics(List<Homework> homeworks,int totalStudents)
        {
            foreach (var hw in homeworks)
            {
                Console.WriteLine($"{hw.Title} ({hw.Subject})");
                Console.WriteLine($"   Submissions: {hw.SubmissionCount}/{totalStudents} " +
                                $"({hw.GetSubmissionRate(totalStudents):F1}%)");
                Console.WriteLine($"   Graded: {hw.GradedCount}/{hw.SubmissionCount}");
                
                double avg = hw.CalculateAverageGrade();
                if (hw.GradedCount > 0)
                {
                    Console.WriteLine($"   Average Grade:  {hw.CalculateAverageGrade():F2}/{hw.TotalMarks}");
                }
                else
                {
                    Console.WriteLine($"   Average Grade: N/A (not yet graded)");
                }

                if (hw.IsOverdue())
                {
                    Console.WriteLine($"   Status: OVERDUE");
                }
                else
                {
                    Console.WriteLine($"   Days Remaining: {hw.DaysRemaining()} days");
                }
                Console.WriteLine();
            }
        }

        // Helper Method 3: Individual student performance
        static void ShowStudentPerformance(Student student,
                                          List<Homework> homeworks)
        {
            Console.WriteLine($"Student: {student.Name} ({student.StudentId})");

            int totalSubmitted = 0;
            int totalGraded = 0;
            double totalGrade = 0;

            foreach (var hw in homeworks)
            {
               if (hw.HasStudentSubmitted(student.StudentId))
                {
                    totalSubmitted++;
                    
                    Submission sub = hw.GetSubmissionByStudentId(student.StudentId);
                    string status = sub.IsGraded 
                        ? $"{sub.Grade}/{hw.TotalMarks}" 
                        : "Pending";
                    
                    Console.WriteLine($"  • {hw.Title}: {status}");
                    
                    if (sub.IsGraded)
                    {
                        totalGraded++;
                        totalGrade += sub.Grade;
                    }
                }
                else
                {
                    Console.WriteLine($"  • {hw.Title}:  Not submitted");
                }
            }
            Console.WriteLine($"\n  Summary:");
            Console.WriteLine($"    Submitted: {totalSubmitted}/{homeworks.Count}");
            Console.WriteLine($"    Graded: {totalGraded}/{totalSubmitted}");

            // Console.WriteLine($"  Summary: {totalSubmitted} submitted, {totalGraded} graded");
            if (totalGraded > 0)
            {
                Console.WriteLine($"  Averagerade: {(totalGrade / totalGraded):F2}/100");
            }
            Console.WriteLine();
        }

        // Display homework overview
        static void DisplayHomeworkOverview(List<Homework> homeworks)
        {
            for (int i = 0; i < homeworks.Count; i++)
            {
                Homework hw = homeworks[i];
                Console.WriteLine($"{i + 1}. [{hw.HomeworkId}] {hw.Title} ({hw.Subject})");
                Console.WriteLine($"   Description: {hw.Description}");
                Console.WriteLine($"   Assigned: {hw.AssignDate:dd MMM yyyy} | Due: {hw.DueDate:dd MMM yyyy}");
                Console.WriteLine($"   Total Marks: {hw.TotalMarks}");
                Console.WriteLine($"   Submissions: {hw.SubmissionCount} | Graded: {hw.GradedCount}");
                Console.WriteLine();
            }
        }

        //Helper Methods 4:Display All submissions
        static void DisplayAllSubmissions(Homework hw){
            Console.WriteLine($"Homework : {hw.Title}");
            
            List<Submission> submissions = hw.GetAllSubmissions();

            if(submissions.Count == 0){
                Console.WriteLine("No Submissions Yet.\n");
                return;
            }

            Console.WriteLine($"Total Submissions: {hw.SubmissionCount}");
            Console.WriteLine($"  Graded: {hw.GradedCount}/{hw.SubmissionCount}\n");

            foreach (var sub in submissions)
            {
                Console.WriteLine($"    {sub.StudentName} ({sub.StudentId})");
                Console.WriteLine($"     Submit Date: {sub.SubmitDate:dd MMM yyyy}");
                
                string gradeInfo = sub.IsGraded 
                    ? $"{sub.Grade}/{hw.TotalMarks}" 
                    : "Not graded yet";
                Console.WriteLine($"       Grade: {gradeInfo}");
                
                if (sub.IsGraded && !string.IsNullOrEmpty(sub.Comments))
                {
                    Console.WriteLine($"       Comment: \"{sub.Comments}\"");
                }
                Console.WriteLine();
            }
        }

         // Display pending submissions
        static void DisplayPendingSubmissions(Homework hw, List<Student> students)
        {
            Console.WriteLine($"\n Pending for: {hw.Title}");
            

            List<string> allStudentIds = new List<string>();
            foreach (var student in students)
            {
                allStudentIds.Add(student.StudentId);
            }

            List<string> pendingIds = hw.GetPendingStudents(allStudentIds);

            if (pendingIds.Count == 0)
            {
                Console.WriteLine("  All students submitted!");
                return;
            }

            foreach (string pendingId in pendingIds)
            {
                // Find student name
                foreach (var student in students)
                {
                    if (student.StudentId == pendingId)
                    {
                        Console.WriteLine($"  {student.Name} ({student.StudentId})");
                        break;
                    }
                }
            }

            Console.WriteLine($"\nTotal Pending: {pendingIds.Count} students");
        }


    }
    
}
