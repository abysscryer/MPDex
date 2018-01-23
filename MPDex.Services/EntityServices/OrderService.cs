using AutoMapper;
using Microsoft.Extensions.Logging;
using MPDex.Models.Base;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using MPDex.Repository;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MPDex.Services
{
    public interface IOrderService : IService<Order>
    { }

    public class OrderService : Service<Order>, IOrderService
    {
        public OrderService(IUnitOfWork unitOfWork,
                           ILogger<Service<Order>> logger)
            : base(unitOfWork, logger)
        { }
    }
}
