using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models.Request
{
    public class PersonUpdateRequest
    {
        public string Fullname { get; set; }
        public string EmailId { get; set; }
        public int? Age { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
