namespace StockManager.UserInterface;

public static class DatabaseCreationConsoleInterface
{
	public static bool Start()
	{
		Console.WriteLine("Would you like to create a local database before starting Stock Management Operations?");
		
		Console.WriteLine("Press Y for YES");
		Console.WriteLine("Press N for NO");

		var input2 = Console.ReadLine();

		if (input2.ToUpper().Equals("Y"))
		{
			return true;
		}
		
		if (input2.ToUpper().Equals("N"))
		{
			return false;
		}
		
		Console.WriteLine("Invalid input");
		return Start();
	}
	
}