using AutoMapper;
using MPDex.Models;
using MPDex.Models.ViewModels;

namespace MPDex.Web.Frontend.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerCreateViewModel>();
            CreateMap<CustomerCreateViewModel, Customer>();
            CreateMap<Book, BookCreateViewModel>();
            CreateMap<BookCreateViewModel, Book>();
        }
    }
}
