namespace My_project.Models
{
    public class CourseLecture
    {
        public int Id { get; set; }
        public string LectureTitle { get; set; } = "";
        public string VideoUrl { get; set; } = "";

        // কোন কোর্সের আন্ডারে ভিডিওটি আছে তার আইডি
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}