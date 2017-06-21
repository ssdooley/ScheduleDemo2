using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleDemoApp.Models.ViewModels
{
    public class CommitmentPersonModel
    {
        public int id { get; set; }
        public CommitmentModel commitment { get; set; }
        public PersonModel person { get; set; }
    }
}
