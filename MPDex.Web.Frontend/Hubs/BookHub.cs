using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MPDex.Models.ViewModels;

namespace MPDex.Web.Frontend.Hubs
{
    public class BookHub : Hub<IBookHub>
    {
        public async Task Send(BookSummaryModel book)
        {
            // Call the broadcastMessage method to update clients.
            await Clients.All.UpdateBook(book);
        }
    }

    public interface IBookHub
    {
        Task UpdateBook(BookSummaryModel book);
        //Task<IEnumerable<BookSummaryModel>> GetBooks();
    }
}
