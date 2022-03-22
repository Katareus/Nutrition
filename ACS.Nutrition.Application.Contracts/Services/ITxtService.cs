namespace ACS.Nutrition.Application.Contracts.Services
{
    public interface ITxtService
    {
        void UpsertFile(string fileName, string content);
    }
}
