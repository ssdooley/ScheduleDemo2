using Microsoft.AspNetCore.Mvc;
using Schedule.Data;
using ScheduleDemoApp.Models.Extensions;
using ScheduleDemoApp.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleDemoApp.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        private AppDbContext db;

        public PeopleController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonModel>> GetPeople()
        {
            return await db.GetPeople();
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonModel>> GetSimplePeople()
        {
            return await db.GetSimplePeople();
        }

        [HttpGet("[action]/{id}")]
        public async Task<PersonModel> GetPerson([FromRoute]int id)
        {
            return await db.GetPerson(id);
        }

        [HttpGet("[action]/{id}")]
        public async Task<PersonModel> GetSimplePerson([FromRoute]int id)
        {
            return await db.GetSimplePerson(id);
        }

        [HttpPost("[action]")]
        public async Task<ObjectResult> AddPerson([FromBody]PersonModel model)
        {
            await db.AddPerson(model);
            return Created("/api/People/AddPerson", model);
        }

        [HttpPost("[action]")]
        public async Task<ObjectResult> UpdatePerson([FromBody]PersonModel model)
        {
            await db.UpdatePerson(model);
            return Accepted("/api/People/UpdatePerson", model);
        }

        [HttpPost("[action]")]
        public async Task<ObjectResult> DeletePerson([FromBody]int id)
        {
            await db.DeletePerson(id);
            return Accepted("/api/People/DeletePerson", id);
        }
    }
}
