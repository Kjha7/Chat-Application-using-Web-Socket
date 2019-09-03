using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models.Response;
using WebApplication4.Models;
using WebApplication4.Models.Request;
using WebApplication4.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApplication4.Controllers
{
    public class PersonController : Controller
    {
        private IPersonServices personServices;
        public PersonController(PersonServices _personServices)
        {
            personServices = _personServices;
        }

        // GET: Person
        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetAll()
        {
            return personServices.GetAllPerson();
        }

        // GET: Person/Details/5
        [HttpGet]
        public ActionResult<Person> Get(Guid id)
        {
            return personServices.GetPerson(id);
        }
        
        [HttpPost]
        public ActionResult<Person> Create([FromBody]PersonCreateRequest personCreateRequest)
        {
            return personServices.CreatePerson(personCreateRequest);
        }
        
        [HttpPut]
        public ActionResult<Person> Edit(Guid id, [FromBody]PersonUpdateRequest personUpdate)
        {
            return personServices.UpdatePerson(id, personUpdate);
        }
        
        // GET: Person/Delete/5
        [HttpDelete]
        public Task<bool> Delete(Guid id)
        {
            return personServices.DeletePersonAsync(id);
        }
        
    }
}