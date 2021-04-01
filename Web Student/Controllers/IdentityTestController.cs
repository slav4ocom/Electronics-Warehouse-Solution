using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Manager_v._2.Controllers
{
    public class IdentityTestController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        public IdentityTestController(UserManager<IdentityUser> userMan)
        {
            this.userManager = userMan;
        }

        public async Task<IActionResult> WhoAmI()
        {
            var user = await userManager.GetUserAsync(this.User);
            return this.Json(user);
        }
        public IActionResult Index()
        {
            return Json("index.html");
            //return View();
        }
    }
}
