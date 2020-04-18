using AutoMapper;
using EmployeeDataAccessLibrary.Models;
using EmployeeInfoReviewer.Models;

namespace EmployeeInfoReviewer.Services.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, ReviewerPerson>();
            CreateMap<Address, ReviewerAddress>();
            CreateMap<Email, ReviewerEmail>();
        }
    }
}
