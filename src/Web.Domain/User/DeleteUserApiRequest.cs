using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Web.Domain.User
{
    public class DeleteUserApiRequest
    {
        [Required]
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
