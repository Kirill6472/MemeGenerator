using MemeGenerator.DAL;

namespace MemeGenerator.BLL.Services.InitialMemesProvider
{
    public interface IInitialMemesProvider
    {
        ImageTemplateList GetDataFromJson();
    }
}
