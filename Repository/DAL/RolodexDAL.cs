using Microsoft.EntityFrameworkCore;
using Repository.DBContext;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public class RolodexDAL : IRolodexDAL
    {
        private readonly RolodexContext _rolodexContext;

        public RolodexDAL(RolodexContext context)
        {
            _rolodexContext = context;
        }
        public int AddEmailToContact(EmailAddress emailAddress)
        {
            var tempEmail = new EmailAddress()
            {
                Email = emailAddress.Email,
                PersonId = emailAddress.PersonId
            };
            //emailAddress.PersonId = personId;
            //var person = GetContactById(personId);
            //emailAddress.Person = person;
            _rolodexContext.EmailAddresses.Add(tempEmail);
            _rolodexContext.SaveChanges();
            return emailAddress.Id;
        }

        public int AddPhoneNumberToContact(PhoneNumber phoneNumber)
        {
            var tempPhoneNumber = new PhoneNumber()
            {
                Number = phoneNumber.Number,
                PersonId = phoneNumber.PersonId
            };
            //phoneNumber.PersonId = personId;
            //var person = GetContactById(personId);
            //phoneNumber.Person = person;
            _rolodexContext.PhoneNumbers.Add(tempPhoneNumber);
            _rolodexContext.SaveChanges();
            return phoneNumber.Id;
        }

        public int AddPhysicalAddressToContact(PhysicalAddress physicalAddress)
        {
            var tempAddress = new PhysicalAddress()
            {
                StreetAddress = physicalAddress.StreetAddress,
                City = physicalAddress.City,
                Region = physicalAddress.Region,
                PostalCode = physicalAddress.PostalCode,
                PersonId = physicalAddress.PersonId
            };
            _rolodexContext.PhysicalAddresses.Add(tempAddress);
            _rolodexContext.SaveChanges();
            return physicalAddress.Id;
        }

        public int CreateContact(Person person)
        {
            var tempPerson = new Person(person.FirstName, person.LastName);
            _rolodexContext.People.Add(tempPerson);
            _rolodexContext.SaveChanges();
            foreach(var address in person.PhysicalAddresses)
            {
                address.PersonId = tempPerson.Id;
                AddPhysicalAddressToContact(address);
            }
            foreach(var emailAddress in person.EmailAddresses)
            {
                emailAddress.PersonId = tempPerson.Id;
                AddEmailToContact(emailAddress);
            }
            foreach(var phoneNumber in person.PhoneNumbers)
            {
                phoneNumber.PersonId = tempPerson.Id;
                AddPhoneNumberToContact(phoneNumber);
            }
            return person.Id;
        }

        public int DeleteContact(int id)
        {
            var person = _rolodexContext.People
                .Where(p => p.Id == id)
                .Include(e => e.EmailAddresses)
                .Include(e => e.PhoneNumbers)
                .Include(e => e.PhysicalAddresses)
                .Single();
            _rolodexContext.Remove(person);
            _rolodexContext.SaveChanges();
            return id;
        }

        public void DeleteAllEmailAddressesByPersonID(int id)
        {
            var emailaddresses = _rolodexContext.EmailAddresses
                .Where(p => p.PersonId == id)
                .ToList();
            _rolodexContext.RemoveRange(emailaddresses);
            _rolodexContext.SaveChanges();
        }

        public void DeleteAllPhoneNumbersByPersonID(int id)
        {
            var phoneNumbers = _rolodexContext.PhoneNumbers
                .Where(p => p.PersonId == id)
                .ToList();
            _rolodexContext.RemoveRange(phoneNumbers);
            _rolodexContext.SaveChanges();
        }

        public void DeleteAllPhysicalAddressesByPersonID(int id)
        {
            var physicalAddresses = _rolodexContext.PhysicalAddresses
                .Where(p => p.PersonId == id)
                .ToList();
            _rolodexContext.RemoveRange(physicalAddresses);
            _rolodexContext.SaveChanges();
        }

        public int DeleteEmailAddress(int id)
        {
            var emailAddress = _rolodexContext.EmailAddresses
                .Where(p => p.Id == id)
                .Single();
            _rolodexContext.Remove(emailAddress);
            _rolodexContext.SaveChanges();
            return id;
        }

        public int DeletePhoneNumber(int id)
        {
            var phoneNumber = _rolodexContext.PhoneNumbers
                .Where(p => p.Id == id)
                .Single();
            _rolodexContext.Remove(phoneNumber);
            _rolodexContext.SaveChanges();
            return id;
        }

        public int DeletePhysicalAddress(int id)
        {
            var physicalAddresses = _rolodexContext.PhysicalAddresses
                .Where(p => p.Id == id)
                .Single();
            _rolodexContext.Remove(physicalAddresses);
            _rolodexContext.SaveChanges();
            return id;
        }

        public Person GetContactById(int id)
        {
            var person = _rolodexContext.People
                .Where(p => p.Id == id)
                .Single();
            return person;
        }

        public List<Person> GetContacts()
        {
            var people = _rolodexContext.People.ToList();
            foreach(var person in people)
            {
                GetEmailAddressByPersonId(person.Id);
                GetPhoneNumberByPersonId(person.Id);
                GetPhysicalAddressByPersonId(person.Id);
            }
            return people;
        }

        public List<EmailAddress> GetEmailAddressByPersonId(int id)
        {
            return _rolodexContext.EmailAddresses
                .Where(e => e.PersonId == id)
                .ToList();
        }

        public List<PhoneNumber> GetPhoneNumberByPersonId(int id)
        {
            return _rolodexContext.PhoneNumbers
                .Where(e => e.PersonId == id)
                .ToList();
        }

        public List<PhysicalAddress> GetPhysicalAddressByPersonId(int id)
        {
            return _rolodexContext.PhysicalAddresses
                .Where(e => e.PersonId == id)
                .ToList();
        }

        public int RemoveEmailFromContaxt(int personId, int emailId)
        {
            throw new NotImplementedException();
        }

        public int RemovePhoneFromContaxt(int personId, int phoneNumberId)
        {
            throw new NotImplementedException();
        }

        public int RemovePhysicalAddressFromContact(int personId, PhysicalAddress physicalAddressId)
        {
            throw new NotImplementedException();
        }

        public int UpdateContact(Person person)
        {
            var person2 = _rolodexContext.People
                .Where(p => p.Id == person.Id)
                .Single();
            person2 = person;
            _rolodexContext.SaveChanges();
            return person.Id;
        }
    }
}
