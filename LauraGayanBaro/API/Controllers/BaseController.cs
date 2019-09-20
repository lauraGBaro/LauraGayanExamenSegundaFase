using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogger Logger;
        protected readonly MyDbContext _context;

        public BaseController(ILogger<BaseController> logger)
        {
            Logger = logger;
            _context = new MyDbContext();
        }
    }
}