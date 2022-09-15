using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class RolodexController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RolodexController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            var people = new List<PersonViewModel>();
            var values = GetContacts().Result;
            if(values != null)
            foreach( var contact in values)
            {
                people.Add(contact);
            }
            return View(people);
        }

        public IActionResult AddOrEditContact()
        {
            return View();
        }

        public IActionResult AddContact()
        {
            var person = new PersonViewModel();
            person.PhysicalAddresses.Add(new() { StreetAddress = "" });
            person.PhoneNumbers.Add(new() { Number = "" });
            person.EmailAddresses.Add(new() { Email = "" });
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> AddContactAsync(PersonViewModel person)
        {
            string url = "https://localhost:7228/api/Rolodex/Contact";
            var content = new StringContent(JsonSerializer.Serialize(person), Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.PostAsync(url, content);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Contacts");
            }

            return RedirectToAction("Contacts");
        }

        public async Task<List<PersonViewModel>?> GetContacts()
        {
            string url = "https://localhost:7228/api/Rolodex/Contacts";
            List<PersonViewModel>? people = null;

            var httpClient = _httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.GetAsync(url);

            Console.WriteLine($"Response: {httpResponseMessage.StatusCode}");

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };
                people = await JsonSerializer.DeserializeAsync<List<PersonViewModel>>(contentStream, options);
            }

            return people;
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string url = $"https://localhost:7228/api/Rolodex/Contact?id={id}";
            PersonViewModel? person = new();

            var httpClient = _httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.GetAsync(url);

            Console.WriteLine($"Response: {httpResponseMessage.StatusCode}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };
                person = await JsonSerializer.DeserializeAsync<PersonViewModel>(contentStream, options);
            }
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            string url = $"https://localhost:7228/api/Rolodex/Contact?id={id}";
            var httpClient = _httpClientFactory.CreateClient();

            await httpClient.DeleteAsync(url);

            return RedirectToAction("Contacts");
        }

        public async Task<IActionResult> EditContact(int id)
        {
            string url = $"https://localhost:7228/api/Rolodex/Contact?id={id}";
            PersonViewModel? person = new();

            var httpClient = _httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.GetAsync(url);

            Console.WriteLine($"Response: {httpResponseMessage.StatusCode}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };
                person = await JsonSerializer.DeserializeAsync<PersonViewModel>(contentStream, options);
            }
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> EditContactAsync(PersonViewModel person)
        {
            var url = $"https://localhost:7228/api/Rolodex/Contact?id={person.Id}";
            var content = new StringContent(JsonSerializer.Serialize(person), Encoding.UTF8, "application/json");
            var httpClient = _httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.PutAsync(url, content);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Contacts");
            }

            return RedirectToAction("Contacts");
        }

        [HttpGet]
        public ActionResult AddAddress(int id)
        {
            var address = new PhysicalAddressViewModel()
            {
                PersonId = id
            };

            return View(address);
        }

        [HttpPost]
        public async Task<IActionResult> AddAddressAsync(PhysicalAddressViewModel address)
        {
            var url = $"https://localhost:7228/api/Rolodex/Address";
            var content = new StringContent(JsonSerializer.Serialize(address), Encoding.UTF8, "application/json");
            var httpClient = _httpClientFactory.CreateClient();

            await httpClient.PostAsync(url, content);

            return RedirectToAction("Contacts");
        }

        [HttpGet]
        public ActionResult AddEmail(int id)
        {
            var address = new EmailAddressViewModel()
            {
                PersonId = id
            };

            return View(address);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmailAsync(EmailAddressViewModel address)
        {
            var url = $"https://localhost:7228/api/Rolodex/Email";
            var content = new StringContent(JsonSerializer.Serialize(address), Encoding.UTF8, "application/json");
            var httpClient = _httpClientFactory.CreateClient();

            await httpClient.PostAsync(url, content);

            return RedirectToAction("Contacts");
        }

        [HttpGet]
        public ActionResult AddPhone(int id)
        {
            var number = new PhoneNumberViewModel()
            {
                PersonId = id
            };

            return View(number);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhoneAsync(PhoneNumberViewModel phoneNumber)
        {
            var url = $"https://localhost:7228/api/Rolodex/Phone";
            var content = new StringContent(JsonSerializer.Serialize(phoneNumber), Encoding.UTF8, "application/json");
            var httpClient = _httpClientFactory.CreateClient();

            await httpClient.PostAsync(url, content);

            return RedirectToAction("Contacts");
        }
    }
}
