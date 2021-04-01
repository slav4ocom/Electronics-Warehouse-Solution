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
        public decimal Price { get; set; }
        public string PartType { get; set; }
        public string PictureName { get; set; }
        //public IdentityUser OwnerUser { get; set; }
        public string OwnerUser { get; set; }
    }
}
