using MemeGenerator.DAL.MigrationsChecker;

namespace MemeGenerator.Tests
{
    class StubMigrationsChecker : IMigrationsChecker
    {
        public bool ShouldAllMigrationsBeApplied;

        public bool DoAllMigrationsApply()
        {
            return ShouldAllMigrationsBeApplied;
        }
    }
}
