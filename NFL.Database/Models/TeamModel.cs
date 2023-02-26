using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NFL.Database.Models
{
    public class TeamModel : BaseModel
    {
        public int Id { get; set; }

        public string Location { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public int DivisionId { get; set; }

        public virtual DivisionModel Division { get; set; }

        public virtual List<GameModel> HomeGames { get; set; }

        public virtual List<GameModel> AwayGames { get; set; }


        /// <summary>
        /// Update the Teams table in the database with the model passed in 
        /// </summary>
        /// <param name="model">TeamModel to update in the database</param>
        /// <exception cref="NullReferenceException">If model is null</exception>
        /// <exception cref="InvalidOperationException">If new Team Id doesn't match actual model in database</exception>
        internal void UpdateModel(TeamModel model)
        {
            // validate
            if (model == null) throw new NullReferenceException("input model cannot be null");
            if (model.Id != Id) throw new InvalidOperationException("Id mismatch: incoming model and current model are different");
            if (model.Location == null) throw new NullReferenceException("Team Location model cannot be null");
            if (model.Name == null) throw new NullReferenceException("Team Name model cannot be null");
            if (model.FullName == null) throw new NullReferenceException("Team FullName model cannot be null");

            // update
            if (IsUpdateRequired(Location, model.Location)) Location = model.Location;
            if (IsUpdateRequired(Name, model.Name)) Name = model.Name;
            if (IsUpdateRequired(FullName, model.FullName)) FullName = model.FullName;
        }
    }
}
