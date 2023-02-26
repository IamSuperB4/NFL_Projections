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
    /// <summary>
    /// Get data from the database for the website's Admin Page
    /// </summary>
    class AdminRepository : BaseRepository<NflContext>, IAdminRepository
    {
        public AdminRepository(NflContext context, ILogger logger) : base(context, logger)
        {
        }


        #region Upsert Methods

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
        async public Task<GameModel> UpsertGameAsync(GameModel model, bool insert = true, bool update = true, bool skipSave = false, bool skipDetach = false)
        {
            var db = CreateDbContext();

            GameModel? existingModel = await db.Games
                .Where(g => g.SeasonId == model.SeasonId
                    && g.Week == model.Week
                    && g.UserId == model.UserId
                    && g.AwayTeamId == model.AwayTeamId)
                .FirstOrDefaultAsync();

            // determine the upsert action
            UpsertAction action = (existingModel == null) ? UpsertAction.Insert : UpsertAction.Update;

            // if insert operation is not allowed, existing model should be null
            if (insert == false && action == UpsertAction.Insert)
            {
                throw new Exception($"Game is already in the database");
            }

            // if update is allowed, existing model should not be found
            // same time, if update is allowed, Id in both models should match
            if (action == UpsertAction.Update && (update == false || (model.Id != 0 && model.Id != existingModel.Id)))
            {
                throw new Exception("Game does not exist");
            }

            // perform the desired action
            if (action == UpsertAction.Insert)
            {
                // insert the new model
                var tracker = db.Games.Add(model);
                model = tracker.Entity;
            }
            else
            {
                model.Id = existingModel.Id;
                existingModel.UpdateModel(model);
                model = existingModel;
            }

            // save to database
            if (skipSave == false)
            {
                await db.SaveChangesAsync();
            }

            // detach the entity
            if (skipDetach == false)
            {
                db.Entry(model).State = EntityState.Detached;
            }

            // return
            return model;
        }

        /// <summary>
        /// Upsert (Update/Insert) a List of Game models to the database
        /// </summary>
        /// <param name="list">List of models to upsert</param>
        /// <param name="insert">Whether to insert new model</param>
        /// <param name="update">Whether to insert current database model</param>
        /// <param name="skipSave">Whether to save changes</param>
        async public Task UpsertGamesRangeAsync(List<GameModel> list, bool insert = true, bool update = true, bool skipSave = false)
        {
            // add to the collection
            foreach (GameModel model in list)
            {
                await UpsertGameAsync(model, insert, update, true, true);
            }

            // save to database now
            var db = CreateDbContext();
            await db.SaveChangesAsync();
        }

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
        async public Task<TeamModel> UpsertTeamAsync(TeamModel model, bool insert = true, bool update = true, bool skipSave = false, bool skipDetach = false)
        {
            var db = CreateDbContext();

            TeamModel? existingModel = await db.Teams
                .Where(t => t.FullName == model.FullName
                    && t.DivisionId == model.DivisionId)
                .FirstOrDefaultAsync();

            // determine the upsert action
            UpsertAction action = (existingModel == null) ? UpsertAction.Insert : UpsertAction.Update;

            // if insert operation is not allowed, existing model should be null
            if (insert == false && action == UpsertAction.Insert)
            {
                throw new Exception($"Team is already in the database");
            }

            // if update is allowed, existing model should not be found
            // same time, if update is allowed, Id in both models should match
            if (action == UpsertAction.Update && (update == false || (model.Id != 0 && model.Id != existingModel.Id)))
            {
                throw new Exception("Team does not exist");
            }

            // perform the desired action
            if (action == UpsertAction.Insert)
            {
                // insert the new model
                var tracker = db.Teams.Add(model);
                model = tracker.Entity;
            }
            else
            {
                model.Id = existingModel.Id;
                existingModel.UpdateModel(model);
                model = existingModel;
            }

            // save to database
            if (skipSave == false)
            {
                await db.SaveChangesAsync();
            }

            // detach the entity
            if (skipDetach == false)
            {
                db.Entry(model).State = EntityState.Detached;
            }

            // return
            return model;
        }

        /// <summary>
        /// Upsert (Update/Insert) a List of Team models to the database
        /// </summary>
        /// <param name="list">List of models to upsert</param>
        /// <param name="insert">Whether to insert new model</param>
        /// <param name="update">Whether to insert current database model</param>
        /// <param name="skipSave">Whether to save changes</param>
        async public Task UpsertTeamsRangeAsync(List<TeamModel> list, bool insert = true, bool update = true, bool skipSave = false)
        {
            // add to the collection
            foreach (TeamModel model in list)
            {
                await UpsertTeamAsync(model, insert, update, true, true);
            }

            // save to database now
            var db = CreateDbContext();
            await db.SaveChangesAsync();
        }

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
        async public Task<DivisionModel> UpsertDivisionAsync(DivisionModel model, bool insert = true, bool update = true, bool skipSave = false, bool skipDetach = false)
        {
            var db = CreateDbContext();

            DivisionModel? existingModel = await db.Divisions
                .Where(d => d.SeasonId == model.SeasonId
                    && d.ConferenceId == model.ConferenceId
                    && d.Name == model.Name)
                .FirstOrDefaultAsync();

            // determine the upsert action
            UpsertAction action = (existingModel == null) ? UpsertAction.Insert : UpsertAction.Update;

            // if insert operation is not allowed, existing model should be null
            if (insert == false && action == UpsertAction.Insert)
            {
                throw new Exception($"Division is already in the database");
            }

            // if update is allowed, existing model should not be found
            // same time, if update is allowed, Id in both models should match
            if (action == UpsertAction.Update && (update == false || (model.Id != 0 && model.Id != existingModel.Id)))
            {
                throw new Exception("Division does not exist");
            }

            // perform the desired action
            if (action == UpsertAction.Insert)
            {
                // insert the new model
                var tracker = db.Divisions.Add(model);
                model = tracker.Entity;
            }
            else
            {
                model.Id = existingModel.Id;
                existingModel.UpdateModel(model);
                model = existingModel;
            }

            // save to database
            if (skipSave == false)
            {
                await db.SaveChangesAsync();
            }

            // detach the entity
            if (skipDetach == false)
            {
                db.Entry(model).State = EntityState.Detached;
            }

            // return
            return model;
        }

        /// <summary>
        /// Upsert (Update/Insert) a List of Division models to the database
        /// </summary>
        /// <param name="list">List of models to upsert</param>
        /// <param name="insert">Whether to insert new model</param>
        /// <param name="update">Whether to insert current database model</param>
        /// <param name="skipSave">Whether to save changes</param>
        async public Task UpsertDivisionsRangeAsync(List<DivisionModel> list, bool insert = true, bool update = true, bool skipSave = false)
        {
            // add to the collection
            foreach (DivisionModel model in list)
            {
                await UpsertDivisionAsync(model, insert, update, true, true);
            }

            // save to database now
            var db = CreateDbContext();
            await db.SaveChangesAsync();
        }

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
        async public Task<SeasonModel> UpsertSeasonAsync(SeasonModel model, bool insert = true, bool update = true, bool skipSave = false, bool skipDetach = false)
        {
            var db = CreateDbContext();

            SeasonModel? existingModel = await db.Seasons
                .Where(g => g.Year == model.Year
                    && g.Name == model.Name)
                .FirstOrDefaultAsync();

            // determine the upsert action
            UpsertAction action = (existingModel == null) ? UpsertAction.Insert : UpsertAction.Update;

            // if insert operation is not allowed, existing model should be null
            if (insert == false && action == UpsertAction.Insert)
            {
                throw new Exception($"Season is already in the database");
            }

            // if update is allowed, existing model should not be found
            // same time, if update is allowed, Id in both models should match
            if (action == UpsertAction.Update && (update == false || (model.Id != 0 && model.Id != existingModel.Id)))
            {
                throw new Exception("Season does not exist");
            }

            // perform the desired action
            if (action == UpsertAction.Insert)
            {
                // insert the new model
                var tracker = db.Seasons.Add(model);
                model = tracker.Entity;
            }
            else
            {
                model.Id = existingModel.Id;
                existingModel.UpdateModel(model);
                model = existingModel;
            }

            // save to database
            if (skipSave == false)
            {
                await db.SaveChangesAsync();
            }

            // detach the entity
            if (skipDetach == false)
            {
                db.Entry(model).State = EntityState.Detached;
            }

            // return
            return model;
        }

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
        async public Task<UserModel> UpsertUserAsync(UserModel model, bool insert = true, bool update = true, bool skipSave = false, bool skipDetach = false)
        {
            var db = CreateDbContext();

            UserModel? existingModel = await db.Users
                .Where(g => g.Name == model.Name
                    && g.Type == model.Type)
                .FirstOrDefaultAsync();

            // determine the upsert action
            UpsertAction action = (existingModel == null) ? UpsertAction.Insert : UpsertAction.Update;

            // if insert operation is not allowed, existing model should be null
            if (insert == false && action == UpsertAction.Insert)
            {
                throw new Exception($"User is already in the database");
            }

            // if update is allowed, existing model should not be found
            // same time, if update is allowed, Id in both models should match
            if (action == UpsertAction.Update && (update == false || (model.Id != 0 && model.Id != existingModel.Id)))
            {
                throw new Exception("User does not exist");
            }

            // perform the desired action
            if (action == UpsertAction.Insert)
            {
                // insert the new model
                var tracker = db.Users.Add(model);
                model = tracker.Entity;
            }
            else
            {
                model.Id = existingModel.Id;
                existingModel.UpdateModel(model);
                model = existingModel;
            }

            // save to database
            if (skipSave == false)
            {
                await db.SaveChangesAsync();
            }

            // detach the entity
            if (skipDetach == false)
            {
                db.Entry(model).State = EntityState.Detached;
            }

            // return
            return model;
        }

        /// <summary>
        /// Upsert (Update/Insert) a List of User models to the database
        /// </summary>
        /// <param name="list">List of models to upsert</param>
        /// <param name="insert">Whether to insert new model</param>
        /// <param name="update">Whether to insert current database model</param>
        /// <param name="skipSave">Whether to save changes</param>
        async public Task UpsertUsersRangeAsync(List<UserModel> list, bool insert = true, bool update = true, bool skipSave = false)
        {
            // add to the collection
            foreach (UserModel model in list)
            {
                await UpsertUserAsync(model, insert, update, true, true);
            }

            // save to database now
            var db = CreateDbContext();
            await db.SaveChangesAsync();
        }


        #endregion
    }
}
