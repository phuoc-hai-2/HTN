using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Job_Portal.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Job_Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                if (await _userManager.IsInRoleAsync(user, "Employer"))
                {
                    return RedirectToAction("Dashboard", "Employer");
                }
                else if (await _userManager.IsInRoleAsync(user, "JobSeeker"))
                {
                    return RedirectToAction("Search", "Jobs");
                }
            }

            return View(); // Trang chủ mặc định cho khách
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
