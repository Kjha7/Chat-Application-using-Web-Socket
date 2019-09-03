using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models.Request
{
    public class PersonCreateRequest
    {
        public string Fullname { get; set; }
        public string EmailId { get; set; }
        public int? Age { get; set; }
        public string Password { get; set; }
    }
}
