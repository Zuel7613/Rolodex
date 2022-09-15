using AutoMapper;
using Repository.Models;
using WebUI.Models;

namespace WebService.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonViewModel, Person>().ReverseMap();
            CreateMap<EmailAddressViewModel, EmailAddress>().ReverseMap();
            CreateMap<PhysicalAddressViewModel, PhysicalAddress>().ReverseMap();
            CreateMap<PhoneNumberViewModel, PhoneNumber>().ReverseMap();
        }
    }
}
