using NFL.Database.Models;
using NFL.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Business
{
    static class ChassisTranslator
    {
        /// <summary>
        /// Converts list of database GameCsvRecords to a list of GameModel database objects
        /// </summary>
        /// <param name="list">List of GameCsvRecord objects</param>
        /// <param name="teamDictionary">Dictionary with season's teams</param>
        /// <param name="season">SeasonModel</param>
        /// <param name="user">UserModel</param>
        /// <returns>List of GameModel objects</returns>
        public static List<GameModel> AsGameModel(this List<GameCsvRecord> list, IDictionary<string, TeamModel> teamDictionary, SeasonModel season, UserModel user)
        {
            return list.ConvertAll(game => game.AsGameModel(teamDictionary, season, user));
        }

        /// <summary>
        /// Converts a GameCsvRecord to GameModel database object
        /// </summary>
        /// <param name="game">GameCsvRecord object</param>
        /// <param name="teamDictionary">Dictionary with season's teams</param>
        /// <param name="season">SeasonModel</param>
        /// <param name="user">UserModel</param>
        /// <returns>GameModel database object</returns>
        public static GameModel AsGameModel(this GameCsvRecord game, IDictionary<string, TeamModel> teamDictionary, SeasonModel season, UserModel user)
        {
            string awayTeam = game.Location.Contains('@') ? game.Winner : game.Loser;
            string homeTeam = game.Location.Contains('@') ? game.Loser : game.Winner;
            int awayScore = (int)(game.Location.Contains('@') ? game.PointsWinner : game.PointsLoser);
            int homeScore = (int)(game.Location.Contains('@') ? game.PointsLoser : game.PointsWinner);

            return new GameModel()
            {
                Week = game.Week,
                StartTime = DateTime.ParseExact(game.Date + " " + game.Time, "M/d/yyyy h:mmtt", CultureInfo.InvariantCulture),
                IsPlayoffs = game.Week > season.RegularSeasonWeekCount,
                AwayTeamId = teamDictionary[awayTeam].Id,
                AwayTeamScore = awayScore,
                HomeTeamId = teamDictionary[homeTeam].Id,
                HomeTeamScore = homeScore,
                SeasonId = season.Id,
                UserId = user.Id
            };
        }

        /// <summary>
        /// Converts list of database TeamCsvRecords to a list of GameModel database objects
        /// </summary>
        /// <param name="teams">List of TeamCsvRecord objects</param>
        /// <param name="season">SeasonModel</param>
        /// <param name="conferences">List of ConferenceModels</param>
        /// <returns>List of GameModel objects</returns>
        public static List<DivisionModel> AsDivisionModels(this List<TeamCsvRecord> teams, SeasonModel season, List<ConferenceModel> conferences)
        {
            List<DivisionModel> divisions = new();

            foreach (TeamCsvRecord team in teams)
            {
                string conferenceName = team.Division.Trim().Split(' ')[0].ToLower();
                ConferenceModel conference = conferences.FirstOrDefault(c => c.Name.ToLower() == team.Division);

                DivisionModel? division = divisions.FirstOrDefault(d => d.Name == team.Division);

                if (division == null)
                {
                    division = new()
                    {
                        Name = team.Division,
                        ConferenceId = conference.Id,
                        SeasonId = season.Id
                    };

                    divisions.Add(division);
                }
            }

            return divisions;
        }

        /// <summary>
        /// Converts list of database TeamCsvRecords to a list of GameModel database objects
        /// </summary>
        /// <param name="list">List of TeamCsvRecord objects</param>
        /// <param name="divisions">List of DivisionModels</param>
        /// <returns>List of TeamModels objects</returns>
        public static List<TeamModel> AsTeamModel(this List<TeamCsvRecord> list, List<DivisionModel> divisions)
        {
            return list.ConvertAll(team => team.AsTeamModel(divisions));
        }

        /// <summary>
        /// Converts a TeamCsvRecord to TeamModel database object
        /// </summary>
        /// <param name="team">TeamCsvRecord object</param>
        /// <param name="divisions">List of DivisionModels</param>
        /// <returns>TeamModel database object</returns>
        public static TeamModel AsTeamModel(this TeamCsvRecord team, List<DivisionModel> divisions)
        {
            return new TeamModel()
            {
                Location = team.Location,
                Name = team.Name,
                FullName = $"{ team.Location } { team.Name }",
                DivisionId = divisions.FirstOrDefault(d => d.Name == team.Division).Id,
            };
        }

        /// <summary>
        /// Converts a SeasonDto to SeasonModel database object
        /// </summary>
        /// <param name="season">SeasonDto object</param>
        /// <returns>SeasonModel database object</returns>
        public static SeasonModel AsSeasonModel(this SeasonDto season)
        {
            return new SeasonModel()
            {
                Name = season.Name,
                Year = season.Year,
                RegularSeasonWeekCount = season.RegularSeasonWeekCount,
                PlayoffTeams = season.PlayoffTeams
            };
        }

        /// <summary>
        /// Converts list of database TeamCsvRecords to a list of GameModel database objects
        /// </summary>
        /// <param name="list">List of UserDtos</param>
        /// <returns>List of UserModels objects</returns>
        public static List<UserModel> AsUserModel(this List<UserDto> list)
        {
            return list.ConvertAll(user => user.AsUserModel());
        }

        /// <summary>
        /// Converts a UserDto to UserModel database object
        /// </summary>
        /// <param name="user">UserDto object</param>
        /// <returns>TeamModel database object</returns>
        public static UserModel AsUserModel(this UserDto user)
        {
            return new UserModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Name = user.Name,
                Type = user.Type,
                Money = user.Money,
            };
        }
    }
}
