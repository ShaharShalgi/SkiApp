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

        public bool? IsPositive { get; set; }


        public string? Txt { get; set; }
    }
}
