using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class UserModel
    {
        public int uidd { get; set; }
        public string name { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Number exceeded")]
        public string phoneNo { get; set;}
        public string city { get; set; }
        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Email not valid")]
        public string emailid { get; set; }

        public string pass1 { get; set; }
        [Required]
        [Compare("pass1", ErrorMessage = "Password Didn't match")]
        public string pass2 { get; set; }
    }
    public class login
    {
        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Email not valid")]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }

}
