using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Taallama.Domain.Commons;

namespace Taallama.Domain.Entities
{
    public class Course : BaseEntity, IAuditable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public Guid CourseOwnerId { get; set; }

        [ForeignKey(nameof(CourseOwnerId))]
        public User CourseOwner { get; set; }

        public int? CountOfVideos => Videos?.Count;
        public virtual ICollection<Video> Videos { get; set; }
    }
}