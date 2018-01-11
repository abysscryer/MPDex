using AutoMapper;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;

namespace MPDex.Services
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<AutoMappingProfile>();
            });
        }
    }

    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            //Customer mapping
            CreateMap<Customer, CustomerCreateModel>();
            CreateMap<CustomerCreateModel, Customer>();
            CreateMap<Customer, CustomerUpdateModel>();
            CreateMap<CustomerUpdateModel, Customer>();
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<CustomerViewModel, Customer>();


            //Book mapping
            CreateMap<Book, BookCreateModel>();
            CreateMap<BookCreateModel, Book>();
            CreateMap<Book, BookUpdateModel>();
            CreateMap<BookUpdateModel, Book>();
            CreateMap<Book, BookViewModel>();
            CreateMap<BookViewModel, Book>();

            //Coin mapping
            CreateMap<Coin, CoinCreateModel>();
            CreateMap<CoinCreateModel, Coin>();
            CreateMap<Coin, CoinUpdateModel>();
            CreateMap<CoinUpdateModel, Coin>();
            CreateMap<Coin, CoinViewModel>();
            CreateMap<CoinViewModel, Coin>();
        }
    }
}
