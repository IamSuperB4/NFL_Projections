using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Database.Models
{
    public class DivisionModel : BaseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ConferenceId { get; set; }

        public int SeasonId { get; set; }

        public virtual ConferenceModel Conference { get; set; }

        public virtual SeasonModel Season { get; set; }

        public virtual List<TeamModel> Teams { get; set; }


        /// <summary>
        /// Update the Divisions table in the database with the model passed in 
        /// </summary>
        /// <param name="model">DivisionModel to update in the database</param>
        /// <exception cref="NullReferenceException">If model is null</exception>
        /// <exception cref="InvalidOperationException">If new Team Id doesn't match actual model in database</exception>
        internal void UpdateModel(DivisionModel model)
        {
            // validate
            if (model == null) throw new NullReferenceException("input model cannot be null");
            if (model.Id != Id) throw new InvalidOperationException("Id mismatch: incoming model and current model are different");
            if (model.Name == null) throw new NullReferenceException("Division Name model cannot be null");

            // update
            if (IsUpdateRequired(Name, model.Name)) Name = model.Name;
        }
    }
}
