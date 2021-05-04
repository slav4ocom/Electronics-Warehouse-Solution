using CommonModels;
using CommonModels.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Teacher")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public AdminController(UserManager<IdentityUser> userMan)
        {
            this.userManager = userMan;
        }

        public UserManager<IdentityUser> UserMan { get; }

        public async Task<IActionResult> Index()
        {
            //ViewBag.students = userManager.Users.ToArray();
            var studentUsers = await userManager.GetUsersInRoleAsync("Student");

            var students = studentUsers
                .Select(n => GetUserProfile(n))
                .Where(n => n != null)
                .ToList();

            students
                .Where(n => System.IO.File.Exists($"{PictureProcessing.PictureProcessor.profileAbsolutePath}/{n.UserFK}_small.jpg"))
                .ToList()
                .ForEach(s => s.Picture = $"{s.UserFK}_small.jpg");
            
            students
                .Where(n => System.IO.File.Exists($"{PictureProcessing.PictureProcessor.profileAbsolutePath}/{n.UserFK}_small.jpg") == false)
                .ToList()
                .ForEach(s => s.Picture = $"student_small.jpg");

       
            ViewBag.StudentProfiles = students;
            return View();
        }

        private UserProfile GetUserProfile(IdentityUser n)
        {
            var context = new StudentDbContext();

            var result = context.UserProfiles.FirstOrDefault(p => p.UserFK == n.Id);

            return result;
        }
    }
}
