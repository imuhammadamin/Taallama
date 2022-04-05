using System;
using System.ComponentModel.DataAnnotations;
using Taallama.Domain.Commons;
using Taallama.Domain.Enums;

namespace Taallama.Domain.Entities
{
    public class User : BaseEntity, IAuditable
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [MaxLength(64), MinLength(11)]
        public string Email { get; set; }

        [MaxLength(32), MinLength(6)]
        public string Password { get; set; }

        public DateTime BirthDate { get; set; }

        [MaxLength(13), MinLength(12)]
        public string PhoneNumber { get; set; }

        public string Image { get; set; }

        public UserRole Role { get; set; }
    }
}