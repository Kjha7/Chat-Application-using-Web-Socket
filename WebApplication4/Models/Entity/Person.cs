using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using WebApplication4.Models.Request;

namespace WebApplication4.Models
{
    public class Person
    {
        [BsonId]
        public Guid personId;
        public string Fullname { get; set; }
        public string EmailId { get; set; }
        public int? Age { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Password { get; set; }

        public Person(PersonCreateRequest personCreateRequest) {
            personId = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Age = personCreateRequest.Age;
            EmailId =  personCreateRequest.EmailId;
            Fullname = personCreateRequest.Fullname;
            Password = personCreateRequest.Password;
        }

        public static UpdateDefinition<Person> UpdateBuilder(PersonUpdateRequest personUpdateRequest, DateTime updatedAt)
        {
            return Builders<Person>.Update
                .Set(p => p.Fullname, personUpdateRequest.Fullname)
                .Set(p => p.EmailId, personUpdateRequest.EmailId)
                .Set(p => p.Age, personUpdateRequest.Age)
                .Set(p => p.UpdatedAt, updatedAt);
        }

    }
}
