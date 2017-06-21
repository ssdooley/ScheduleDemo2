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
    public static class PeopleExtensions
    {
        public static Task<IEnumerable<PersonModel>> GetPeople(this AppDbContext db)
        {
            return Task.Run(() =>
            {
                var model = db.People.Include(x => x.CommitmentPeople.Select(y => y.Commitment.Category)).Select(x => new PersonModel
                {
                    id = x.Id,
                    name = x.Name,
                    commitmentPeople = x.CommitmentPeople.Select(y => new CommitmentPersonModel
                    {
                        id = y.Id,
                        commitment = new CommitmentModel
                        {
                            id = y.CommitmentId,
                            startDate = y.Commitment.StartDate,
                            endDate = y.Commitment.EndDate,
                            subject = y.Commitment.Subject,
                            body = y.Commitment.Body,
                            location = y.Commitment.Location,
                            category = new CategoryModel
                            {
                                id = y.Commitment.CategoryId,
                                name = y.Commitment.Category.Name
                            }
                        }
                    }).OrderBy(y => y.commitment.startDate).AsEnumerable()
                }).OrderBy(x => x.name).AsEnumerable();

                return model;
            });
        }

        public static Task<IEnumerable<PersonModel>> GetSimplePeople(this AppDbContext db)
        {
            return Task.Run(() =>
            {
                var model = db.People.Select(x => new PersonModel
                {
                    id = x.Id,
                    name = x.Name
                }).OrderBy(x => x.name).AsEnumerable();

                return model;
            });
        }

        public static async Task<PersonModel> GetPerson(this AppDbContext db, int id)
        {
            var person = await db.People.Include(x => x.CommitmentPeople.Select(y => y.Commitment.Category)).FirstOrDefaultAsync(x => x.Id == id);

            var model = new PersonModel
            {
                id = person.Id,
                name = person.Name,
                commitmentPeople = person.CommitmentPeople.Select(y => new CommitmentPersonModel
                {
                    id = y.Id,
                    commitment = new CommitmentModel
                    {
                        id = y.CommitmentId,
                        startDate = y.Commitment.StartDate,
                        endDate = y.Commitment.EndDate,
                        subject = y.Commitment.Subject,
                        body = y.Commitment.Body,
                        location = y.Commitment.Location,
                        category = new CategoryModel
                        {
                            id = y.Commitment.CategoryId,
                            name = y.Commitment.Category.Name
                        }
                    }
                }).OrderBy(y => y.commitment.startDate).AsEnumerable()
            };

            return model;
        }

        public static async Task<PersonModel> GetSimplePerson(this AppDbContext db, int id)
        {
            var person = await db.People.FindAsync(id);

            var model = new PersonModel
            {
                id = person.Id,
                name = person.Name
            };

            return model;
        }

        public static async Task AddPerson(this AppDbContext db, PersonModel model)
        {
            if (await model.Validate(db))
            {
                var person = new Person
                {
                    Name = model.name
                };

                await db.People.AddAsync(person);
                await db.SaveChangesAsync();
            }
        }

        public static async Task UpdatePerson(this AppDbContext db, PersonModel model)
        {
            if (await model.Validate(db))
            {
                var person = await db.People.FindAsync(model.id);
                person.Name = model.name;
                await db.SaveChangesAsync();
            }
        }

        public static async Task DeletePerson(this AppDbContext db, int id)
        {
            var person = await db.People.FindAsync(id);
            db.People.Remove(person);
            await db.SaveChangesAsync();
        }

        public static async Task<bool> Validate(this PersonModel model, AppDbContext db)
        {
            if (string.IsNullOrEmpty(model.name))
            {
                throw new Exception("The provided person must have a name");
            }

            if (model.id > 0)
            {
                var check = await db.People.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(model.name.ToLower()) && !(x.Id == model.id));

                if (check != null)
                {
                    throw new Exception("The specified person already exists");
                }
            }
            else
            {
                var check = await db.People.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(model.name.ToLower()));

                if (check != null)
                {
                    throw new Exception("The specified person already exists");
                }
            }

            return true;
        }
    }
}
