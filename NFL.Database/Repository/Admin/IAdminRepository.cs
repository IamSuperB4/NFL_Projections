using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFL.Database.Models;
using NFL.Dto;

namespace NFL.Database
{
    /// <summary>
    /// Get data from the database for the website's Admin Page
    /// </summary>
    public interface IAdminRepository : IBaseRepository
    {
        /// <summary>
        /// Upsert (Update/Insert) a Game model to the database
        /// </summary>
        /// <param name="model">Model to upsert</param>
        /// <param name="insert">Whether to insert new model</param>
        /// <param name="update">Whether to insert current database model</param>
        /// <param name="skipSave">Whether to save changes</param>
        /// <param name="skipDetach">Whether to set model's state to EntityState.Detached</param>
        /// <returns>New model in database</returns>
        /// <exception cref="Exception">
        ///     - On insert: Game already in database
        ///     - On update: Game does not exist in database
        /// </exception>
        Task<GameModel> UpsertGameAsync(GameModel model, bool insert = true, bool update = true, bool skipSave = false, bool skipDetach = false);

        /// <summary>
        /// Upsert (Update/Insert) a List of Game models to the database
        /// </summary>
        /// <param name="list">List of models to upsert</param>
        /// <param name="insert">Whether to insert new model</param>
        /// <param name="update">Whether to insert current database model</param>
        /// <param name="skipSave">Whether to save changes</param>
        Task UpsertGamesRangeAsync(List<GameModel> list, bool insert = true, bool update = true, bool skipSave = false);

        /// <summary>
        /// Upsert (Update/Insert) a Team model to the database
        /// </summary>
        /// <param name="model">Model to upsert</param>
        /// <param name="insert">Whether to insert new model</param>
        /// <param name="update">Whether to insert current database model</param>
        /// <param name="skipSave">Whether to save changes</param>
        /// <param name="skipDetach">Whether to set model's state to EntityState.Detached</param>
        /// <returns>New model in database</returns>
        /// <exception cref="Exception">
        ///     - On insert: Team already in database
        ///     - On update: Team does not exist in database
        /// </exception>
        Task<TeamModel> UpsertTeamAsync(TeamModel model, bool insert = true, bool update = true, bool skipSave = false, bool skipDetach = false);

        /// <summary>
        /// Upsert (Update/Insert) a List of Team models to the database
        /// </summary>
        /// <param name="list">List of models to upsert</param>
        /// <param name="insert">Whether to insert new model</param>
        /// <param name="update">Whether to insert current database model</param>
        /// <param name="skipSave">Whether to save changes</param>
        Task UpsertTeamsRangeAsync(List<TeamModel> list, bool insert = true, bool update = true, bool skipSave = false);

        /// <summary>
        /// Upsert (Update/Insert) a Division model to the database
        /// </summary>
        /// <param name="model">Model to upsert</param>
        /// <param name="insert">Whether to insert new model</param>
        /// <param name="update">Whether to insert current database model</param>
        /// <param name="skipSave">Whether to save changes</param>
        /// <param name="skipDetach">Whether to set model's state to EntityState.Detached</param>
        /// <returns>New model in database</returns>
        /// <exception cref="Exception">
        ///     - On insert: Division already in database
        ///     - On update: Division does not exist in database
        /// </exception>
        Task<DivisionModel> UpsertDivisionAsync(DivisionModel model, bool insert = true, bool update = true, bool skipSave = false, bool skipDetach = false);

        /// <summary>
        /// Upsert (Update/Insert) a List of Division models to the database
        /// </summary>
        /// <param name="list">List of models to upsert</param>
        /// <param name="insert">Whether to insert new model</param>
        /// <param name="update">Whether to insert current database model</param>
        /// <param name="skipSave">Whether to save changes</param>
        Task UpsertDivisionsRangeAsync(List<DivisionModel> list, bool insert = true, bool update = true, bool skipSave = false);

        /// <summary>
        /// Upsert (Update/Insert) a Season model to the database
        /// </summary>
        /// <param name="model">Model to upsert</param>
        /// <param name="insert">Whether to insert new model</param>
        /// <param name="update">Whether to insert current database model</param>
        /// <param name="skipSave">Whether to save changes</param>
        /// <param name="skipDetach">Whether to set model's state to EntityState.Detached</param>
        /// <returns>New model in database</returns>
        /// <exception cref="Exception">
        ///     - On insert: Season already in database
        ///     - On update: Season does not exist in database
        /// </exception>
        Task<SeasonModel> UpsertSeasonAsync(SeasonModel model, bool insert = true, bool update = true, bool skipSave = false, bool skipDetach = false);

        /// <summary>
        /// Upsert (Update/Insert) a User model to the database
        /// </summary>
        /// <param name="model">Model to upsert</param>
        /// <param name="insert">Whether to insert new model</param>
        /// <param name="update">Whether to insert current database model</param>
        /// <param name="skipSave">Whether to save changes</param>
        /// <param name="skipDetach">Whether to set model's state to EntityState.Detached</param>
        /// <returns>New model in database</returns>
        /// <exception cref="Exception">
        ///     - On insert: User already in database
        ///     - On update: User does not exist in database
        /// </exception>
        Task<UserModel> UpsertUserAsync(UserModel model, bool insert = true, bool update = true, bool skipSave = false, bool skipDetach = false);

        /// <summary>
        /// Upsert (Update/Insert) a List of User models to the database
        /// </summary>
        /// <param name="list">List of models to upsert</param>
        /// <param name="insert">Whether to insert new model</param>
        /// <param name="update">Whether to insert current database model</param>
        /// <param name="skipSave">Whether to save changes</param>
        Task UpsertUsersRangeAsync(List<UserModel> list, bool insert = true, bool update = true, bool skipSave = false);
    }
}
