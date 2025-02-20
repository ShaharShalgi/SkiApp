using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiApp.Models
{
    public class VisitorInfo
    {
        public string Username { get; set; } = null;
        public string Pass { get; set; } = null;
        public string Gender { get; set; } = null;
        public string Email { get; set; } = null;
        public int UserID { get; set; } = 0;
        public bool IsPro {  get; set; }
        public VisitorInfo()
        {
            Photos = new List<PostPhotoInfo>();
        }
        public List<PostPhotoInfo> Photos { get; set; } = null;

        public string PhotoURL
        {
            get
            {
                if (Photos != null && Photos.Count > 0)
                    return Photos[0].FullURL;
                return "";
            }
        }

    }
}
