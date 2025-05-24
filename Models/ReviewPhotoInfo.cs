using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiApp.Services;

namespace SkiApp.Models
{
    public class ReviewPhotoInfo
    {
        public int PhotoId { get; set; }
        public string PhotoUrlPath { get; set; }
        public int? ReviewID { get; set; }
        public string FullURL
        {
            get
            {
                return SkiServiceWebAPIProxy.ImageBaseAddress + "/reviews/" + PhotoUrlPath;
            }
        }


        public ReviewPhotoInfo() { }
    }
}
