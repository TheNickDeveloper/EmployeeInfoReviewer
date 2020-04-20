using AutoMapper;
using EmployeeDataAccessLibrary.DataAccess.Sql;
using EmployeeDataAccessLibrary.Models;
using EmployeeInfoReviewer.Interfaces;
using EmployeeInfoReviewer.Models;
using EmployeeInfoReviewer.Services.LogControllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeInfoReviewer.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly PeopleContext _context;
        private readonly IMapper _mapper;
        private readonly LogHelper _logHelper;

        public PeopleService(PeopleContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _logHelper = new LogHelper("PeopleService", new PeopleLogActionNameHandler());
        }

        public IEnumerable<ReviewerPerson> Get()
        {
            _logHelper.GetTaskActionName("GetPeople");
            var sourePeopleInfo = _context.People.Include(a => a.Addresses).Include(e => e.EmailAddresses);
            var result = new List<ReviewerPerson>();
            try
            {
                result = _mapper.Map<List<ReviewerPerson>>(sourePeopleInfo);
                _logHelper.ReturnSuccessStatus();
            }
            catch (Exception e)
            {
                _logHelper.ReturnUncontrolException(e.Message);
            }
            return result;
        }

        public ReviewerPerson Get(int id)
        {
            _logHelper.GetTaskActionName("GetPerson",id.ToString());

            if (!PersonExists(id))
            {
                _logHelper.ReturnNoFoudStatus(id.ToString());
                return null;
            }

            var person = _context.People
                    .Include(a => a.Addresses)
                    .Include(e => e.EmailAddresses).Where(x => x.Id == id).ToList().FirstOrDefault();

            var reviewerPerson = new ReviewerPerson();

            try
            {
                reviewerPerson = _mapper.Map<ReviewerPerson>(person);
                _logHelper.ReturnSuccessStatus();
            }
            catch (Exception e)
            {
                _logHelper.ReturnUncontrolException(e.Message);
            }

            return reviewerPerson;
        }

        public void Post(ReviewerPerson person)
        {
            _logHelper.GetTaskActionName("PostPerson");

            var convertedPerson = _mapper.Map<Person>(person);
            _context.People.Add(convertedPerson);

            try
            {
                _context.SaveChanges();
                _logHelper.ReturnSuccessStatus();
            }
            catch (Exception e)
            {
                _logHelper.ReturnUncontrolException(e.Message);
            }
        }

        public string Update(int id, ReviewerPerson inputPerson)
        {
            _logHelper.GetTaskActionName("UpdatePerson",id.ToString());

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
                _logHelper.ReturnSuccessStatus();
                return "Success";
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!PersonExists(id))
                {
                    _logHelper.ReturnNoFoudStatus(id.ToString());
                    return "NotFound";
                }
                else
                {
                    _logHelper.ReturnUncontrolException(e.Message);
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
            _logHelper.GetTaskActionName("DeletePerson", id.ToString());
            var person = _context.People
               .Include(a => a.Addresses)
               .Include(e => e.EmailAddresses).FirstOrDefault();

            if (person == null)
            {
                _logHelper.ReturnNoFoudStatus(id.ToString());
                return false;
            }

            _context.People.Remove(person);

            try
            {
                _context.SaveChangesAsync();
                _logHelper.ReturnSuccessStatus();
            }
            catch (Exception e)
            {
                _logHelper.ReturnUncontrolException(e.Message);
                return false;
            }

            return true;
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }

    }
}
