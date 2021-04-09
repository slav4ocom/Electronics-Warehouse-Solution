using CommonModels;
using CommonModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web_Manager_v._2.Models;

namespace Web_Manager_v._2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> userManager;

        public HomeController(ILogger<HomeController> logger,
                                UserManager<IdentityUser> userMan)
        {
            _logger = logger;
            userManager = userMan;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.GetUserAsync(this.User);
            var myContext = new StudentDbContext();
            UserProfile currentUserData = null;
            if (currentUser != null)
            {
                currentUserData = myContext.UserProfiles.FirstOrDefault(p => p.UserFK == currentUser.Id);

            }

            if (currentUserData == null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Profile");
            }
            else
            {
                return View();

            }

            //return View();
        }

        public IActionResult About()
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
