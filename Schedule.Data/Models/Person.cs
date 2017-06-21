using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.Data.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CommitmentPerson> CommitmentPeople { get; set; }
    }
}