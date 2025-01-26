namespace StockManagerWithAI.UserInterface.InputOutput;

public static class ConsoleIO
{
	public static decimal ReadQuantity()
	{
		Console.WriteLine("Please provide new Stock Quantity: ");
		Console.WriteLine("Use . (dot symbol) as a decimal separator");
		var quantity = Console.ReadLine();
		
		if (!decimal.TryParse(quantity, out var quantityAsDec) || quantity.Contains(','))
		{
			Console.WriteLine("Invalid Stock Quantity");
			return ReadQuantity(); //todo replace with while + continue?
		}

		return quantityAsDec;
	}

	public static decimal ReadPrice()
	{
		Console.WriteLine("Please provide new Stock Price: ");
		Console.WriteLine("Use . (dot symbol) as a decimal separator");
		var price = Console.ReadLine();
		if (!decimal.TryParse(price, out var priceAsDec) || price.Contains(',')) //todo handle these as valid separators?
		{
			Console.WriteLine("Invalid Stock Price");
			return ReadPrice();
		};

		return priceAsDec;
	}
	
	public static string ReadName()
	{
		Console.WriteLine("Please provide Stock Name: ");
		var name = Console.ReadLine();
		
		if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
		{
			Console.WriteLine("Invalid Stock Name");
			return ReadName();
		}
		return name;
	}

	public static string ReadIsin()
	{
		Console.WriteLine("Please provide Stock ISIN: ");
		var isin = Console.ReadLine();
		
		if (string.IsNullOrEmpty(isin) || string.IsNullOrWhiteSpace(isin) || isin.Length != 12)
		{
			Console.WriteLine("Invalid Stock ISIN");
			return ReadIsin();
		}

		return isin;
	}
	
	public static string ReadInputForSearching()
	{
		Console.WriteLine("Please provide partial Stock Name or ISIN: ");
		var input = Console.ReadLine();
		
		if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
		{
			Console.WriteLine("Invalid Input");
			return ReadInputForSearching();
		}

		return input;
	}
}