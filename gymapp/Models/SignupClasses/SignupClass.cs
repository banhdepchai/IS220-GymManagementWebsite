using App.Models.Classes;

namespace App.Models.SignupClasses
{
    public class SignupClass
    {
        public int ClassId { get; set; }

        public Class Class { get; set; }

        public string UserId { get; set; }

        public AppUser User { get; set; }

        public DateTime SignupDate { get; set; }
    }
}