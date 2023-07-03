using System.ComponentModel.DataAnnotations.Schema;
using BataSCADA.DTOs;

namespace BataSCADA.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsLoggedIn { get; set; }

        public User(UserDTO user)
        {
            Name = user.Name;
            Surname = user.Surname;
            Username = user.Username;
            Password = user.Password;
            IsLoggedIn = false;
        }

        public User() {}
    }
}
