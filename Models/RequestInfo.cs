using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiApp.Models
{
    public class RequestInfo
    {
        public int RequestId { get; set; }


        public int? SenderId { get; set; }


        public int? RecieverId { get; set; }


        public int? StatusId { get; set; }
    }
}
