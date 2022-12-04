using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace App.Models.Classes
{
    [Table("Instructors")]
    public class Instructor
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên huân luyện viên")]
        [Required(ErrorMessage = "Tên huấn luyện viên không được để trống")]
        public string Name { get; set; }

        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Giới tính không được để trống")]
        public string Gender { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [Display(Name = "Chuyên môn")]
        public string Expertise { get; set; }

        [Display(Name = "Mức lương")]
        public decimal Salary { get; set; }

        private List<Class>? Classes { get; set; }
    }
}