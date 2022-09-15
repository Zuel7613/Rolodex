using System.Text.Json.Serialization;

namespace WebUI.Models
{
    public class PhysicalAddressViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("streetAddress")]
        public string StreetAddress { get; set; } = null!;

        [JsonPropertyName("city")]
        public string City { get; set; } = null!;

        [JsonPropertyName("region")]
        public string Region { get; set; } = null!;

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; } = null!;

        [JsonPropertyName("personId")]
        public int PersonId { get; set; }
    }
}