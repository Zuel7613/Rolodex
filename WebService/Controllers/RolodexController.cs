using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.DAL;
using Repository.Models;
using WebUI.Models;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolodexController : ControllerBase
    {
        private readonly IRolodexDAL _rolodexDAL;
        private readonly IMapper _mapper;
        private readonly ILogger<RolodexController> _logger;

        public RolodexController(IRolodexDAL rolodexDAL, ILogger<RolodexController> logger, IMapper mapper)
        {
            _rolodexDAL = rolodexDAL;
            _mapper = mapper;
            _logger = logger;
        }

        // POST api/rolodex/createContact
        [HttpPost]
        [Route("Contact")]
        public ActionResult<int> CreateContact(PersonViewModel person)
        {
            var personDto = _mapper.Map<Person>(person);
            return Ok(_rolodexDAL.CreateContact(personDto));
        }

        // DELETE api/Rolodex/DeleteContact/id
        [HttpDelete]
        [Route("Contact")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int> DeleteContact(int id)
        {
            try
            {
                return Ok(_rolodexDAL.DeleteContact(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // GET api/rolodex/GetContactByID
        [HttpGet]
        [Route("Contact")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonViewModel> GetContactById(int id) 
        {
            Person personDto;
            try
            {
                personDto = _rolodexDAL.GetContactById(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
            var person = _mapper.Map<PersonViewModel>(personDto);
            return Ok(person);
        }

        // PUT api/Rolodex/Contact
        [HttpPut]
        [Route("Contact")]
        public ActionResult<int> UpdateContact(PersonViewModel person)
        {
            var personDto = _mapper.Map<Person>(person);
            try
            {
                return Ok(_rolodexDAL.UpdateContact(personDto));
            }
            catch
            {
                return NotFound();
            }
        }

        // GET api/rolodex/GetContacts
        [HttpGet]
        [Route("Contacts")]
        public List<PersonViewModel> GetContacts() => _mapper.Map<List<PersonViewModel>>(_rolodexDAL.GetContacts());

        // Post api/rolodex/Address
        [HttpPost]
        [Route("Address")]
        public ActionResult<int> AddAddressToContact(PhysicalAddressViewModel address)
        {
            var physicalAddressDto = _mapper.Map<PhysicalAddress>(address);
            return Ok(_rolodexDAL.AddPhysicalAddressToContact(physicalAddressDto));
        }

        // Post api/rolodex/Email
        [HttpPost]
        [Route("Email")]
        public ActionResult<int> AddEmailAddressToContact(EmailAddressViewModel address)
        {
            var emailAddressDto = _mapper.Map<EmailAddress>(address);
            return Ok(_rolodexDAL.AddEmailToContact(emailAddressDto));
        }

        // Post api/rolodex/Phone
        [HttpPost]
        [Route("Phone")]
        public ActionResult<int> AddPhoneToContact(PhoneNumberViewModel phoneNumber)
        {
            var phoneNumberDto = _mapper.Map<PhoneNumber>(phoneNumber);
            return Ok(_rolodexDAL.AddPhoneNumberToContact(phoneNumberDto));
        }
    }
}
