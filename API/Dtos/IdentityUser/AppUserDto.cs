﻿using System.ComponentModel.DataAnnotations;

namespace API.Dtos.IdentityUser
{
    public class AppUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }


    }
}
