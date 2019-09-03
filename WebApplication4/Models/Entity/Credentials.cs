using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Models.Request;

namespace WebApplication4.Models.Entity
{
    [BsonIgnoreExtraElements]
    public class Credentials
    {
        public Credentials(CredentialCreateRequest credential)
        {
            Fullname = credential.Fullname;
            Password = credential.Password;
        }

        public string Fullname { get; set; }
        public string Password { get; set; }
    }
}
