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
        public string Lection { get; set; }
        public string TaskPicture { get; set; }
        public string TaskNotes { get; set; }
        public string SolutionPicture { get; set; }
        public string SolutionNotes { get; set; }
        public string UserFk { get; set; }
    }
}
