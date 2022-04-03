using System;
using Taallama.Domain.Enums;

namespace Taallama.Domain.Commons
{
    public interface IAuditable
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public State State { get; set; }
    }
}