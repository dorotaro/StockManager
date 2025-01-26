using System.Data.SQLite;

namespace StockManagerWithAI.Persistence.ConnectionFactories;

public interface IConnectionFactory : IDisposable
{
	SQLiteConnection Connection { get; }
}