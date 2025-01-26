using StockManager.Models;
using StockManager.Persistence.Repositories;

namespace StockManager.Services;

public class StockManagementService : IStockManager
{
	private readonly IStockRepository _stockRepository;
	public StockManagementService(IStockRepository stockRepository)
	{
		_stockRepository = stockRepository;
	}
	
	public bool AddStock(StockItem item)
	{
		var duplicate = _stockRepository.GetById(item.ISIN);

		if (duplicate != null)
		{
			Console.WriteLine($"Save operation failed. A stock item with ISIN: {item.ISIN} already exists");
			//todo better to use exceptions but for the sake of simplicity will print in console throw new ApplicationException($"Save operation failed. A stock item with ISIN: {item.ISIN} already exists");
			return false;
		}
		
		var result = _stockRepository.Add(item);

		return result == 1;
	}

	public bool UpdateStockQuantity(string isin, decimal newQuantity)
	{
		var existingItem = _stockRepository.GetById(isin);
		
		if (existingItem == null)
		{
			Console.WriteLine($"Update operation failed. A stock item with ISIN: {isin} does not exist.");
			return false;
		}
		
		var result = _stockRepository.UpdateQuantity(isin, newQuantity);

		return result == 1;
	}
	
	public bool UpdateStockPrice(string isin, decimal newPrice)
	{
		var existingItem = _stockRepository.GetById(isin);
		
		if (existingItem == null)
		{
			Console.WriteLine($"Update operation failed. A stock item with ISIN: {isin} does not exist.");
			return false;
		}
		
		var result = _stockRepository.UpdatePrice(isin, newPrice);
		
		return result == 1;
	}

	public IEnumerable<StockItem>? GetAllStocks()
	{
		return _stockRepository.GetAll();
	}

	public StockItem? GetStockById(string isin)
	{
		var result = _stockRepository.GetById(isin);

		if (result == null)
		{
			Console.WriteLine($"Retrieve operation failed. A stock item with ISIN: {isin} does not exist");
			return null;
		}

		return result;
	}

	public IEnumerable<StockItem> GetStocksByMaxQuantity(decimal maxQuantityValue)
	{
		return _stockRepository.GetByMaxQuantity(maxQuantityValue);
	}

	public IEnumerable<StockItem>? SearchStocks(string input, bool searchByIsin)
	{
		if (searchByIsin)
		{
			var item = GetStockById(input);
			return item != null ? new List<StockItem> { item } : null;
		}
		
		var allStocks = GetAllStocks(); //TODO: this is per requirement to use LINQ, but actually an antipattern - unnecessary huge select to db
		var stocksFilteredByName = allStocks?.Where(stock => stock.Name.ToLower().Contains(input.ToLower())).ToList();
		
		return stocksFilteredByName;
	}
}