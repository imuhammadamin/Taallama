using System;
using Taallama.Domain.Enums;

namespace Taallama.Domain.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public State State { get; set; }

        public void Update()
        {
            UpdatedAt = DateTime.Now;
            State = State.Updated;
        }

        public void Create()
        {
            CreatedAt = DateTime.Now;
            State = State.Created;
        }

        public void Delete()
        {
            State = State.Deleted;
        }

    }
}