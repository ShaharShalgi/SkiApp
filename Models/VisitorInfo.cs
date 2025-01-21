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
        public int? UserID { get; set; }
     
    }
}
