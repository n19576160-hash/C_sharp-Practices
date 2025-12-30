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

    }

    // Enum for submission results
    public enum SubmissionResult
    {
        Success,
        AlreadySubmitted,
        InvalidStudent
    }

    // Enum for grading results
    public enum GradingResult
    {
        Success,
        InvalidGrade,
        SubmissionNotFound
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

        // Method 1: Student submitted homework 
        public SubmissionResult SubmitHomework(string studentId, string studentName)
        {
            // Check whether this student has already submitted 
            foreach (var sub in submissions)
            {
                if (sub.StudentId == studentId)
                {
                    
                    return SubmissionResult.AlreadySubmitted;
                }
            }

            DateTime submitDate = DateTime.Now;
            Submission newSubmission = new Submission(studentId, studentName, submitDate);
            submissions.Add(newSubmission);

            return SubmissionResult.Success;
        }

        // Method 2: Check if submission is late
        public bool IsSubmissionLate(string studentId)
        {
            foreach (var sub in submissions)
            {
                if (sub.StudentId == studentId)
                {
                    return sub.SubmitDate > DueDate;
                }
            }
            return false;
        }
        
        // Method 3: Grading
        public GradingResult GradeSubmission(string studentId, double grade, string comments = "")
        {
            if (grade < 0 || grade > TotalMarks)
            {
                return GradingResult.InvalidGrade;;
            }

            // Student submission finding
            foreach (var sub in submissions)
            {
                if (sub.StudentId == studentId)
                {
                    sub.Grade = grade;
                    sub.IsGraded = true;
                    sub.Comments = comments;
                    
                    return GradingResult.Success;
                }
            }

            return GradingResult.SubmissionNotFound;
        }

        // Method 4: show all submissions
        public List<Submission> GetAllSubmissions()
        {
            return new List<Submission>(submissions);  // Return copy
        }

        // Method 5:Show Pending students (who haven't submitted till now)
        public List<string> GetPendingStudents(List<string> allStudentIds)
        {
            List<string> pendingStudents = new List<string>();

           

            foreach (string studentId in allStudentIds)
            {
                bool hasSubmitted = false;
                
                foreach (var sub in submissions)
                {
                    if (sub.StudentId == studentId)
                    {
                        hasSubmitted = true;
                        break;
                    }
                }

                if (!hasSubmitted)
                {
                    pendingStudents.Add(studentId);
                }
            }

            return pendingStudents;
        }

        

        // Method 6: Calculate average grade
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

        // Method 7: Check if overdue
        public bool IsOverdue()
        {
            return DateTime.Now > DueDate;
        }

        // Method 8: Days remaining
        public int DaysRemaining()
        {
            TimeSpan remaining = DueDate - DateTime.Now;
            return remaining.Days;
        }

         // Method 9: Get submission by student ID
        public Submission GetSubmissionByStudentId(string studentId)
        {
            foreach (var sub in submissions)
            {
                if (sub.StudentId == studentId)
                {
                    return sub;
                }
            }
            return null;
        }

        // Method 10: Check if student has submitted
        public bool HasStudentSubmitted(string studentId)
        {
            foreach (var sub in submissions)
            {
                if (sub.StudentId == studentId)
                {
                    return true;
                }
            }
            return false;
        }

        // Method 11: Get submission rate (percentage)
        public double GetSubmissionRate(int totalStudents)
        {
            if (totalStudents == 0)
            {
                return 0;
            }
            return (submissions.Count * 100.0) / totalStudents;
        }
    }
}
