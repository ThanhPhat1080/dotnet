﻿namespace trainingEF.Models.DTOs;

public class UserLoginRequestDto
{
    public UserLoginRequestDto()
    {
    }

    public string Email { get; set; }
    public string Password { get; set; }
}
