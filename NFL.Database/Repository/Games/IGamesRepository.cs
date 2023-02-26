using NFL.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Database
{
    public interface IGamesRepository : IBaseRepository
    {
        Task<SeasonModel> GetSeason(int year);

        Task<List<ConferenceModel>> GetConferences();

        Task<ConferenceModel> GetConference(string conference);

        Task<List<DivisionModel>> GetDivisions(int year);

        Task<DivisionModel> GetDivision(int year, string division);

        Task<List<TeamModel>> GetTeams(int year);

        Task<List<TeamModel>> GetTeamsFromConference(int year, string conference);

        Task<TeamModel> GetTeamFromFullName(int year, string fullTeamName);

        Task<TeamModel> GetTeamFromTeamName(int year, string teamName);

        Task<UserModel> GetUser(string username = "actual");

        //Task FillDatabaseTables();
    }
}
