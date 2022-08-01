namespace QueryProject.Application.Services.Interfaces
{
    public interface ICacheService
    {
        Task Set<T>(string key, T obj, DateTime? expireDate);
        Task<T> Get<T>(string key);
        Task Clear();
    }
}
