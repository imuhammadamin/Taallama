using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using Taallama.Domain.Enums;

namespace Taallama.Service.DTOs
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        [MaxLength(13), MinLength(12)]
        public string PhoneNumber { get; set; }
        public IFormFile ProfileImage { get; set; }
        public UserRole Role { get; set; } = 0;
    }
}