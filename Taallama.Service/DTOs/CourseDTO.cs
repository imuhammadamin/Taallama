using Microsoft.AspNetCore.Http;

namespace Taallama.Service.DTOs
{
    public class CourseDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public System.Guid CourseOwnerId { get; set; }

        public IFormFile Thumbnail { get; set; }
    }
}