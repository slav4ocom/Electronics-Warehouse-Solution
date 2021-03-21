using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModels
{
    public class Article
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PartType { get; set; }
        public string PictureName { get; set; }

    }
}
