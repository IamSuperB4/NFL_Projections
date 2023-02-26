using Microsoft.EntityFrameworkCore;
using NFL.Database.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Database
{
    partial class GamesRepository : BaseRepository<NflContext>, IGamesRepository
    {
        public GamesRepository(NflContext context, ILogger logger) : base(context, logger)
        {
        }

        async public Task<SeasonModel> GetSeason(int year)
        {
            var db = CreateDbContext();

            var query = db.Seasons
                .Where(s => s.Year == year);

            return await query.FirstOrDefaultWithNoLockAsync();
        }

        async public Task<List<ConferenceModel>> GetConferences()
        {
            var db = CreateDbContext();

            var query = db.Conferences
                .Include(c => c.Divisions)
                .ThenInclude(d => d.Teams);

            return await query.ToListWithNoLockAsync();
        }

        async public Task<ConferenceModel> GetConference(string conference)
        {
            var db = CreateDbContext();

            var query = db.Conferences
                .Where(c => c.Name == conference)
                .Include(c => c.Divisions)
                .ThenInclude(d => d.Teams);

            return await query.FirstOrDefaultWithNoLockAsync();
        }

        async public Task<List<DivisionModel>> GetDivisions(int year)
        {
            var db = CreateDbContext();

            var query = db.Divisions
                .Where(d => d.Season.Year == year)
                .Include(d => d.Teams);

            return await query.ToListWithNoLockAsync();
        }

        async public Task<DivisionModel> GetDivision(int year, string division)
        {
            var db = CreateDbContext();

            var query = db.Divisions
                .Where(d => d.Season.Year == year
                    && d.Name == division)
                .Include(d => d.Teams);

            return await query.FirstOrDefaultWithNoLockAsync();
        }

        async public Task<List<TeamModel>> GetTeams(int year)
        {
            var db = CreateDbContext();

            var query = db.Teams
                .Where(t => t.Division.Season.Year == year);

            return await query.ToListWithNoLockAsync();
        }

        async public Task<List<TeamModel>> GetTeamsFromConference(int year, string conference)
        {
            var db = CreateDbContext();

            var query = db.Teams
                .Where(t => t.Division.Season.Year == year
                    && t.Division.Conference.Name == conference);

            return await query.ToListWithNoLockAsync();
        }

        async public Task<TeamModel> GetTeamFromFullName(int year, string fullTeamName)
        {
            var db = CreateDbContext();

            var query = db.Teams
                .Where(t => t.Division.Season.Year == year
                    && t.FullName == fullTeamName);

            return await query.FirstOrDefaultWithNoLockAsync();
        }

        async public Task<TeamModel> GetTeamFromTeamName(int year, string teamName)
        {
            var db = CreateDbContext();

            var query = db.Teams
                .Where(t => t.Division.Season.Year == year
                    && t.Name == teamName);

            return await query.FirstOrDefaultWithNoLockAsync();
        }

        async public Task<UserModel> GetUser(string username = "actual")
        {
            var db = CreateDbContext();

            var query = db.Users
                .Where(u => u.Name == username && u.Type == "actual");

            return await query.FirstOrDefaultWithNoLockAsync();
        }

        async public Task FillDatabaseTables()
        {
            // get reference to context
            var db = CreateDbContext();


            /**********************************************************
             * 
             * Seasons
             * 
             *********************************************************/

            SeasonModel seasonModel = new()
            {
                Name = "2022-2023",
                Year = 2022,
                RegularSeasonWeekCount = 18,
                PlayoffTeams = 7
            };

            db.Seasons.Add(seasonModel);

            // save changes to database
            await db.SaveChangesAsync();


            /**********************************************************
             * 
             * Conferences
             * 
             *********************************************************/

            ConferenceModel conferenceModel = new()
            {
                Name = "AFC"
            };

            db.Conferences.Add(conferenceModel);

            conferenceModel = new()
            {
                Name = "NFC"
            };

            db.Conferences.Add(conferenceModel);

            // save changes to database
            await db.SaveChangesAsync();


            /**********************************************************
             * 
             * Divisions
             * 
             *********************************************************/

            /*
             * AFC 
             */
            DivisionModel divisionModel = new()
            {
                Name = "AFC East",
                ConferenceId = 1,
                SeasonId = 1
            };

            db.Divisions.Add(divisionModel);

            divisionModel = new()
            {
                Name = "AFC West",
                ConferenceId = 1,
                SeasonId = 1
            };

            db.Divisions.Add(divisionModel);

            divisionModel = new()
            {
                Name = "AFC North",
                ConferenceId = 1,
                SeasonId = 1
            };

            db.Divisions.Add(divisionModel);

            divisionModel = new()
            {
                Name = "AFC South",
                ConferenceId = 1,
                SeasonId = 1
            };

            db.Divisions.Add(divisionModel);

            /*
             * NFC 
             */
            divisionModel = new()
            {
                Name = "NFC East",
                ConferenceId = 2,
                SeasonId = 1
            };

            db.Divisions.Add(divisionModel);

            divisionModel = new()
            {
                Name = "NFC West",
                ConferenceId = 2,
                SeasonId = 1
            };

            db.Divisions.Add(divisionModel);

            divisionModel = new()
            {
                Name = "NFC North",
                ConferenceId = 2,
                SeasonId = 1
            };

            db.Divisions.Add(divisionModel);

            divisionModel = new()
            {
                Name = "NFC South",
                ConferenceId = 2,
                SeasonId = 1
            };

            db.Divisions.Add(divisionModel);

            // save changes to database
            await db.SaveChangesAsync();


            /**********************************************************
             * 
             * Teams
             * 
             *********************************************************/

            /*
             * AFC East
             */
            TeamModel teamModel = new()
            {
                Location = "Buffalo",
                Name = "Bills",
                FullName = "Buffalo Bills",
                DivisionId = 1
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Miami",
                Name = "Dolphins",
                FullName = "Miami Dolphins",
                DivisionId = 1
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "New England",
                Name = "Patriots",
                FullName = "New England Patriots",
                DivisionId = 1
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "New York",
                Name = "Jets",
                FullName = "New York Jets",
                DivisionId = 1
            };

            db.Teams.Add(teamModel);

            /*
             * AFC West
             */
            teamModel = new()
            {
                Location = "Kansas City",
                Name = "Chiefs",
                FullName = "Kansas City Chiefs",
                DivisionId = 2
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Los Angeles",
                Name = "Chargers",
                FullName = "Los Angeles Chargers",
                DivisionId = 2
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Las Vegas",
                Name = "Raiders",
                FullName = "Las Vegas Raiders",
                DivisionId = 2
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Denver",
                Name = "Broncos",
                FullName = "Denver Broncos",
                DivisionId = 2
            };

            db.Teams.Add(teamModel);

            /*
             * AFC North
             */
            teamModel = new()
            {
                Location = "Cincinnati",
                Name = "Bengals",
                FullName = "Cincinnati Bengals",
                DivisionId = 3
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Baltimore",
                Name = "Ravens",
                FullName = "Baltimore Ravens",
                DivisionId = 3
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Pittsburgh",
                Name = "Steelers",
                FullName = "Pittsburgh Steelers",
                DivisionId = 3
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Cleveland",
                Name = "Browns",
                FullName = "Cleveland Browns",
                DivisionId = 3
            };

            db.Teams.Add(teamModel);

            /*
             * AFC South
             */
            teamModel = new()
            {
                Location = "Jacksonville",
                Name = "Jaguars",
                FullName = "Jacksonville Jaguars",
                DivisionId = 4
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Tennessee",
                Name = "Titans",
                FullName = "Tennessee Titans",
                DivisionId = 4
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Indianapolis",
                Name = "Colts",
                FullName = "Indianapolis Colts",
                DivisionId = 4
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Houston",
                Name = "Texans",
                FullName = "Houston Texans",
                DivisionId = 4
            };

            db.Teams.Add(teamModel);

            /*
             * NFC East
             */
            teamModel = new()
            {
                Location = "Philadelphia",
                Name = "Eagles",
                FullName = "Philadelphia Eagles",
                DivisionId = 5
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Dallas",
                Name = "Cowboys",
                FullName = "Dallas Cowboys",
                DivisionId = 5
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "New York",
                Name = "Giants",
                FullName = "New York Giants",
                DivisionId = 5
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Washington",
                Name = "Commanders",
                FullName = "Washington Commanders",
                DivisionId = 5
            };

            db.Teams.Add(teamModel);

            /*
             * NFC West
             */
            teamModel = new()
            {
                Location = "San Francisco",
                Name = "49ers",
                FullName = "San Francisco 49ers",
                DivisionId = 6
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Seattle",
                Name = "Seahawks",
                FullName = "Seattle Seahawks",
                DivisionId = 6
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Los Angeles",
                Name = "Rams",
                FullName = "Los Angeles Rams",
                DivisionId = 6
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Buffalo",
                Name = "Bills",
                FullName = "Arizona Cardinals",
                DivisionId = 6
            };

            db.Teams.Add(teamModel);

            /*
             * NFC North
             */
            teamModel = new()
            {
                Location = "Minnesota",
                Name = "Vikings",
                FullName = "Minnesota Vikings",
                DivisionId = 7
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Detroit",
                Name = "Lions",
                FullName = "Detroit Lions",
                DivisionId = 7
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Green Bay",
                Name = "Packers",
                FullName = "Green Bay Packers",
                DivisionId = 7
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Buffalo",
                Name = "Bills",
                FullName = "Chicago Bears",
                DivisionId = 7
            };

            db.Teams.Add(teamModel);

            /*
             * NFC South
             */
            teamModel = new()
            {
                Location = "Tampa Bay",
                Name = "Buccaneers",
                FullName = "Tampa Bay Buccaneers",
                DivisionId = 8
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Carolina",
                Name = "Panthers",
                FullName = "Carolina Panthers",
                DivisionId = 8
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "New Orleans",
                Name = "Saints",
                FullName = "New Orleans Saints",
                DivisionId = 8
            };

            db.Teams.Add(teamModel);

            teamModel = new()
            {
                Location = "Atlanta",
                Name = "Falcons",
                FullName = "Atlanta Falcons",
                DivisionId = 8
            };

            db.Teams.Add(teamModel);

            // save changes to database
            await db.SaveChangesAsync();
        }
    }
}
