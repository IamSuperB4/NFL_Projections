using NFL.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Business
{
    public interface IGamesService
    {
        Task PopulateDatabase();

        Task<List<DivisionModel>> GetDatabaseData();
    }
}
