using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Memberships
{
    [Table("Memberships")]
    public class Membership
    {
        [Key]
        public int MembershipId { get; set; }

        [Display(Name = "Tên gói tập")]
        [Required(ErrorMessage = "Tên gói tập không được để trống")]
        public string Level { get; set; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Giá gói tập không được để trống")]
        public decimal Fee { get; set; }

        [Display(Name = "Thời hạn")]
        [Required(ErrorMessage = "Thời hạn gói tập không được để trống")]
        public int Duration { get; set; }

        [Display(Name = "Số giờ tập")]
        [Required(ErrorMessage = "Số giờ tập không được để trống")]
        public int Hours { get; set; }

        [Display(Name = "Đặc quyền")]
        [Required(ErrorMessage = "Đặc quyền không được để trống")]
        public string Bonus { get; set; }
    }
}