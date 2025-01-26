using System.Data.SQLite;

namespace StockManager.Persistence.ConnectionFactories;

public interface IConnectionFactory : IDisposable
{
	SQLiteConnection Connection { get; }
}