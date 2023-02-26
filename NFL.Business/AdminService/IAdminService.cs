using NFL.Database.Models;
using NFL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Business
{
    /// <summary>
    /// Middle man service between database and website API for admin page
    /// </summary>
    public interface IAdminService
    {
        Task AddOrUpdateGamesAsync(List<GameCsvRecord> games, int year);

        Task AddOrUpdateTeamsAsync(List<TeamCsvRecord> games, int year);

        Task AddOrUpdateSeasonAsync(SeasonDto season);

        Task AddOrUpdateUsersAsync(List<UserDto> users);

        Task<List<DivisionModel>> GetDatabaseData();
    }
}
