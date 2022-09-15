using System.Text.Json.Serialization;

namespace WebUI.Models
{
    public class PhoneNumberViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; } = null!;

        [JsonPropertyName("personId")]
        public int PersonId { get; set; }
    }
}