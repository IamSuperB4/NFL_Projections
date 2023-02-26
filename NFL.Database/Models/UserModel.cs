using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Database.Models
{
    public class UserModel : BaseModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string? Money { get; set; }

        public virtual List<GameModel> Games { get; set; }


        private static readonly string[] validStatus = new string[] { "actual", "season-long", "playoff-pool", "bet" };

        /// <summary>
        /// If status is valid ("ok", "not-ok", "unknown", "re-scan", "new")
        /// </summary>
        /// <param name="value">Database status of chassis</param>
        /// <returns>True/False is valid status</returns>
        private static bool IsValidUserType(string value)
        {
            return (value != null) && validStatus.Contains(value);
        }


        /// <summary>
        /// Update the Users table in the database with the model passed in 
        /// </summary>
        /// <param name="model">UserModel to update in the database</param>
        /// <exception cref="NullReferenceException">If model is null</exception>
        /// <exception cref="InvalidOperationException">If new Team Id doesn't match actual model in database</exception>
        /// <exception cref="ArgumentNullException">If one of the types is null</exception>
        /// <exception cref="ArgumentException">If one of the types is invalid (must be one of these: "actual", "season-long", "playoff-pool", or "bet") </exception>
        internal void UpdateModel(UserModel model)
        {
            // validate
            if (model == null) throw new NullReferenceException("input model cannot be null");
            if (model.Id != Id) throw new InvalidOperationException("Id mismatch: incoming model and current model are different");
            if (model.Name == null) throw new NullReferenceException("User Name model cannot be null");
            if (model.Type == null) throw new NullReferenceException("User Type model cannot be null");
            if (IsValidUserType(model.Type) == false) throw new ArgumentException($"'{model.Type}' is not valid for user type");

            // update
            if (IsUpdateRequired(Name, model.Name)) Name = model.Name;
            if (IsUpdateRequired(Type, model.Type)) Type = model.Type;
            if (IsUpdateRequired(Money, model.Money)) Money = model.Money;
        }
    }
}
