using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime;

namespace FileUploadSample2.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int ProductCategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; } 
        public byte[] ProductPicture { get; set; }

        [NotMapped]
        public string ImageContentType { get; set; } = "image/png";

        public string GetInLineImgSource()
        {
            if (ProductPicture == null || ImageContentType == null)
            {
                return null;
            }
            var base64Image = Convert.ToBase64String(ProductPicture);
            return $"data:{ImageContentType};base64,{base64Image}";
        }

        [Display(Name = "Upload Product Picture")]
        [NotMapped]
        public IFormFile File
        {
            get
            {
                return null;
            }
            set
            {
                try
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        value.CopyTo(memoryStream);
                        ProductPicture = memoryStream.ToArray();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
