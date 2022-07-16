using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Dotnet6_API.Models.DTO.User
{
    public class UserWithoutRoleAndIdDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters")]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
