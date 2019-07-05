namespace MemeGenerator.DAL.Providers
{
    public interface IInitialMemesProvider
    {
        ImageTemplateList GetDataFromJson();
    }
}
