using System.ComponentModel.DataAnnotations;

namespace DOCOSoft.UserAPI.DTOs
{
    public class RequestUpdateDto
    {
        [Required] [MaxLength(100)] 
        public string Name { get; set; } = string.Empty;
        [Required] [MinLength(8)] 
        public string Password { get; set; } = string.Empty;
    }
}
