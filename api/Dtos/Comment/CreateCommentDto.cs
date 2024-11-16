using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace api.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters")]
        [MaxLength(280, ErrorMessage = "Title must be less than 280 characters")]
        public string Title { get; set; } = String.Empty;
        [Required]
        [MinLength(3, ErrorMessage = "Content must be at least 3 characters")]
        [MaxLength(280, ErrorMessage = "Content must be less than 280 characters")]
        public string Content { get; set; } = String.Empty;
    }
}