using AutoMapper;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;

namespace MPDex.Models
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
            //Balance mapping
            CreateMap<Balance, BalanceCreateModel>();
            CreateMap<BalanceCreateModel, Balance>();
            CreateMap<Balance, BalanceUpdateModel>();
            CreateMap<BalanceUpdateModel, Balance>();
            CreateMap<Balance, BalanceViewModel>();
            CreateMap<BalanceViewModel, Balance>();

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

            //Contract mapping
            CreateMap<Contract, ContractCreateModel>();
            CreateMap<ContractCreateModel, Contract>();
            CreateMap<Contract, ContractViewModel>();
            CreateMap<ContractViewModel, Contract>();

            //Customer mapping
            CreateMap<Customer, CustomerCreateModel>();
            CreateMap<CustomerCreateModel, Customer>();
            CreateMap<Customer, CustomerUpdateModel>();
            CreateMap<CustomerUpdateModel, Customer>();
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<CustomerViewModel, Customer>();

            //Fee mapping
            CreateMap<Fee, FeeCreateModel>();
            CreateMap<FeeCreateModel, Fee>();
            CreateMap<Fee, FeeViewModel>();
            CreateMap<FeeViewModel, Fee>();

            //Order mapping
            CreateMap<Order, OrderCreateModel>();
            CreateMap<OrderCreateModel, Order>();
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderViewModel, Order>();

            //Statement mapping
            CreateMap<Statement, StatementCreateModel>();
            CreateMap<StatementCreateModel, Statement>();
            CreateMap<Statement, StatementViewModel>();
            CreateMap<StatementViewModel, Statement>();

            //Trade mapping
            CreateMap<Trade, TradeCreateModel>();
            CreateMap<TradeCreateModel, Trade>();
            CreateMap<Trade, TradeViewModel>();
            CreateMap<TradeViewModel, Trade>();
        }
    }
}
