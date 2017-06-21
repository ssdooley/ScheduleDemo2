using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.Data.Models
{
    public class Commitment
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Location { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Category Category { get; set; }

        public virtual ICollection<CommitmentPerson> CommitmentPeople { get; set; }
    }
}
