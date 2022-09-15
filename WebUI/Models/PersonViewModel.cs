using System.ComponentModel;
using System.Text.Json.Serialization;

namespace WebUI.Models
{
    public class PersonViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("firstName")]
        [DisplayName("First Name")]
        public string FirstName { get; set; } = null!;

        [JsonPropertyName("lastName")]
        [DisplayName("Last Name")]
        public string LastName { get; set; } = null!;

        [JsonPropertyName("emailAddresses")]
        public List<EmailAddressViewModel> EmailAddresses { get; set; } = new();

        [JsonPropertyName("phoneNumbers")]
        public List<PhoneNumberViewModel> PhoneNumbers { get; set; } = new();

        [JsonPropertyName("physicalAddresses")]
        public List<PhysicalAddressViewModel> PhysicalAddresses { get; set; } = new();
    }
}