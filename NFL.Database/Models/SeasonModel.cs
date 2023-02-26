using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Database.Models
{
    public class SeasonModel : BaseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public int RegularSeasonWeekCount { get; set; }

        public int PlayoffTeams { get; set; }

        public virtual List<DivisionModel> Divisions { get; set; }


        /// <summary>
        /// Update the Seasons table in the database with the model passed in 
        /// </summary>
        /// <param name="model">SeasonModel to update in the database</param>
        /// <exception cref="NullReferenceException">If model is null</exception>
        /// <exception cref="InvalidOperationException">If new Team Id doesn't match actual model in database</exception>
        internal void UpdateModel(SeasonModel model)
        {
            // validate
            if (model == null) throw new NullReferenceException("input model cannot be null");
            if (model.Id != Id) throw new InvalidOperationException("Id mismatch: incoming model and current model are different");
            if (model.Name == null) throw new NullReferenceException("Season Name model cannot be null");

            // update
            if (IsUpdateRequired(Name, model.Name)) Name = model.Name;
            if (IsUpdateRequired(Year, model.Year)) Year = model.Year;
            if (IsUpdateRequired(RegularSeasonWeekCount, model.RegularSeasonWeekCount)) RegularSeasonWeekCount = model.RegularSeasonWeekCount;
            if (IsUpdateRequired(PlayoffTeams, model.PlayoffTeams)) PlayoffTeams = model.PlayoffTeams;
        }
    }
}
