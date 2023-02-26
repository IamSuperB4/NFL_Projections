using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFL.Database.Models;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace NFL.Database
{
    class NflContext : BaseDbContext
    {
        public NflContext(DbContextOptions<NflContext> options) : base(options)
        {
        }

        public DbSet<SeasonModel> Seasons { get; set; }

        public DbSet<ConferenceModel> Conferences { get; set; }

        public DbSet<DivisionModel> Divisions { get; set; }

        public DbSet<TeamModel> Teams { get; set; }

        public DbSet<GameModel> Games { get; set; }

        public DbSet<UserModel> Users { get; set; }


        /// <summary>
        /// Default function called by Entity Framework to configure the DbContext
        /// </summary>
        /// <param name="optionsBuilder">Pre-configured by EntityFramework</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameModel>()
                        .HasOne(g => g.HomeTeam)
                        .WithMany(t => t.HomeGames)
                        .HasForeignKey(g => g.HomeTeamId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<GameModel>()
                        .HasOne(g => g.AwayTeam)
                        .WithMany(t => t.AwayGames)
                        .HasForeignKey(g => g.AwayTeamId)
                        .OnDelete(DeleteBehavior.NoAction);

            var decimalProps = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => (System.Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

            foreach (var property in decimalProps)
            {
                property.SetPrecision(2);
                property.SetScale(1);
            }
        }

        /// <summary>
        /// Upsert (Update or Insert) a model to the database
        /// </summary>
        /// <typeparam name="TModel">Database model being upserted</typeparam>
        /// <param name="dbset">DbSet of the model</param>
        /// <param name="model">Model object being upserted</param>
        /// <param name="existingModel">
        ///     If updating: the model in the database that will be updated
        ///     If inserting: null
        /// </param>
        /// <param name="insertValidator">(Optional) Used to see if model exists in the database on an insert? Never used so I'm not quite sure what this does</param>
        /// <param name="updateValidator">(Optional) Used to see if update is valid based on existing and new model? Never used so I'm not quite sure what this does</param>
        /// <param name="updater">Actions to take when updating model. Required for updates</param>
        /// <returns>Return new model object in database</returns>
        /// <exception cref="NotImplementedException">If updater parameter Invoke() function is not implemented</exception>
        async public Task<TModel> UpsertModelAsync<TModel>(
            DbSet<TModel> dbset, TModel model, TModel existingModel,
            Func<TModel, bool>? insertValidator = null, Func<TModel, TModel, bool>? updateValidator = null, Action<TModel, TModel>? updater = null) where TModel : class
        {
            // invoke the intercepters, if they exist
            bool proceed = true;
            if (insertValidator != null) proceed = insertValidator.Invoke(existingModel);
            if (proceed == true && updateValidator != null) proceed = updateValidator.Invoke(model, existingModel);

            // perform the desired action
            if (proceed == true && existingModel == null)
            {
                // insert the new model
                var tracker = dbset.Add(model);
                existingModel = tracker.Entity;
            }
            else
            {
                // update the existing model
                if (updater != null)
                {
                    updater.Invoke(model, existingModel);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            // save changes
            await SaveChangesAsync();

            // return
            return existingModel;
        }

        /// <summary>
        /// Upsert (Update or Insert) a model to the database, with a where clause
        /// </summary>
        /// <typeparam name="TModel">Database model being upserted</typeparam>
        /// <param name="dbset">DbSet of the model</param>
        /// <param name="model">Model object being upserted</param>
        /// <param name="whereClause">Where clause to filter DbSet of model? Never used so I'm not quite sure what this does</param>
        /// <param name="insertValidator">(Optional) Used to see if model exists in the database on an insert? Never used so I'm not quite sure what this does</param>
        /// <param name="updateValidator">(Optional) Used to see if update is valid based on existing and new model? Never used so I'm not quite sure what this does</param>
        /// <param name="updater">Actions to take when updating model. Required for updates</param>
        /// <returns>Return new model object in database</returns>
        async public Task<TModel>? UpsertModelAsync<TModel>(
            DbSet<TModel> dbset, TModel model, Func<TModel, bool>? whereClause = null,
            Func<TModel, bool>? insertValidator = null, Func<TModel, TModel, bool>? updateValidator = null, Action<TModel, TModel>? updater = null) where TModel : class
        {
            // look for the flat file with 
            TModel? existingModel = null;

            if (whereClause != null)
            {
                existingModel = dbset
                    .Where(whereClause).FirstOrDefault();
            }

            // return
            return await UpsertModelAsync(dbset, model, existingModel, insertValidator, updateValidator, updater);
        }
    }
}
