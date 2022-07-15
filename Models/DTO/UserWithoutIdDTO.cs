using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace EsercitazioneAPI.Models.DTO
{
    public class UserWithoutIdDTO
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

        [Required(ErrorMessage = "Role is required")]
        [JsonProperty("role")]
        [JsonConverter(typeof(StringEnumConverter))]
        public UsersRolesModel Role { get; set; }
    }
}
