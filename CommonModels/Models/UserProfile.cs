using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonModels.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string School { get; set; }
        public string Grade { get; set; }
        public string Town { get; set; }
        public string PhoneNum { get; set; }
        public string UserFK { get; set; }
        [NotMapped]
        public string Picture { get; set; }
    }
}
