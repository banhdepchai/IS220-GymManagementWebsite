using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Classes
{
    [Table("Rooms")]
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Display(Name = "Tên phòng")]
        [Required(ErrorMessage = "Tên phòng không được để trống")]
        public string RoomName { get; set; }

        [Display(Name = "Sức chứa")]
        [Required(ErrorMessage = "Sức chứa không được để trống")]
        public int Capacity { get; set; }

        private List<Class>? Classes { get; set; }
    }
}