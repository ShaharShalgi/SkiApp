using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiApp.Services;

namespace SkiApp.Models
{
    public class PostPhotoInfo
    {
        public int PhotoId { get; set; }
        public string PhotoUrlPath { get; set; }
        public int? UserID { get; set; }
        public string FullURL
        {
            get
            {
                return SkiServiceWebAPIProxy.ImageBaseAddress + "/posts/" + PhotoUrlPath;
            }
        }
        

        public PostPhotoInfo() { }
    }
}
