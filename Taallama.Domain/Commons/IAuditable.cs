using System;
using Taallama.Domain.Enums;

namespace Taallama.Domain.Commons
{
    public interface IAuditable
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public State State { get; set; }
    }
}