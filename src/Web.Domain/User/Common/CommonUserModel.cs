using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Web.Domain.User.Common
{
    public class UserIdModel 
    {
        [JsonPropertyName("id")]
        [Required(ErrorMessage = "id parameter is required")]
        public int Id { get; set; }
    }
    public class UserModel : CommonUserModel
    {
        [JsonPropertyName("id")]
        [Required(ErrorMessage = "id parameter is required")]
        public int Id { get; set; }
    }
    public class CommonUserModel
    {
        [JsonPropertyName("username")]
        [Required(ErrorMessage = "username parameter is required")]
        [StringLength(100,ErrorMessage = "username parameter cannot more than 100 character")]
        public string UserName { get; set; }

        [JsonPropertyName("email")]
        [Required(ErrorMessage = "email parameter is required")]
        [EmailAddress(ErrorMessage = "email parameter is not a valid email format")]
        [StringLength(150, ErrorMessage = "email parameter cannot more than 150 character")]
        public string Email { get; set; }


        [JsonPropertyName("phone")]
        [Phone(ErrorMessage = "phone parameter is not a valid phone number")]
        public string? Phone { get; set; }

        [JsonPropertyName("skillsets")]
        public string? SkillSets { get; set; }
        [JsonPropertyName("hobby")]
        public string? Hobby { get; set; }
    }
}
