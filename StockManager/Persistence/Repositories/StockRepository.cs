using System.Data.SQLite;
using StockManager.Models;
using StockManager.Persistence.ConnectionFactories;

namespace StockManager.Persistence.Repositories;

public class StockRepository : IStockRepository
{
	private readonly IConnectionFactory _connectionFactory;
	public StockRepository(IConnectionFactory connectionFactory)
	{
		_connectionFactory = connectionFactory;
	}

	public int Add(StockItem item)
	{
		var insertQuery = "INSERT INTO Stocks (Name, Isin, Price, Quantity) VALUES (@Name, @Isin, @Price, @Quantity)";

		using (var command = new SQLiteCommand(insertQuery, _connectionFactory.Connection))
		{
			command.Parameters.AddWithValue("@Name", item.Name);
			command.Parameters.AddWithValue("@Isin", item.ISIN);
			command.Parameters.AddWithValue("@Price", item.Price);
			command.Parameters.AddWithValue("@Quantity", item.Quantity);

			var result = 0;
			
			try
			{
				result = command.ExecuteNonQuery();
				Console.WriteLine("Record inserted successfully.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
			}

			return result;
		}
	}

	public int UpdatePrice(string isin, decimal newPrice)
	{
		var insertQuery = "UPDATE Stocks SET Price = @Price WHERE Isin = @Isin ";

		using (var command = new SQLiteCommand(insertQuery, _connectionFactory.Connection))
		{
			command.Parameters.AddWithValue("@Isin", isin);
			command.Parameters.AddWithValue("@Price", newPrice);

			var result = 0;

			try
			{
				result = command.ExecuteNonQuery();
				Console.WriteLine("Record updated successfully.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
			}

			return result;
		}
	}

	public int UpdateQuantity(string isin, decimal newQuantity)
	{
		var insertQuery = "UPDATE Stocks SET Quantity = @Quantity WHERE Isin = @Isin ";

		using (var command = new SQLiteCommand(insertQuery, _connectionFactory.Connection))
		{
			command.Parameters.AddWithValue("@Isin", isin);
			command.Parameters.AddWithValue("@Quantity", newQuantity);

			var result = 0;

			try
			{
				result = command.ExecuteNonQuery();
				Console.WriteLine("Record updated successfully.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
			}

			return result;
		}
	}

	public StockItem? GetById(string isin)
	{
		var selectQuery = "SELECT Name, Isin, Price, Quantity FROM Stocks WHERE Isin = @Isin";
		
		using (var command = new SQLiteCommand(selectQuery, _connectionFactory.Connection))
		{
			command.Parameters.AddWithValue("@Isin", isin);

			using (var reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					return new StockItem()
					{
						Name = reader.GetString(reader.GetOrdinal("Name")),
						ISIN = reader.GetString(reader.GetOrdinal("Isin")),
						Price = reader.GetDecimal(reader.GetOrdinal("Price")),
						Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
					};
				}
			}
		}
		return null;
	}
	
	public IEnumerable<StockItem> GetAll()
	{
		var list = new List<StockItem>();
		var selectQuery = "SELECT Name, Isin, Price, Quantity FROM Stocks";
		
		using (var command = new SQLiteCommand(selectQuery, _connectionFactory.Connection))
		{
			using (var reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					list.Add(new StockItem()
					{
						Name = reader.GetString(reader.GetOrdinal("Name")),
						ISIN = reader.GetString(reader.GetOrdinal("Isin")),
						Price = reader.GetDecimal(reader.GetOrdinal("Price")),
						Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
					});
				}
			}
		}
		return list;
	}

	public IEnumerable<StockItem> GetByMaxQuantity(decimal maxStockQuantity)
	{
		var selectQuery = "SELECT Name, Isin, Price, Quantity FROM Stocks WHERE Quantity <= @MaxQuantity";
		var list = new List<StockItem>();
		
		using (var command = new SQLiteCommand(selectQuery, _connectionFactory.Connection))
		{
			command.Parameters.AddWithValue("@MaxQuantity", maxStockQuantity);

			using (var reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					list.Add(new StockItem()
					{
						Name = reader.GetString(reader.GetOrdinal("Name")),
						ISIN = reader.GetString(reader.GetOrdinal("Isin")),
						Price = reader.GetDecimal(reader.GetOrdinal("Price")),
						Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
					});
				}
			}
		}
		return list;

	}

	public bool CreateStocksTable()
	{
		//TODO: table structure, data types up for review
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
