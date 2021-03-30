using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonModels;
using PictureProcessing;

namespace Web_Manager.Controllers
{
    public class ManageController : Controller
    {
        public IActionResult Index()
        {
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
            var myContext = new ElectronicsWarehouseContext();
            var myArticle = myContext.Articles.FirstOrDefault(n => n.Id == int.Parse(id));
            myArticle.Name = partname;
            myArticle.PartType = parttype;
            myArticle.Price = decimal.Parse(price);

            myContext.SaveChanges();

            ViewData["Id"] = id;
            return View("Edit");
        }

        public IActionResult DeletePart(string id)
        {
            var myContext = new ElectronicsWarehouseContext();
            var myPart = myContext.Articles.FirstOrDefault(n => n.Id == int.Parse(id));
            myContext.Articles.Remove(myPart);
            myContext.SaveChanges();
            PictureProcessor.DeletePicture(myPart.PictureName);

            ViewData["Id"] = id;
            return View();
        }

        public IActionResult SubmitPart(string partname, string href, string price, string parttype)
        {
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

            var myContext = new ElectronicsWarehouseContext();
            myContext.Articles.Add(new Article()
            {
                Name = partname,
                PictureName = $"{fileName}.{fileExtension}",
                Price = decimal.Parse(price),
                PartType = parttype
            });

            myContext.SaveChanges();
            return View();
        }


    }
}
