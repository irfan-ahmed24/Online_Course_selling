using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_project.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        // কোন ইউজারের কার্ট এটি
        public int UserId { get; set; }

        // কোন কোর্সটি যোগ করা হলো
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}