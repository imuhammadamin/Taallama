using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Taallama.Domain.Commons;

namespace Taallama.Domain.Entities
{
    public class Course : BaseEntity, IAuditable
    {
        public Course()
        {
            Videos = new List<Video>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Thumbnail { get; set; }

        [JsonIgnore]
        public Guid CourseOwnerId { get; set; }

        [ForeignKey("CourseOwnerId")]
        public User CourseOwner { get; set; }

        public int? CountOfVideos => Videos?.Count;

        public ICollection<Video> Videos { get; set; }
    }
}