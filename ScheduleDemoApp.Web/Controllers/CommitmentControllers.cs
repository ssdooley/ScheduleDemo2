using Microsoft.AspNetCore.Mvc;
using Schedule.Data;
using ScheduleDemoApp.Models.Extensions;
using ScheduleDemoApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleDemoApp.Controllers
{
    [Route("api/[controller]")]
    public class CommitmentController : Controller
    {
        private AppDbContext db;

        public CommitmentController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<CommitmentModel>> GetCommitments()
        {
            return await db.GetCommitments();
        }

        [HttpGet("[action]/{id}")]
        public async Task<CommitmentModel> GetCommitment(int id)
        {
            return await db.GetCommitment(id);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IEnumerable<CommitmentModel>> GetPersonalCommitments(int id)
        {
            return await db.GetPersonalCommitments(id);
        }

        [HttpGet("[action]")]
        public async Task<ObjectResult> AddCommitment([FromBody]CommitmentModel model)
        {
            await db.AddCommitment(model);
            return Created("/api/Commitments/AddCommitment", model);
        }

        [HttpPost("[action]")]
        public async Task<ObjectResult> UpdateCommitment([FromBody]CommitmentModel model)
        {
            await db.UpdateCommitment(model);
            return Accepted("/api/Commitments/UpdateCommitment", model);
        }

        [HttpPost("[action]")]
        public async Task<ObjectResult> AddCommitmentPerson([FromBody]CommitmentPersonModel model)
        {
            await db.AddCommitmentPerson(model);
            return Created("/api/Commitments/AddCommitmentPerson", model);
        }

        [HttpPost("[action]")]
        public async Task<ObjectResult> DeleteCommitmentPerson([FromBody]int id)
        {
            await db.DeleteCommitmentPerson(id);
            return Accepted("/api/Commitments/DeleteCommitmentPerson", id);
        }

        [HttpPost("actiom")]
        public async Task<ObjectResult> DeleteCommitment([FromBody]int id)
        {
            await db.DeleteCommitment(id);
            return Accepted("/api/Commitments/DeleteCommitment", id);
        }
    }
}