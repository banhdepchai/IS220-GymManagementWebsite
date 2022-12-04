using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Models.Memberships;

namespace App.Models.Payments
{
    [Table("Payments")]
    public class Payment
    {
        [Key]
        public int PaymentID { set; get; }

        public DateTime? DateCreated { set; get; }

        [Display(Name = "Khách hàng")]
        public string? UserID { set; get; }

        [Display(Name = "Khách hàng")]
        public AppUser? User { set; get; }

        [Display(Name = "Mã giảm giá")]
        public int? DiscountCode { set; get; }

        [Display(Name = "Tổng tiền")]
        public decimal TotalPrice { set; get; }

        private List<PaymentDetail>? PaymentDetails { set; get; }

        private List<SignupMembership> signupMemberships { set; get; }
    }
}