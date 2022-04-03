using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Taallama.Service.DTOs
{
    public class CourseDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public System.Guid CourseOwnerId { get; set; }

        public IFormFile Thumbnail { get; set; }
    }
}