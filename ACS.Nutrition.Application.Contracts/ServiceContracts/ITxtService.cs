namespace Nutrition.Application.Interfaces.ServiceContracts
{
    public interface ITxtService
    {
        void UpsertFile(string fileName, string content);
    }
}
