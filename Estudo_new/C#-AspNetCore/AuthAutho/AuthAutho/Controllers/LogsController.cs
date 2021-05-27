using Microsoft.AspNetCore.Mvc;
using KissLog;
using Microsoft.AspNetCore.Identity;
using AuthAutho.Areas.Identity.Data;

namespace AuthAutho.Controllers
{
    public class LogsController : Controller
    {
        private readonly ILogger  _logger;
        private readonly UserManager<ApplicationUser> _user;
        public LogsController(ILogger logger, UserManager<ApplicationUser> user)
        {
            _user = user;
            _logger = logger;
        }
        public IActionResult Index()
        {
            _logger.Trace("o usuario passou aqui.", $"Id_Usuario::[{_user.GetUserId(User)} ] linha::",19);
            _logger.Debug("Esse erro aconteceu aqui...");
            return View();
        }
    }
}