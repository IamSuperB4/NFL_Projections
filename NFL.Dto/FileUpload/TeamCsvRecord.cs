using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Dto
{
    /// <summary>
    /// Object to store information in the teams CSV file uploaded from the Admin Page
    /// </summary>
    public class TeamCsvRecord
    {
        [CsvHelper.Configuration.Attributes.Ignore]
        public int LineNumber { get; set; }

        [CsvHelper.Configuration.Attributes.Ignore]
        public string Location { get; set; }

        [CsvHelper.Configuration.Attributes.Optional]
        public string Name { get; set; }

        [CsvHelper.Configuration.Attributes.Optional]
        public string Division { get; set; }

        [CsvHelper.Configuration.Attributes.Optional]
        public int Season { get; set; }

        [CsvHelper.Configuration.Attributes.Ignore]
        public List<string> ActivityLog { get; set; } = new List<string>();
    }
}
