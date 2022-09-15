using System.Text.Json.Serialization;

namespace WebUI.Models
{
    public class EmailAddressViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("personId")]
        public int PersonId { get; set; }
    }
}