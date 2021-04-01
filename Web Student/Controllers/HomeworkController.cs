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

        
        public async Task<IActionResult> Index()
        {
            ViewData["currentUser"]  = await userManager.GetUserAsync(this.User);
            return View();
        }
        public IActionResult AddArticle()
        {
            return View();
        }

        public IActionResult Edit(string id)
        {
            ViewData["Id"] = id;
            return View();
        }

        public IActionResult EditPart(string id, string partname, string picture, string price, string parttype)
        {

            var myContext = new StudentDbContext();
            var myArticle = myContext.Homeworks.FirstOrDefault(n => n.Id == int.Parse(id));
            myArticle.Name = partname;
            myArticle.PartType = parttype;
            myArticle.Price = decimal.Parse(price);

            myContext.SaveChanges();

            ViewData["Id"] = id;
            return View("Edit");
        }

        public IActionResult DeletePart(string id)
        {
            var myContext = new StudentDbContext();
            var myPart = myContext.Homeworks.FirstOrDefault(n => n.Id == int.Parse(id));
            myContext.Homeworks.Remove(myPart);
            myContext.SaveChanges();
            PictureProcessor.DeletePicture(myPart.PictureName);

            ViewData["Id"] = id;
            return View();
        }

        public async Task<IActionResult> SubmitPart(string partname, string href, string price, string parttype)
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
                Price = decimal.Parse(price),
                PartType = parttype,
                OwnerUser = user.UserName
            });

            myContext.SaveChanges();
            return View();
        }


    }
}
