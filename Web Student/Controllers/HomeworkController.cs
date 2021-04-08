using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonModels;
using PictureProcessing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

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
            ViewData["currentUser"]  = await userManager.GetUserAsync(this.User);
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

        /*public IActionResult EditPart(string id, string partname, string picture, string price, string parttype)
        {

            var myContext = new StudentDbContext();
            var myArticle = myContext.Homeworks.FirstOrDefault(n => n.Id == int.Parse(id));
            myArticle.Name = partname;
            myArticle.WorkType = parttype;
            myArticle.Points = decimal.Parse(price);

            myContext.SaveChanges();

            ViewData["Id"] = id;
            return View("Edit");
        }*/

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

        /*public async Task<IActionResult> SubmitHomework(string partname, string href, string price, string parttype)
        {
            var user = await userManager.GetUserAsync(this.User);

            new PictureProcessor().Download(href);
            var fileName = href
                .Split(new string[] { "\\", "/" }, StringSplitOptions.RemoveEmptyEntries)
                .Last()
                .Split(".")
                .First()
                .Replace("%", "_");

            var fileExtension = href
                .Split(new string[] { "\\", "/" }, StringSplitOptions.RemoveEmptyEntries)
                .Last()
                .Split(".")
                .Last();

            var myContext = new StudentDbContext();
            myContext.Homeworks.Add(new Homework()
            {
                Name = partname,
                PictureName = $"{fileName}.{fileExtension}",
                Points = decimal.Parse(price),
                WorkType = parttype,
                OwnerUser = user.UserName
            });

            myContext.SaveChanges();
            return View();
        }*/

        public async Task<IActionResult> SubmitMyHomework(string name, string href, string notes)
        {
            var user = await userManager.GetUserAsync(this.User);

            new PictureProcessor().Download(href);
            var fileName = href
                .Split(new string[] { "\\", "/" }, StringSplitOptions.RemoveEmptyEntries)
                .Last()
                .Split(".")
                .First()
                .Replace("%", "_");

            var fileExtension = href
                .Split(new string[] { "\\", "/" }, StringSplitOptions.RemoveEmptyEntries)
                .Last()
                .Split(".")
                .Last();

            var myContext = new StudentDbContext();
            myContext.Homeworks.Add(new Homework()
            {
                PictureName = $"{fileName}.{fileExtension}",
                Notes = notes,
                OwnerUser = user.UserName
            });

            myContext.SaveChanges();
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
