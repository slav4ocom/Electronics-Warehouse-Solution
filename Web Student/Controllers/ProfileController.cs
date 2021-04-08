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
        private readonly RoleManager<IdentityRole> roleManager;
        public ProfileController(UserManager<IdentityUser> userMan, RoleManager<IdentityRole> roleMan)
        {
            this.userManager = userMan;
            this.roleManager = roleMan;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.GetUserAsync(this.User);
            var myContext = new StudentDbContext();
            var currentUserData = myContext.UserProfiles.FirstOrDefault(p => p.UserFK == currentUser.Id);


            if (currentUserData == null)
            {
                currentUserData = new CommonModels.Models.UserProfile();
                currentUserData.FullName = "Вашите имена";
                currentUserData.Grade = "въведете вашият клас";
                currentUserData.PhoneNum = "088 XX XX XXX";
                currentUserData.School = "Комаров";
                currentUserData.Town = "Велико Търново";
                ViewBag.profileFinished = false;
                //return Json("You have not setup your profile yet.");
            }
            else
            {
                ViewBag.profileFinished = true;
            }

            ViewBag.userData = currentUserData;

            if (currentUserData.PictureName == null || currentUserData.PictureName == string.Empty)
            {
                ViewBag.userData.PictureName = "student_small.jpg";
            }

            ViewBag.User = User;
            return View();
        }

        public IActionResult SubmitProfile()
        {
            //return Json("GOTOVO !");
            return View("../Home/Index");
        }

        public async Task<IActionResult> AddTeacher()
        {
            if (!await roleManager.RoleExistsAsync("Teacher"))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Teacher",
                });
            }
            var currentUser = await userManager.GetUserAsync(this.User);
            var result = await userManager.AddToRoleAsync(currentUser, "Teacher");
            return Json(result);
        }

        public async Task<IActionResult> RemoveTeacher()
        {
            if (!await roleManager.RoleExistsAsync("Teacher"))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Teacher",
                });
            }
            var currentUser = await userManager.GetUserAsync(this.User);
            var result = await userManager.RemoveFromRoleAsync(currentUser, "Teacher");

            return Json(result);
        }

        public async Task<IActionResult> AddStudent()
        {
            if (!await roleManager.RoleExistsAsync("Student"))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Student",
                });
            }
            var currentUser = await userManager.GetUserAsync(this.User);
            var result = await userManager.AddToRoleAsync(currentUser, "Student");
            return Json(result);
        }
        public async Task<IActionResult> RemoveStudent()
        {
            if (!await roleManager.RoleExistsAsync("Student"))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Student",
                });
            }
            var currentUser = await userManager.GetUserAsync(this.User);
            var result = await userManager.RemoveFromRoleAsync(currentUser, "Student");
            return Json(result);
        }
    }
}
