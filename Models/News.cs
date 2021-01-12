using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace MvcNewsFlash.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public string Description { get; set; }

        [Display(Name = "Image Path")]
        public string ImagePath { get; set; }

        [NotMapped]
        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }
    }
}
