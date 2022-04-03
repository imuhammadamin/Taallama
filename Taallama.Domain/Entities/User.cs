using System;
using Taallama.Domain.Commons;
using Taallama.Domain.Enums;

namespace Taallama.Domain.Entities
{
    public class User : Login, IAuditable
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public UserRole Role { get; set; } = 0;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public State State { get; set; }
    }
}