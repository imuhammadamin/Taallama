using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Taallama.Domain.Commons;

namespace Taallama.Domain.Entities
{
    public class Video : BaseEntity, IAuditable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public Guid CourseId { get; set; }
        public string CourseName => Course?.Title;

        [JsonIgnore]
        [ForeignKey("CourseId")]
        public Course Course { get; set; }

    }
}