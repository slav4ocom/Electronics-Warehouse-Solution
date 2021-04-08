using CommonModels.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModels
{
    public class Homework
    {
        public int Id { get; private set; }
        //public int TaskId { get; set; }
        public HomeTask TaskId { get; set; }
        public string Notes { get; set; }
        public string PictureName { get; set; }
        public string OwnerUser { get; set; }
    }
}
