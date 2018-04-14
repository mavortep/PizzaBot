using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Administrating.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Category { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

        public ProductModel()
        {
            ImagePath = "~/App_Files/Images/default-image.png";
        }
    }
}