using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
