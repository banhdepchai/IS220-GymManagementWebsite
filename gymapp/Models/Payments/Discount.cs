using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Payments
{
    [Table("Discounts")]
    public class Discount
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã giảm giá không được để trống")]
        [Display(Name = "Mã giảm giá")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Phần trăm giảm giá không được để trống")]
        [Display(Name = "Phần trăm giảm giá")]
        public int Percent { get; set; }
    }
}