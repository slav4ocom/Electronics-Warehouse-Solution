using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonModels;
using PictureProcessing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using CommonModels.Models;

namespace Web_Manager.Controllers
{
    [Authorize]
    public class HomeworkController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        public HomeworkController(UserManager<IdentityUser> userMan)
        {
            this.userManager = userMan;
        }


        [Authorize(Roles = "Teacher")]
        public IActionResult Index()
        {
            ViewBag.IAmTeacher = true;
            ViewBag.Title = "Всички домашни";
            ViewBag.User = "списък";
            var context = new StudentDbContext();
            var homeworks = context.Homeworks.ToList();
            var students = context.UserProfiles.ToList();
            
            ViewBag.MyHomeworks = homeworks;
            homeworks.ForEach(h => h.UserName = students.FirstOrDefault(s => s.UserFK == h.UserFk).FullName);
            homeworks.ForEach(h => h.UserId = students.FirstOrDefault(s => s.UserFK == h.UserFk).Id);
            homeworks.ForEach(h => h.UserPicture = $"{students.FirstOrDefault(s => s.UserFK == h.UserFk).UserFK}_small.jpg");
            return View();
        }

        [Route("Homework/AddHomeworkTask/{userId:int?}")]
        public IActionResult AddHomeworkTask(int? userId)
        {
            var context = new StudentDbContext();
            var student = context.UserProfiles.FirstOrDefault(p => p.Id == userId);

            if (student != null)
            {
                ViewBag.UserData = student;

            }
            else
            {
                ViewBag.UserData = "няма такъв студент";
            }
            return View();
        }

       
        [Route("Homework/Delete/{homeworkId:int}")]
        public IActionResult DeleteHomework(int homeworkId)
        {
            var context = new StudentDbContext();
            var homework = context.Homeworks.FirstOrDefault(n => n.Id == homeworkId);
            var owner = context.UserProfiles.FirstOrDefault(p => p.UserFK == homework.UserFk);
            context.Homeworks.Remove(homework);

            if (homework.TaskPicture != null)
            {
                System.IO.File.Delete($@"{PictureProcessor.homeworksPath}/{homework.UserFk}/{homework.TaskPicture}");

            }
            if (homework.SolutionPicture != null)
            {
                System.IO.File.Delete($@"{PictureProcessor.homeworksPath}/{homework.UserFk}/{homework.SolutionPicture}");

            }
            context.SaveChanges();

            return Redirect($@"~/Homework/MyHomeworks/{owner.Id}");
        }
        public async Task<IActionResult> SubmitHomeworkSolution(IFormFile file,
                                                            string tasknotes,
                                                            string lection,
                                                            int id)
        {
            var myContext = new StudentDbContext();
            //var myHomework = new Homework();
            var myHomework = myContext.Homeworks.FirstOrDefault(h => h.Id == id);

            var currentUser = await userManager.GetUserAsync(this.User);

            if (Directory.Exists($"{PictureProcessor.homeworksPath}{currentUser.Id}") == false)
            {
                Directory.CreateDirectory($"{PictureProcessor.homeworksPath}{currentUser.Id}");
            }

            await PictureProcessor.SaveFileAsync(file, $"{currentUser.Id}/{file.FileName}");

            myHomework.Lection = lection;
            myHomework.SolutionPicture = file.FileName;
            myHomework.SolutionNotes = tasknotes;
            await myContext.SaveChangesAsync();

            return RedirectToAction("MyHomeworks");
        }

        [Route("Homework/MyHomeworks/{userId:int?}")]
        public async Task<IActionResult> MyHomeworks(int? userId)
        {
            var currentUser = await userManager.GetUserAsync(this.User);
            var myContext = new StudentDbContext();
            UserProfile currentUserData = null;
            if (User.IsInRole("Teacher"))
            {
                ViewBag.IAmTeacher = true;
                ViewBag.Title = "Домашни на ученика";
            }
            else
            {
                ViewBag.IAmTeacher = false;
                ViewBag.Title = "Моите домашни";
            }


            if (currentUser != null)
            {
                if (userId == null)
                {
                    currentUserData = myContext.UserProfiles.FirstOrDefault(p => p.UserFK == currentUser.Id);

                }
                else
                {
                    currentUserData = myContext.UserProfiles.FirstOrDefault(p => p.Id == userId);
                    ViewBag.UserName = $"ученик {currentUserData.FullName}, клас {currentUserData.Grade} от {currentUserData.School}";
                }

            }


            if (currentUserData == null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Profile");
            }

            var myHomeworks = new List<Homework>();
            myHomeworks = myContext.Homeworks.Where(n => n.UserFk == currentUserData.UserFK).ToList();
            ViewBag.MyHomeworks = myHomeworks;
            ViewBag.UserData = currentUserData;
            return View();
        }

        public IActionResult ApplyHomework(int? id)
        {
            ViewBag.HomeworkId = id;
            return View();
        }

        public async Task<IActionResult> SubmitHomeworkTask(string userfk,
                                                            string lection,
                                                            string tasknotes,
                                                            IFormFile file)
        {
            var context = new StudentDbContext();
            var owner = context.UserProfiles.FirstOrDefault(p => p.UserFK == userfk);
            context.Add(new Homework
            {
                UserFk = userfk,
                TaskPicture = file.FileName,
                TaskNotes = tasknotes,
                Lection = lection
            });

            if (Directory.Exists($"{PictureProcessor.homeworksPath}{userfk}") == false)
            {
                Directory.CreateDirectory($"{PictureProcessor.homeworksPath}{userfk}");
            }

            await PictureProcessor.SaveFileAsync(file, $"{userfk}/{file.FileName}");

            await context.SaveChangesAsync();
            return Redirect($@"~/Homework/MyHomeworks/{owner.Id}");
        }

    }
}
