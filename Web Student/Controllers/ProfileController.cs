using CommonModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Student.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        public ProfileController(UserManager<IdentityUser> userMan)
        {
            this.userManager = userMan;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.GetUserAsync(this.User);
            var myContext = new StudentDbContext();
            var currentUserData = myContext.UserProfiles.FirstOrDefault(p => p.UserFK == currentUser.Id);

            if(currentUserData == null)
            {
                return Json("You have not setup your profile yet.");
            }

            ViewBag.userData = currentUserData;
            return View();
        }
    }
}
