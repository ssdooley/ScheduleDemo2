using Microsoft.EntityFrameworkCore;
using Schedule.Data;
using Schedule.Data.Models;
using ScheduleDemoApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleDemoApp.Models.Extensions
{
    public static class CommitmentExtensions
    {
        private static readonly DateTime checkDate = new DateTime(1990, 01, 01, 00, 00, 00);

        public static Task<IEnumerable<CommitmentModel>> GetCommitments(this AppDbContext db)
        {
            return Task.Run(() =>
            {
                var model = db.Commitments
                .Include(x => x.CommitmentPeople)
                .Include(x => x.Category)
                .Select(x => x.CastToCommitmentModel()).OrderBy(x => x.startDate).AsEnumerable();

                return model;
            });
        }

        private static CommitmentModel CastToCommitmentModel(this Commitment commitment)
        {
            var model = new CommitmentModel
            {
                id = commitment.Id,
                body = commitment.Body,
                subject = commitment.Subject,
                startDate = commitment.StartDate,
                endDate = commitment.EndDate,
                location = commitment.Location,
                category = new CategoryModel
                {
                    id = commitment.CategoryId,
                    name = commitment.Category.Name
                },
                people = commitment.CommitmentPeople.Select(y => new PersonModel
                {
                    id = y.PersonId,
                    commitmentPersonId = y.Id,
                    name = y.Person.Name
                }).OrderBy(y => y.name).AsEnumerable()
            };

            return model;
        }
        
        public static async Task<IEnumerable<CommitmentModel>> GetPersonalCommitments(this AppDbContext db, int id)
        {
            var commitmentIds = await db.CommitmentPeople.Where(x => x.PersonId == id).Select(x => x.CommitmentId).Distinct().ToListAsync();

            var model = db.Commitments.Include(x => x.CommitmentPeople)
                .Include(x => x.Category)
                .Where(x => commitmentIds.Contains(x.Id))
                .Select(x => x.CastToCommitmentModel()).OrderBy(x => x.startDate).AsEnumerable();

            return model;
        }

        public static async Task<CommitmentModel> GetCommitment(this AppDbContext db, int id)
        {
            var commitment = await db.Commitments
                .Include(x => x.CommitmentPeople)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            return commitment.CastToCommitmentModel();
        }

        public static async Task AddCommitment(this AppDbContext db, CommitmentModel model)
        {
            if (await model.Validate(db))
            {
                var commitment = new Commitment
                {
                    Subject = model.subject,
                    Location = model.location,
                    Body = model.body,
                    StartDate = model.startDate,
                    EndDate = model.endDate,
                    CategoryId = model.category.id
                };

                db.Commitments.Add(commitment);
                await db.SaveChangesAsync();

                foreach (var person in model.people)
                {
                    var commitmentPerson = new CommitmentPerson
                    {
                        CommitmentId = commitment.Id,
                        PersonId = person.id
                    };

                    db.CommitmentPeople.Add(commitmentPerson);
                }

                await db.SaveChangesAsync();
            }
        }

        public static async Task UpdateCommitment(this AppDbContext db, CommitmentModel model)
        {
            if (await model.Validate(db))
            {
                var commitment = await db.Commitments.FindAsync(model.id);
                commitment.Subject = model.subject;
                commitment.Location = model.location;
                commitment.Body = model.body;
                commitment.StartDate = model.startDate;
                commitment.EndDate = model.endDate;

                await db.SaveChangesAsync();
            };
        }

        public static async Task DeleteCommitment(this AppDbContext db, int id)
        {
            var commitmentPeople = db.CommitmentPeople.Where(x => x.CommitmentId == id);
            db.CommitmentPeople.RemoveRange(commitmentPeople);
            await db.SaveChangesAsync();

            var commitment = await db.Commitments.FindAsync(id);
            db.Commitments.Remove(commitment);
            await db.SaveChangesAsync();
        }

        //public static async Task UpdateCommitmentPerson(this AppDbContext db, CommitmentPersonModel model)
        //{
        //    if (await model.ValidateCommitmentPerson(db))
        //    {
        //        var person = await db.CommitmentPeople.FindAsync(model.id);
        //        person.Id = model.id;

        //        await db.SaveChangesAsync();
        //    }
        //}

        public static async Task AddCommitmentPerson(this AppDbContext db, CommitmentPersonModel model)
        {
            if (await model.ValidateCommitmentPerson(db))
            {
                var person = new CommitmentPerson
                {
                    CommitmentId = model.commitment.id,
                    PersonId = model.person.id
                };

                db.CommitmentPeople.Add(person);
                await db.SaveChangesAsync();
            }
        }

        public static async Task DeleteCommitmentPerson(this AppDbContext db, int id)
        {
            var person = await db.CommitmentPeople.FindAsync(id);
            db.CommitmentPeople.Remove(person);
            await db.SaveChangesAsync();
        }


        public static Task<bool> Validate(this CommitmentModel model, AppDbContext db)
        {
            return Task.Run(() =>
            {
                if (string.IsNullOrEmpty(model.location))
                {
                    throw new Exception("The commitment must have a location");
                }

                if (model.people.Count() < 1)
                {
                    throw new Exception("A person must be associated with each commitment");
                }

                if (model.startDate > model.endDate)
                {
                    throw new Exception("Start date must occur before End date");
                }

                if (model.endDate < model.startDate)
                {
                    throw new Exception("End date must occur after the Start date");
                }

                //if (model.startDate <= checkDate)
                //{
                //    throw new Exception("Please select the correct Start date");
                //}

                //if (model.endDate <= checkDate)
                //{
                //    throw new Exception("Please select the correct Start date");
                //}
                if (model.category == null)
                {
                    throw new Exception("The commitment must be associated with a category");
                }

                if (string.IsNullOrEmpty(model.subject))
                {
                    throw new Exception("The commitment must have a subject");
                }

                return true;
            });
        }

        public static async Task<bool> ValidateCommitmentPerson(this CommitmentPersonModel model, AppDbContext db)
        {
            if (string.IsNullOrEmpty(model.person.name))
            {
                throw new Exception("the provided person must have a name");
            }

            if (model.id > 0)
            {
                var check = await db.CommitmentPeople.FirstOrDefaultAsync(x => x.PersonId == model.person.id && !(x.Id == model.id));

                if (check != null)
                {
                    throw new Exception("The specified persona already exists");
                }
            }
            else
            {
                var check = await db.CommitmentPeople.FirstOrDefaultAsync(x => x.PersonId == model.person.id);

                if (check != null)
                {
                    throw new Exception("The specified person already exists");
                }
            }



            return true;
        }
    }
}