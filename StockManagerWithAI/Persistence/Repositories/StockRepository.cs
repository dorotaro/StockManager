using System.Data.SQLite;
using StockManagerWithAI.Models;
using StockManagerWithAI.Persistence.ConnectionFactories;

namespace StockManagerWithAI.Persistence.Repositories;

public class StockRepository : IStockRepository
{
	private readonly IConnectionFactory _connectionFactory;
	public StockRepository(IConnectionFactory connectionFactory)
	{
		_connectionFactory = connectionFactory;
	}
	
	public int Add(StockItem item)
	{
		const string insertQuery = "INSERT INTO Stocks (Name, Isin, Price, Quantity) VALUES (@Name, @Isin, @Price, @Quantity)";

		using var command = new SQLiteCommand(insertQuery, _connectionFactory.Connection);
		AddCommandParameters(command, item.Name, item.ISIN, item.Price, item.Quantity);

		return ExecuteNonQueryCommand(command);
	}
	private static int ExecuteNonQueryCommand(SQLiteCommand command)
	{
		try
		{
			var result = command.ExecuteNonQuery();
			Console.WriteLine("Record inserted successfully.");
			return result;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred: {ex.Message}");
			return 0;
		}
	}
	public int UpdatePrice(string isin, decimal newPrice)
	{
		const string updateQuery = "UPDATE Stocks SET Price = @Price WHERE Isin = @Isin";

		using var command = new SQLiteCommand(updateQuery, _connectionFactory.Connection);
		AddCommandParameters(command, isin: isin, price: newPrice);

		return ExecuteNonQueryCommand(command);
	}

	private static void AddCommandParameters(
		SQLiteCommand command,
		string? name = null,
		string? isin = null,
		decimal? price = null,
		decimal? quantity = null)
	{
		if (!string.IsNullOrEmpty(name))
		{
			command.Parameters.AddWithValue("@Name", name);
		}

		if (!string.IsNullOrEmpty(isin))
		{
			command.Parameters.AddWithValue("@Isin", isin);
		}

		if (price != default)
		{
			command.Parameters.AddWithValue("@Price", isin);
		}

		if (quantity != default)
		{
			command.Parameters.AddWithValue("@Quantity", quantity);
		}
	}
	
	public int UpdateQuantity(string isin, decimal newQuantity)
	{
		const string updateQuery = "UPDATE Stocks SET Quantity = @Quantity WHERE Isin = @Isin";

		using var command = new SQLiteCommand(updateQuery, _connectionFactory.Connection);
		AddCommandParameters(command, isin: isin, quantity: newQuantity);

		return ExecuteNonQueryCommand(command);
	}
	
	public StockItem? GetById(string isin)
	{
		const string selectQuery = "SELECT Name, Isin, Price, Quantity FROM Stocks WHERE Isin = @Isin";

		using var command = new SQLiteCommand(selectQuery, _connectionFactory.Connection);
		AddCommandParameters(command, isin: isin);

		return ExecuteSelectCommand(command);
	}

	private static StockItem? ExecuteSelectCommand(SQLiteCommand command)
	{
		using var reader = command.ExecuteReader();
		if (reader.Read())
		{
			return new StockItem
			{
				Name = reader.GetString(reader.GetOrdinal("Name")),
				ISIN = reader.GetString(reader.GetOrdinal("Isin")),
				Price = reader.GetDecimal(reader.GetOrdinal("Price")),
				Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity"))
			};
		}
		return null;
	}

	public IEnumerable<StockItem> GetAll()
	{
		const string selectQuery = "SELECT Name, Isin, Price, Quantity FROM Stocks";
		var list = new List<StockItem>();

		using var command = new SQLiteCommand(selectQuery, _connectionFactory.Connection);
		return ExecuteSelectAllCommand(command, list);
	}

	private static IEnumerable<StockItem> ExecuteSelectAllCommand(SQLiteCommand command, List<StockItem> list)
	{
		using var reader = command.ExecuteReader();
		while (reader.Read())
		{
			list.Add(new StockItem
			{
				Name = reader.GetString(reader.GetOrdinal("Name")),
				ISIN = reader.GetString(reader.GetOrdinal("Isin")),
				Price = reader.GetDecimal(reader.GetOrdinal("Price")),
				Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity"))
			});
		}
		return list;
	}
	
	public IEnumerable<StockItem> GetByMaxQuantity(decimal maxStockQuantity)
	{
		const string selectQuery = "SELECT Name, Isin, Price, Quantity FROM Stocks WHERE Quantity <= @Quantity";
		var list = new List<StockItem>();

		using var command = new SQLiteCommand(selectQuery, _connectionFactory.Connection);
		AddCommandParameters(command, quantity: maxStockQuantity);

		return ExecuteSelectAllCommand(command, list);
	}

	private static void AddMaxQuantityParameters(SQLiteCommand command, decimal maxStockQuantity)
	{
		command.Parameters.AddWithValue("@MaxQuantity", maxStockQuantity);
	}

	public bool CreateStocksTable()
	{
		var query = $@"CREATE TABLE Stocks (
			Isin TEXT NOT NULL UNIQUE,
			Name TEXT NOT NULL,
			Price DECIMAL NOT NULL,
			Quantity DECIMAL NOT NULL)";
			
		using (var command = new SQLiteCommand(query, _connectionFactory.Connection))
		{
			try
			{
				command.ExecuteNonQuery();
			}
			catch (Exception ex)
			{ 
				Console.WriteLine($"An error occurred: {ex.Message}");
				return false;
			}

			return true;
		}
	}
}
