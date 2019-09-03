using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models.Request
{
    public class CredentialCreateRequest
    {
        public CredentialCreateRequest(string fullname, string password)
        {
            Fullname = fullname;
            Password = password;
        }
        public CredentialCreateRequest() { }
        public string Fullname { get; set; }
        public string Password { get; set; }
    }
}
