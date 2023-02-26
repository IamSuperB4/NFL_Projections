using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NFL.Business;
using NFL.Database;
using NFL.Database.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Sandbox
{
    public class Sandbox
    {
        private static IConfigurationRoot configuration;
        private static ILogger logger;
        private static IGamesService service;
        private static IAdminService adminService;
        private readonly IGamesRepository gamesRepo;

        /// <summary>
        /// Initialize classes to run GamesService and logget
        /// </summary>
        /// <param name="_configuration"></param>
        /// <param name="_logger"></param>
        /// <param name="_service"></param>
        public Sandbox(IConfigurationRoot _configuration, ILogger _logger, IAdminService _adminService, IGamesService _service, IGamesRepository _gamesRepo)
        {
            configuration = _configuration;
            logger = _logger;
            service = _service;
            adminService = _adminService;
            gamesRepo = _gamesRepo;
        }

        async public Task InitializeDatabase()
        {
            //await gamesRepo.FillDatabaseTables();
        }

        async public Task GetDatabaseData()
        {
            List<DivisionModel> divisions = await adminService.GetDatabaseData();

            return;
        }
    }
}
