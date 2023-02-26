using NFL.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Database.Models
{
    public class ConferenceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<DivisionModel> Divisions { get; set; }
    }
}
