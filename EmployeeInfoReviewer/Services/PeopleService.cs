using AutoMapper;
using EmployeeDataAccessLibrary.DataAccess.Sql;
using EmployeeDataAccessLibrary.Models;
using EmployeeInfoReviewer.Interfaces;
using EmployeeInfoReviewer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeInfoReviewer.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly PeopleContext _context;
        private readonly IMapper _mapper;


        public PeopleService(PeopleContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<ReviewerPerson> Get()
        {
            var sourePeopleInfo = _context.People.Include(a => a.Addresses).Include(e => e.EmailAddresses);
            return _mapper.Map<List<ReviewerPerson>>(sourePeopleInfo);
        }

        public ReviewerPerson Get(int id)
        {
            if (!PersonExists(id))
            {
                return null;
            }

            var person = _context.People
                    .Include(a => a.Addresses)
                    .Include(e => e.EmailAddresses).Where(x => x.Id == id).ToList().FirstOrDefault();

            return _mapper.Map<ReviewerPerson>(person);
        }

        public void Post(ReviewerPerson person)
        {
            var convertedPerson = _mapper.Map<Person>(person);
            _context.People.Add(convertedPerson);
            _context.SaveChanges();
        }

        public string Update(int id, ReviewerPerson inputPerson)
        {
            var originalPerson = _context.People
                .Include(a => a.Addresses)
                .Include(e => e.EmailAddresses)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            var convertedInputPerson = _mapper.Map<Person>(inputPerson);

            if (originalPerson != null)
            {
                _context.Entry(originalPerson).CurrentValues.SetValues(convertedInputPerson);

                CleanAddressInDbContext(originalPerson, convertedInputPerson);
                CleanEmailAddressesInDbContext(originalPerson, convertedInputPerson);
                UpdateAddresseInDbContext(originalPerson.Addresses, convertedInputPerson.Addresses);
                UpdateEmailAddresseInDbContext(originalPerson.EmailAddresses, convertedInputPerson.EmailAddresses);
            }

            try
            {
                _context.SaveChanges();
                return "Success";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return "NotFound";
                }
                else
                {
                    return "UnExpectError";
                }
            }
        }

        private void CleanAddressInDbContext(Person originalPerson, Person inputPerson)
        {
            foreach (var address in originalPerson.Addresses.ToList())
            {
                if (!inputPerson.Addresses.Any(a => a.Id == address.Id))
                {
                    _context.Addresses.Remove(address);
                }
            }
        }

        private void CleanEmailAddressesInDbContext(Person originalPerson, Person inputPerson)
        {
            foreach (var email in originalPerson.EmailAddresses.ToList())
            {
                if (!inputPerson.EmailAddresses.Any(a => a.Id == email.Id))
                {
                    _context.EmailsAddresses.Remove(email);
                }
            }
        }

        private void UpdateAddresseInDbContext(List<Address> oringalAddresses, List<Address> inputAddresses)
        {
            foreach (var inputAddress in inputAddresses)
            {
                var originalAddress = oringalAddresses
                    .Where(a => a.Id == inputAddress.Id)
                    .FirstOrDefault();

                var newAddress = new Address
                {
                    StreetAddress = inputAddress.StreetAddress,
                    City = inputAddress.City,
                    State = inputAddress.State,
                    ZipCode = inputAddress.ZipCode
                };

                oringalAddresses.Add(newAddress);
            }
        }

        private void UpdateEmailAddresseInDbContext(List<Email> originalEmailAddresses, List<Email> inputEmailAddresses)
        {
            foreach (var inputEmail in inputEmailAddresses)
            {
                var originalEmail = originalEmailAddresses
                    .Where(a => a.Id == inputEmail.Id)
                    .FirstOrDefault();

                var newEmail = new Email
                {
                    EmailAddress = inputEmail.EmailAddress
                };

                originalEmailAddresses.Add(newEmail);
            }
        }

        public bool Delete(int id)
        {
            var person = _context.People
               .Include(a => a.Addresses)
               .Include(e => e.EmailAddresses).FirstOrDefault();

            if (person == null)
            {
                return false;
            }

            _context.People.Remove(person);
            _context.SaveChangesAsync();

            return true;
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }

    }
}
