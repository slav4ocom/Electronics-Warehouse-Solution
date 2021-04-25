﻿using CommonModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonModels.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using PictureProcessing;

namespace Web_Student.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public ProfileController(UserManager<IdentityUser> userMan,
            RoleManager<IdentityRole> roleMan,
            SignInManager<IdentityUser> signMan)
        {
            this.userManager = userMan;
            this.roleManager = roleMan;
            this.signInManager = signMan;
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
            }
            else
            {
                ViewBag.profileFinished = true;
            }

            ViewBag.userData = currentUserData;

            if (System.IO.File.Exists($"{PictureProcessor.profileAbsolutePath}{currentUserData.UserFK}_small.jpg"))
            {
                ViewBag.picture = $"{currentUserData.UserFK}_small.jpg";
            }
            else
            {
                ViewBag.picture = "student_small.jpg";

            }


            ViewBag.User = User;
            return View();
        }

        public async Task<IActionResult> SubmitProfile(string fullname,
            string school,
            string grade,
            string town,
            string phonenum,
            string picturename,
            IFormFile file)
        {
            var currentUser = await userManager.GetUserAsync(this.User);
            var studentContext = new StudentDbContext();
            var currentProfile = await _GetCurrentUserProfile(studentContext);

            var newProfile = new UserProfile()
            {
                FullName = fullname,
                School = school,
                Grade = grade,
                Town = town,
                PhoneNum = phonenum,
                UserFK = currentUser.Id,
                //PictureName = "student_small.jpg"

            };

            if (currentProfile == null)
            {
                studentContext.Add(newProfile);
                await _AddStudent();
                await signInManager.SignOutAsync();
                studentContext.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                currentProfile.FullName = fullname;
                currentProfile.School = school;
                currentProfile.Grade = grade;
                currentProfile.Town = town;
                currentProfile.PhoneNum = phonenum;
                //currentProfile.PictureName = $"{currentProfile.UserFK}_small.jpg";

                if (file != null)
                {
                    await SavePictureAsync(file, $"{currentProfile.UserFK}.avatar");
                }

                await studentContext.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

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

        private async Task<IdentityResult> _AddStudent()
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
            return result;
        }
        public async Task<IActionResult> AddStudent()
        {
            var result = await _AddStudent();
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

        private async Task<UserProfile> _GetCurrentUserProfile(StudentDbContext userContext)
        {
            //var userContext = new StudentDbContext();
            var currentUser = await userManager.GetUserAsync(this.User);
            var currentProfile = userContext.UserProfiles.FirstOrDefault(u => u.UserFK == currentUser.Id);
            return currentProfile;
        }
        public async Task<IActionResult> EditMyPicture()
        {
            var studentContext = new StudentDbContext();
            var profileData = await _GetCurrentUserProfile(studentContext);
            if (profileData == null
                || System.IO.File.Exists($"{PictureProcessor.profileAbsolutePath}{profileData.UserFK}_small.jpg") == false)
            {
                ViewBag.PictureName = "student_small.jpg";
            }
            else
            {
                ViewBag.pictureName = $"{profileData.UserFK}_small.jpg";

            }
            return View();
        }

        private async Task SavePictureAsync(IFormFile inputFile, string fileName)
        {
            long size = inputFile.Length;

            string filePathAndName = "";

            if (inputFile.Length > 0)
            {
                filePathAndName = @$"{PictureProcessor.profileAbsolutePath}{fileName}";

                using (var stream = new FileStream(filePathAndName, FileMode.Create))
                {
                    await inputFile.CopyToAsync(stream);
                }
            }

            PictureProcessor.Resize($"{filePathAndName}");


        }


    }
}
