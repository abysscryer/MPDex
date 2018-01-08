using AutoMapper;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;

namespace MPDex.Web.Frontend.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerCreateModel>();
            CreateMap<CustomerCreateModel, Customer>();
            CreateMap<Book, BookCreateModel>();
            CreateMap<BookCreateModel, Book>();
        }
    }
}
