using NFL.Database;
using NFL.Database.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Business
{
    /// <summary>
    /// Middle man service between database and website API for database information
    /// </summary>
    public class GamesService : IGamesService
    {
        private readonly ILogger logger;
        private readonly IGamesRepository repo;

        public GamesService(IGamesRepository _repo, ILogger _logger)
        {
            repo = _repo;
            logger = _logger;
        }

        async public Task PopulateDatabase()
        {
            //await repo.FillDatabaseTables();
        }

        async public Task<List<DivisionModel>> GetDatabaseData()
        {
            return await repo.GetDivisions(2022);
        }
    }
}
