using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Models;
using WebApplication4.Models.Request;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using WebApplication4.Configs;
using WebApplication4.Models.Entity;

namespace WebApplication4.Services
{
    public class PersonServices : IPersonServices
    {
        private MongoDbConfigs Personconfig;
        private LoginConfig loginconfig;
        private IMongoCollection<Person> _person;
        private IMongoCollection<Credentials> _cred;
        public PersonServices(IOptions<MongoDbConfigs> options, IOptions<LoginConfig> loginoptions )
        {
            Personconfig = options.Value;
            var client = new MongoClient(Personconfig.Uri);
            var database = client.GetDatabase(Personconfig.Database);
            _person = database.GetCollection<Person>(Personconfig.Collection);
            loginconfig = loginoptions.Value;
            var Loginclient = new MongoClient(loginconfig.Uri);
            var Logindatabase = client.GetDatabase(loginconfig.Database);
            _cred = database.GetCollection<Credentials>(loginconfig.Collection);
        }


        public Person CreatePerson( PersonCreateRequest personCreateRequest)
        {
            try
            {
                var Person = new Person(personCreateRequest);
                var cred = new Credentials(new CredentialCreateRequest(personCreateRequest.Fullname, personCreateRequest.Password));
                _cred.InsertOneAsync(cred).Wait();
                _person.InsertOneAsync(Person).Wait();
                return Person;
            }
            catch { throw; }
        }

        public async Task<bool> DeletePersonAsync(Guid id)
        {
            try
            {
                DeleteResult deleteResult = await _person.DeleteOneAsync(Builders<Person>.Filter.Eq("PersonId", id));
                return deleteResult.IsAcknowledged;
            }
            catch { throw; }
        }

        public List<Person> GetAllPerson()
        {
            return _person.Find(Person => true).ToList();
        }

        public Person GetPerson(Guid id)
        {
            try
            {
                var person =  _person.Find(p => p.personId == id).FirstOrDefault();
                return person;
            }
            catch { throw; }
        }

        public Person UpdatePerson(Guid id, PersonUpdateRequest personUpdateRequest)
        {
            try
            {
                DateTime updatedAt = DateTime.UtcNow;
                var person =  _person.FindOneAndUpdateAsync(p => p.personId == id, Person.UpdateBuilder(personUpdateRequest, updatedAt)).Result;
                return person;
            }
            catch { throw; }
        }
    }
}
