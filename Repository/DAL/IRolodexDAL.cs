using Repository.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public interface IRolodexDAL
    {
        public int CreateContact(Person person);
        public Person GetContactById(int id);
        public List<PhysicalAddress> GetPhysicalAddressByPersonId(int id);
        public List<EmailAddress> GetEmailAddressByPersonId(int id);
        public List<PhoneNumber> GetPhoneNumberByPersonId(int id);
        public List<Person> GetContacts();
        public int AddEmailToContact(EmailAddress emailAddress);
        public int AddPhysicalAddressToContact(PhysicalAddress physicalAddress);
        public int AddPhoneNumberToContact(PhoneNumber phoneNumber);
        public int DeleteContact(int id);
        public void DeleteAllEmailAddressesByPersonID(int id);
        public void DeleteAllPhoneNumbersByPersonID(int id);
        public void DeleteAllPhysicalAddressesByPersonID(int id);
        public int DeletePhysicalAddress(int id);
        public int DeleteEmailAddress(int id);
        public int DeletePhoneNumber(int id);
        public int RemoveEmailFromContaxt(int personId, int emailId);
        public int RemovePhoneFromContaxt(int personId, int phoneNumberId);
        public int RemovePhysicalAddressFromContact(int personId, PhysicalAddress physicalAddressId);
        public int UpdateContact(Person person);
    }
}
