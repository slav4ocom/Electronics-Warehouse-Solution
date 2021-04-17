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
        public async Task<IActionResult> Index()
        {

            return View();
        }
        public IActionResult AddHomework()
        {
            return View();
        }

        public IActionResult Edit(string id)
        {
            ViewData["Id"] = id;
            return View();
        }

        public IActionResult DeleteHomework(string id)
        {
            var myContext = new StudentDbContext();
            var myPart = myContext.Homeworks.FirstOrDefault(n => n.Id == int.Parse(id));
            myContext.Homeworks.Remove(myPart);
            myContext.SaveChanges();
            PictureProcessor.DeletePicture(myPart.PictureName);

            ViewData["Id"] = id;
            return View();
        }
        public async Task<IActionResult> SubmitMyHomework(IFormFile file, string notes)
        {
            var currentUser = await userManager.GetUserAsync(this.User);

            if (Directory.Exists($"{PictureProcessor.homeworksPath}{currentUser.Id}") == false)
            {
                Directory.CreateDirectory($"{PictureProcessor.homeworksPath}{currentUser.Id}");
            }

            await PictureProcessor.SaveFileAsync(file, $"{currentUser.Id}/{file.FileName}");
        
            return View();
        }


        public IActionResult MyHomeworks()
        {
            ViewData["Tasks"] = new string[] {
                "Снимай патриаршията !",
                "Снимай Марийка под гащите.",
                "Загради квадратчетата с кръгчета",
                "Напиши стихотворение за птичките",
                "Направи снимка на небето"
            };
            return View();
        }

        public IActionResult ApplyHomework()
        {
            return View();
        }

    }
}
