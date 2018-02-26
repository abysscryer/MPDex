using Microsoft.AspNetCore.SignalR;
using MPDex.Models.ViewModels;
using System.Threading.Tasks;

namespace MPDex.Web.Api.Hubs
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
    }
}
