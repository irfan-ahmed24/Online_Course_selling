using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_project.Models
{
    public class TeacherWallet
    {
        [Key]
        public int Id { get; set; }

        public int TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public virtual User? Teacher { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentBalance { get; set; } = 0.00m; // শিক্ষকের বর্তমান ব্যালেন্স বা টোটাল আর্নিংস

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalWithdrawn { get; set; } = 0.00m; // শিক্ষক কত টাকা তুলে নিয়েছেন
    }
}