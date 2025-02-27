﻿using System.ComponentModel.DataAnnotations;

namespace SCADA.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Username is a required field!")]
        public string Username { get; set; } = "";
        [Required(ErrorMessage = "Password is a required field!")]
        public string Password { get; set; } = "";
    }
}
