namespace MemeGenerator.DomainServices.Interfaces
{
    public interface IMigrationsChecker
    {
        bool AreAllMigrationsApplied();
    }
}
