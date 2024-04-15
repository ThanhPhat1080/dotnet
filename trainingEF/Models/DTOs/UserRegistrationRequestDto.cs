using System;
using System.ComponentModel.DataAnnotations;

namespace trainingEF.Models.DTOs
{
    public class UserRegistrationRequestDto
    {
        public UserRegistrationRequestDto()
        {
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

