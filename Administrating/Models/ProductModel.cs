using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Administrating.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="This field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public decimal Price { get; set; }
        public string Description { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Image is required")]
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Category { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

        public ProductModel()
        {
            ImagePath = "~/App_Files/Images/default-image.png";
        }
    }
}