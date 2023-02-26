using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFL.Database.Models;
using NFL.Dto;
using NFL.Database;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace NFL.Business
{
    /// <summary>
    /// Middle man service between database and website API for admin page
    /// </summary>
    class AdminService : IAdminService
    {
        private readonly ILogger logger;
        private readonly IAdminRepository repo;
        private readonly IGamesRepository gamesRepo;

        public AdminService(IAdminRepository _repo, IGamesRepository _gamesRepo, ILogger _logger)
        {
            repo = _repo;
            gamesRepo = _gamesRepo;
            logger = _logger;
        }

        /// <summary>
        /// Convert List of GameCsvRecords to database models and upload changes to database
        /// </summary>
        /// <param name="games">List of GameCsvRecords</param>
        /// <param name="year">Season year</param>
        async public Task AddOrUpdateGamesAsync(List<GameCsvRecord> games, int year)
        {
            List<TeamModel> teams = await gamesRepo.GetTeams(year);
            SeasonModel season = await gamesRepo.GetSeason(year);
            UserModel user = await gamesRepo.GetUser();
            IDictionary<string, TeamModel> teamDictionary;

            // if CSV used full team names
            if (teams.Select(t => t.FullName).Contains(games.FirstOrDefault().Winner))
                teamDictionary = teams.ToDictionary(t => t.FullName);
            // if CSV used only team name
            else
                teamDictionary = teams.ToDictionary(t => t.Name);

            List<GameModel> gameModels = games.AsGameModel(teamDictionary, season, user);

            await repo.UpsertGamesRangeAsync(gameModels);
        }

        /// <summary>
        /// Convert List of TeamCsvRecord to database models and upload changes to database
        /// </summary>
        /// <param name="teams">List of TeamCsvRecords</param>
        /// <param name="year">Season year</param>
        async public Task AddOrUpdateTeamsAsync(List<TeamCsvRecord> teams, int year)
        {
            SeasonModel season = await gamesRepo.GetSeason(year);
            List<ConferenceModel> conferences = await gamesRepo.GetConferences();

            List<DivisionModel> divisionModels = teams.AsDivisionModels(season, conferences);

            await repo.UpsertDivisionsRangeAsync(divisionModels);

            divisionModels = await gamesRepo.GetDivisions(year);

            List<TeamModel> teamModels = teams.AsTeamModel(divisionModels);

            await repo.UpsertTeamsRangeAsync(teamModels);
        }

        /// <summary>
        /// Convert SeasonDto to database model and upload changes to database
        /// </summary>
        /// <param name="season">SeasonDto</param>
        async public Task AddOrUpdateSeasonAsync(SeasonDto season)
        {
            SeasonModel seasonModel = season.AsSeasonModel();

            await repo.UpsertSeasonAsync(seasonModel);
        }

        /// <summary>
        /// Convert List of UserDtos to database models and upload changes to database
        /// </summary>
        /// <param name="users">List of UserDtos</param>
        async public Task AddOrUpdateUsersAsync(List<UserDto> users)
        {
            List<UserModel> userModels = users.AsUserModel();

            await repo.UpsertUsersRangeAsync(userModels);
        }

        async public Task<List<DivisionModel>> GetDatabaseData()
        {
            return await gamesRepo.GetDivisions(2022);
        }
    }
}
