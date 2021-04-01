using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonModels.Models
{
    public class StudentProfile
    {
        public int Id { get; private set; }
        public string PictureName { get; set; }
        public string UserFK { get; set; }
    }
}
