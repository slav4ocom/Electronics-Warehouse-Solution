using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Manager_v._2.Data;

namespace Web_Student.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public AdminController(UserManager<IdentityUser> userMan)
        {
            this.userManager = userMan;
        }

        public UserManager<IdentityUser> UserMan { get; }

        public IActionResult Index()
        {
            //var result = userManager.Users.Select(n => n.Id + " " + n.UserName).ToList();
            //ViewData["students"] = userManager.Users.ToList();
            ViewBag.students = userManager.Users.ToArray();
            //return Json(result);
            return View();
        }
    }
}
