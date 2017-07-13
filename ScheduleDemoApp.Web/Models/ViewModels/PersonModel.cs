using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleDemoApp.Models.ViewModels
{
    public class PersonModel
    {
        public int id { get; set; }
        public int commitmentPersonId { get; set; }
        public string name { get; set; }
        public IEnumerable<CommitmentPersonModel> commitmentPeople { get; set; }
    }
}
