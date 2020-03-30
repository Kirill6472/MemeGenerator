using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeGenerator.DAL.MigrationChecker
{
    public class MigrationsChecker : IMigrationsChecker
    {
        private readonly MemeGeneratorDbContext _context;

        public MigrationsChecker(MemeGeneratorDbContext context)
        {
            _context = context;
        }

        public bool AreAllMigrationsApplied()
        {
            var applied = _context.GetService<IHistoryRepository>().GetAppliedMigrations().Select(m => m.MigrationId);

            var allMigrationsKeys = _context.GetService<IMigrationsAssembly>().Migrations.Select(m => m.Key);

            return !allMigrationsKeys.Except(applied).Any();
        }
    }
}
