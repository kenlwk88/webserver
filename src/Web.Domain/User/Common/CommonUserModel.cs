using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Web.Domain.User.Common
{
    public class UserModel : CommonUserModel
    {
        [Required]
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
    public class CommonUserModel
    {
        [Required]
        [JsonPropertyName("full_name")]
        public string FullName { get; set; }
        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
        [JsonPropertyName("age")]
        public int? Age { get; set; }
    }
}
