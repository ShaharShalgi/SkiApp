using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiApp.Models
{
    public class ReviewInfo
    {
        public int ReviewId { get; set; }


        public int? RecieverId { get; set; }


        public int? SenderId { get; set; }

        public int? Rating { get; set; }


        public string? Txt { get; set; }
        public string? Title { get; set; }

        public ReviewInfo()
        {
            Photos = new List<ReviewPhotoInfo>();
        }
        public List<ReviewPhotoInfo> Photos { get; set; } = null;

        public string PhotoURL
        {
            get
            {
                if (Photos != null && Photos.Count > 0)
                    return Photos[0].FullURL;
                return "";
            }
        }
        public List<string> PhotoPaths
        {
            get
            {
                List<ReviewPhotoInfo> reviewphotos = ((App)Application.Current).ReviewPhotos;
                reviewphotos = reviewphotos.Where(s => s.ReviewID == this.ReviewId).ToList();
                List<string> paths = new List<string>();

                if (reviewphotos != null)
                {
                    foreach (ReviewPhotoInfo p in reviewphotos)
                    {
                        paths.Add(p.FullURL);
                    }
                }
                return paths;
                return null;
            }
        }
        public string ReviewerName
        {
            get
            {
                List<VisitorInfo> visitors = ((App)Application.Current).Visitors;
                VisitorInfo visitor = visitors.Where(s => s.UserID == this.SenderId).FirstOrDefault();
                if (visitor != null)
                    return visitor.Username;
                return "Unknown!";
            }
        }
    }
}
