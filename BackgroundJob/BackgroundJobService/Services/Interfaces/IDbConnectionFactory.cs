using System.Data;

namespace BackgroundJobService.Services.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}
