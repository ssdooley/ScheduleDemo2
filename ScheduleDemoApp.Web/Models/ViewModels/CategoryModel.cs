using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleDemoApp.Models.ViewModels
{
    public class CategoryModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public IEnumerable<CommitmentModel> commitments { get; set; }
    }
}
