using System;
using System.ComponentModel.DataAnnotations;

namespace trainingEF.Models.DTOs
{
	public class UserRegistrationRequestDto
	{
		public UserRegistrationRequestDto()
		{
		}

		[Required]
		public string Name { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
        public string Password { get; set; }
	}
}

