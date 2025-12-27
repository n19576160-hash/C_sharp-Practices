using System;
using System.Collections.Generic;

namespace HomeworkTrackingSystem
{
    // Submission class - to track the submission of a Student
    public class Submission
    {
        public string StudentId { get; private set; }
        public string StudentName { get; private set; }
        public DateTime SubmitDate { get; private set; }
        public double Grade { get; set; }
        public bool IsGraded { get; set; }
        public string Comments { get; set; }

        public Submission(string studentId, string studentName, DateTime submitDate)
        {
            StudentId = studentId;
            StudentName = studentName;
            SubmitDate = submitDate;
            Grade = 0;
            IsGraded = false;
            Comments = "";
        }

        public void DisplaySubmission()
        {
            string gradeStatus = IsGraded ? $"{Grade}/100" : "Not Graded";
            string lateStatus = "";
            
            Console.WriteLine($"       {StudentName} ({StudentId})");
            Console.WriteLine($"       Submit: {SubmitDate:dd MMM yyyy}");
            Console.WriteLine($"       Grade: {gradeStatus}");
            
            if (!string.IsNullOrEmpty(Comments))
            {
                Console.WriteLine($"       Comments: {Comments}");
            }
        }
    }

    // Homework class - homework management
    public class Homework
    {
        // Properties
        public string HomeworkId{ get; private set; }   
        public string Title{ get; private set; }
        public string Description{ get; private set; }
        public string Subject{ get; private set; }
        public DateTime AssignDate{ get; private set; }
        public DateTime DueDate{ get; private set; }
        public int TotalMarks{ get; private set; }
        
        // Submissions tracking
        private List<Submission> submissions;

        // Constructor
        public Homework(string homeworkId, string title, string subject, 
                       string description, DateTime DueDate, int totalMarks = 100)
        {
            this.HomeworkId = homeworkId;
            this.Title = title;
            this.Subject = subject;
            this.Description = description;
            this.AssignDate = DateTime.Now;
            this.DueDate = DueDate;
            this.TotalMarks = totalMarks;
            this.submissions = new List<Submission>();
        }


        public int SubmissionCount
        {
            get { return submissions.Count; }
        }

        
        
        public int GradedCount
        {
            get 
            { 
                int count = 0;
                foreach (var sub in submissions)
                {
                    if (sub.IsGraded) count++;
                }
                return count;
            }
        }

        // Method 1: assigning homework to Students
        public void AssignToStudents(int studentCount)
        {
            Console.WriteLine($"\nHomework '{Title}' assigned");
            Console.WriteLine($"  Subject: {Subject}");
            Console.WriteLine($"  Assign Date: {AssignDate:dd MMM yyyy}");
            Console.WriteLine($"  Due Date: {DueDate:dd MMM yyyy}");
            Console.WriteLine($"  Total Students: {studentCount}");
            Console.WriteLine($"  Total Marks: {TotalMarks}");
        }

        // Method 2: Student submitted homework 
        public bool SubmitHomework(string studentId, string studentName)
        {
            // Check whether this student has already submitted 
            foreach (var sub in submissions)
            {
                if (sub.StudentId == studentId)
                {
                    Console.WriteLine($"{studentName} has submitted already !");
                    return false;
                }
            }

            DateTime submitDate = DateTime.Now;
            Submission newSubmission = new Submission(studentId, studentName, submitDate);
            submissions.Add(newSubmission);

            // Check whether it is a late submission 
            if (submitDate > DueDate)
            {
                Console.WriteLine($"{studentName}'s submission LATE! (Due: {DueDate:dd MMM yyyy})");
            }
            else
            {
                Console.WriteLine($"{studentName} has successfully submitted the  homework !");
            }

            return true;
        }

        // Method 3: Grading
        public bool GradeSubmission(string studentId, double grade, string comments = "")
        {
            if (grade < 0 || grade > TotalMarks)
            {
                Console.WriteLine($"Invalid grade! Grade must be between 0 and {TotalMarks}");
                return false;
            }

            // Student submission finding
            foreach (var sub in submissions)
            {
                if (sub.StudentId == studentId)
                {
                    sub.Grade = grade;
                    sub.IsGraded = true;
                    sub.Comments = comments;
                    
                    Console.WriteLine($" {sub.StudentName}'s homework graded: {grade}/{TotalMarks}");
                    return true;
                }
            }

            Console.WriteLine($" Student ID {studentId} has no submission found!");
            return false;
        }

        // Method 4: show all submissions
        public void ShowAllSubmissions()
        {
            Console.WriteLine($"Homework: {Title}");

            if (submissions.Count == 0)
            {
                Console.WriteLine(" no submissions found .\n");
                return;
            }

            Console.WriteLine($"  Total Submissions: {submissions.Count}");
            Console.WriteLine($"  Graded: {GradedCount}/{submissions.Count}\n");

            foreach (var sub in submissions)
            {
                sub.DisplaySubmission();
                Console.WriteLine();
            }
        }

        // Method 5:Show Pending students (who haven't submitted till now)
        public void ShowPendingStudents(List<string> allStudentIds, List<string> allStudentNames)
        {
            Console.WriteLine($"\nPending Submissions for: {Title}");

            int pendingCount = 0;

            for (int i = 0; i < allStudentIds.Count; i++)
            {
                bool hasSubmitted = false;
                
                foreach (var sub in submissions)
                {
                    if (sub.StudentId == allStudentIds[i])
                    {
                        hasSubmitted = true;
                        break;
                    }
                }

                if (!hasSubmitted)
                {
                    pendingCount++;
                    Console.WriteLine($"{allStudentNames[i]} ({allStudentIds[i]})");
                }
            }

            if (pendingCount == 0)
            {
                Console.WriteLine(" Everyone submitted!");
            }
            else
            {
                Console.WriteLine($"\nTotal Pending: {pendingCount} students");
            }
        }

        // Method 6: Homework summary
        public void DisplayHomeworkInfo()
        {
            Console.WriteLine($"  [{HomeworkId}] {Title} ({Subject})");
            Console.WriteLine($"       Description: {Description}");
            Console.WriteLine($"       Assigned: {AssignDate:dd MMM yyyy} | Due: {DueDate :dd MMM yyyy}");
            Console.WriteLine($"       Total Marks: {TotalMarks}");
            Console.WriteLine($"       Submissions: {submissions.Count} | Graded: {GradedCount}");
        }

        // Method 7: Calculate average grade
        public double CalculateAverageGrade()
        {
            if (GradedCount == 0)
            {
                return 0;
            }

            double totalGrade = 0;
            foreach (var sub in submissions)
            {
                if (sub.IsGraded)
                {
                    totalGrade += sub.Grade;
                }
            }

            return totalGrade / GradedCount;
        }

        // Method 8: Check if overdue
        public bool IsOverdue()
        {
            return DateTime.Now > DueDate;
        }

        // Method 9: Days remaining
        public int DaysRemaining()
        {
            TimeSpan remaining = DueDate - DateTime.Now;
            return remaining.Days;
        }
    }
}