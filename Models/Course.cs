using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_project.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        [Required]
        public string Category { get; set; } = "";

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; } = "";

        public string? ThumbnailUrl { get; set; }
        public int VideoCount { get; set; }

        [Required]
        public int TeacherId { get; set; }

        // --- এই নেভিগেশন প্রপার্টিটি এখানে যুক্ত করা হলো ---
        [ForeignKey("TeacherId")]
        public User? Teacher { get; set; }
        // ----------------------------------------------

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<CourseLecture> Lectures { get; set; } = new List<CourseLecture>();

        public bool IsCourseApproved { get; set; } = false;
    }
}