﻿using StockManager.Persistence.ConnectionFactories;
using StockManager.Persistence.Repositories;
using StockManager.Services;
using StockManager.UserInterface;

namespace StockManager;

class Program
{
	static void Main(string[] args)
	{
		//TODO: specify path on Your machine for local DB creation/usage
		var dbFilePath = @"C:\repos\StockManager\Stocks3.db"; //TODO: move to appsettings but leaving here for better readability
		var connectionString = $"Data Source={dbFilePath}";
		var connectionFactory = new SQLiteConnectionFactory(connectionString);
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