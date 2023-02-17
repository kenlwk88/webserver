using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Web.Domain.User.Common;

namespace Web.Domain.User
{
    public class UpdateUserApiRequest : CommonUserModel
    {
        [Required]
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
