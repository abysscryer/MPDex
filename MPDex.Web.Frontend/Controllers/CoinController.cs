using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MPDex.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MPDex.Web.Frontend.Controllers
{
    public class CoinController : Controller
    {
        private readonly CoinManager coinManager;
        private readonly ILogger<CoinController> logger;

        public CoinController(CoinManager coinManager, ILogger<CoinController> logger)
        {
            this.logger = logger;
            this.coinManager = coinManager;
            
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }

    public class CoinManager
    {
    }
}
