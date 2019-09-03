using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Models;
using WebApplication4.Models.Request;

namespace WebApplication4.Services
{
    public interface IPersonServices
    {
        List<Person> GetAllPerson();
        Person GetPerson(Guid id);
        Person CreatePerson(PersonCreateRequest personCreateRequest);
        Person UpdatePerson(Guid id, PersonUpdateRequest personUpdateRequest);
        Task<bool> DeletePersonAsync(Guid id);
        
    }
}
