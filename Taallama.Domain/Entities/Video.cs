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

        public string CourseName => Course?.Title;
        public string Owner => $"{Course?.CourseOwner?.FirstName} {Course?.CourseOwner?.LastName}";

        public Guid CourseId { get; set; }

        [NotMapped, JsonIgnore]
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }
    }
}