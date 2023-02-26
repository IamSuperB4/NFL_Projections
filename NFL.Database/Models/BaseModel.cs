using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Database.Models
{
    public class BaseModel
    {
        protected static bool IsUpdateRequired(string existingValue, string newValue)
        {
            return newValue != null && newValue != existingValue;
        }

        protected static bool IsUpdateRequired(bool existingValue, bool newValue)
        {
            return newValue != default && newValue != existingValue;
        }

        protected static bool IsUpdateRequired(bool? existingValue, bool? newValue)
        {
            return newValue != default && newValue != existingValue;
        }

        protected static bool IsUpdateRequired(int existingValue, int newValue)
        {
            return newValue != default && newValue != existingValue;
        }

        protected static bool IsUpdateRequired(int? existingValue, int? newValue)
        {
            return newValue != default && newValue != existingValue;
        }

        protected static bool IsUpdateRequired(decimal existingValue, decimal newValue)
        {
            return newValue != default && newValue != existingValue;
        }

        protected static bool IsUpdateRequired(decimal? existingValue, decimal? newValue)
        {
            return newValue != default && newValue != existingValue;
        }

        protected static bool IsUpdateRequired(DateTime? existingValue, DateTime? newValue)
        {
            return newValue != DateTime.MinValue && newValue != existingValue;
        }
    }
}
