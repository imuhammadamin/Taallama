using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Taallama.Domain.Commons;
using Taallama.Domain.Enums;

namespace Taallama.Domain.Entities
{
    public class Course : IAuditable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        
        public Guid CourseOwnerId { get; set; }
        
        [ForeignKey("CourseOwnerId")]
        public User CourseOwner { get; set; }

        public int CountOfVideos => Videos.Count;

        public ICollection<Video> Videos { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        
        public State State { get; set; }
    }
}