using StockManagerWithAI.Persistence.ConnectionFactories;
using StockManagerWithAI.Persistence.Repositories;
using StockManagerWithAI.Services;
using StockManagerWithAI.UserInterface;

namespace StockManagerWithAI;

class Program
{
	static void Main(string[] args)
	{
		//TODO: specify path on Your machine for local DB creation/usage
		var dbFilePath = @"C:\repos\StockManager\Stocks4.db";
		var connectionString = $"Data Source={dbFilePath}";
		var connectionFactory = new SqLiteConnectionFactory(connectionString);
		var stockRepository = new StockRepository(connectionFactory);
		
		var myService = new StockManagementService(stockRepository);

		//Recommended to comment this region out after app has been ran and local DB created
		#region Local Database Creation flow
		var creatingDatabase = DatabaseCreationConsoleInterface.Start();
		if (creatingDatabase)
		{
			var result = stockRepository.CreateStocksTable();
			Console.WriteLine($"Database successfully created: {result} in {dbFilePath}");
		}
		
		#endregion
		
		var ui = new ConsoleInterface(myService);
		var keepRunning = ui.Start();
		
		while (keepRunning)
		{
			keepRunning = ui.Start();
		}

		Environment.Exit(0);
	}
}