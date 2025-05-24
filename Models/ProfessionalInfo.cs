using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiApp.Services;

namespace SkiApp.Models
{
    public class ProfessionalInfo
    {
        public int UserId { get; set; }

        public double? Rating { get; set; }


        public int? TypeId { get; set; }


        public string? Loc { get; set; }

        public double? Price { get; set; }


        public string? Txt { get; set; }
        
        public int? RaterNum { get; set; }
        
        public bool? Post { get; set; }



        public string Name
        {
            get
            {
                List<VisitorInfo> visitors = ((App)Application.Current).Visitors;
                VisitorInfo visitor = visitors.Where(s => s.UserID == this.UserId).FirstOrDefault();
                if (visitor != null)
                    return visitor.Username;
                return "Unknown!";
            }
        }
        public List<string> PhotoPaths
        {
            get
            {
                List<PostPhotoInfo> postphotos = ((App)Application.Current).PostPhotos;
                postphotos = postphotos.Where(s => s.UserID == this.UserId).ToList();
                List<string> paths = new List<string>();

                if (postphotos != null)
                {
                    foreach (PostPhotoInfo p in postphotos)
                    {
                        paths.Add(p.FullURL);
                    }
                }
                return paths;
                return null;
            }
        }



    }
}
