using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_project.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }

        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual User? Student { get; set; }

        public int TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public virtual User? Teacher { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; } // মোট পেমেন্ট

        [Column(TypeName = "decimal(18,2)")]
        public decimal TeacherAmount { get; set; } // শিক্ষকের অংশ (৯৫%)

        [Column(TypeName = "decimal(18,2)")]
        public decimal AdminCommission { get; set; } // অ্যাডমিনের কমিশন (৫%)

        public DateTime TransactionDate { get; set; } = DateTime.Now;
    }
}