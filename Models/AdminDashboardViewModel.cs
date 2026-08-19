using My_project.Models;

namespace My_project.Models
{
    public class AdminDashboardViewModel
    {
        public int PendingCoursesCount { get; set; }
        public int PendingTeachersCount { get; set; }
        public int TotalEnrollments { get; set; } // যদি এনরোলমেন্ট টেবিল না থাকে আপাতত 0 বা ডামি রাখতে পারেন
        public decimal TotalRevenue { get; set; }  // পেমেন্ট সিস্টেম থাকলে তার হিসাব

        public List<Course> PendingCourses { get; set; } = new List<Course>();
        public List<User> PendingTeachers { get; set; } = new List<User>();
    }
}