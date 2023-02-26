using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Dto
{
    /// <summary>
    /// Object to store information in the games CSV file uploaded from the Admin Page
    /// </summary>
    public class GameCsvRecord
    {
        [CsvHelper.Configuration.Attributes.Ignore]
        public int LineNumber { get; set; }

        [CsvHelper.Configuration.Attributes.Ignore]
        public int Week { get; set; }

        [CsvHelper.Configuration.Attributes.Optional, CsvHelper.Configuration.Attributes.Name("Day")]
        public string DayOfWeek { get; set; }

        [CsvHelper.Configuration.Attributes.Optional]
        public string Date { get; set; }

        [CsvHelper.Configuration.Attributes.Optional]
        public string Time { get; set; }

        [CsvHelper.Configuration.Attributes.Optional, CsvHelper.Configuration.Attributes.Name("Winner/tie")]
        public string Winner { get; set; }

        [CsvHelper.Configuration.Attributes.Optional, CsvHelper.Configuration.Attributes.Name("At")]
        public string Location { get; set; }

        [CsvHelper.Configuration.Attributes.Optional, CsvHelper.Configuration.Attributes.Name("Loser/tie")]
        public string Loser { get; set; }

        [CsvHelper.Configuration.Attributes.Optional, CsvHelper.Configuration.Attributes.Name("PtsW")]
        public int PointsWinner { get; set; }

        [CsvHelper.Configuration.Attributes.Optional, CsvHelper.Configuration.Attributes.Name("PtsL")]
        public int? PointsLoser { get; set; }

        [CsvHelper.Configuration.Attributes.Ignore]
        public List<string> ActivityLog { get; set; } = new List<string>();
    }
}
