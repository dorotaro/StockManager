using System.Data.SQLite;

namespace StockManagerWithAI.Persistence.ConnectionFactories;

public class SqLiteConnectionFactory : IConnectionFactory
{
	public SQLiteConnection Connection { get; }

	public SqLiteConnectionFactory(string connectionString)
	{
		Connection = new SQLiteConnection(connectionString);
		Connection.Open();
	}

	public void Dispose()
	{
		Connection?.Dispose();
	}
}