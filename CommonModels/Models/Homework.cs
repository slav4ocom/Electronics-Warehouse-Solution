using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModels
{
    public class Homework
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public string PictureName { get; set; }
        public string OwnerUser { get; set; }
    }
}
