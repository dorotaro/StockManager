using System.Data.SQLite;

namespace StockManager.Persistence.ConnectionFactories;

public class SQLiteConnectionFactory : IConnectionFactory
{
	public SQLiteConnection Connection { get; }

	public SQLiteConnectionFactory(string connectionString)
	{
		Connection = new SQLiteConnection(connectionString);
		Connection.Open(); //TODO: make sure it's ok to keep the connection alive whole time as app is running
	}

	public void Dispose()
	{
		Connection?.Dispose();
	}
}