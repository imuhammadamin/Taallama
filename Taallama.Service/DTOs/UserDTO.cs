using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using Taallama.Domain.Enums;

namespace Taallama.Service.DTOs
{
    public class UserDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MaxLength(13), MinLength(12)]
        public string PhoneNumber { get; set; }
        
        public IFormFile ProfileImage { get; set; }

        [Required]
        public UserRole Role { get; set; } = 0;
    }
}