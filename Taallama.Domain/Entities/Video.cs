using System;
using System.ComponentModel.DataAnnotations.Schema;
using Taallama.Domain.Commons;
using Taallama.Domain.Enums;

namespace Taallama.Domain.Entities
{
    public class Video : IAuditable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        
        public Guid CourseId { get; set; }
        
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        
        public State State { get; set; }
    }
}