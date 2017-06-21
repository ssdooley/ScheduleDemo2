using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.Data.Models
{
    public class CommitmentPerson
    {
        public int Id { get; set; }
        public int CommitmentId { get; set; }
        public int PersonId { get; set; }

        public Commitment Commitment { get; set; }
        public Person Person { get; set; }
    }
}
