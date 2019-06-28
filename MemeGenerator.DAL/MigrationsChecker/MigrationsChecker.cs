using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeGenerator.DAL.MigrationsChecker
{
    public class MigrationsChecker : IMigrationsChecker
    {
        private readonly MemeGeneratorDbContext _context;

        public MigrationsChecker(MemeGeneratorDbContext context)
        {
            _context = context;
        }

        public bool DoAllMigrationsApply()
        {
            var applied = _context.GetService<IHistoryRepository>().GetAppliedMigrations().Select(m => m.MigrationId);

            var total = _context.GetService<IMigrationsAssembly>().Migrations.Select(m => m.Key);

            return !total.Except(applied).Any();
        }
    }
}
