using System.ComponentModel.DataAnnotations;
using BataSCADA.Models;

namespace BataSCADA.DTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Name is a required field!")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Surname is a required field!")]
        public string? Surname { get; set; }
        [Required(ErrorMessage = "Username is a required field!")]
        public string? Username { get; set; }
        [StringLength(256,MinimumLength = 8,ErrorMessage = "Password has to be between 8 and 256 characters long!")]
        public string? Password { get; set; }

        public UserDTO(User user)
        {
            Name = user.Name;
            Surname = user.Surname;
            Username = user.Username;
            Password = "";
        }

        public UserDTO() {}
    }
}
