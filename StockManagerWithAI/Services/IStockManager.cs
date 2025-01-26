using StockManagerWithAI.Models;

namespace StockManagerWithAI.Services;

public interface IStockManager
{
	/// <returns>True if operation was successful</returns>
	bool AddStock(StockItem item);
	
	/// <returns>True if operation was successful</returns>
	bool UpdateStockQuantity(string isin, decimal newQuantity);
	
	/// <returns>True if operation was successful</returns>
	bool UpdateStockPrice(string isin, decimal newPrice);
	IEnumerable<StockItem>? GetAllStocks();
	StockItem? GetStockById(string isin);
	IEnumerable<StockItem>? GetStocksByMaxQuantity(decimal maxQuantityValue);
	IEnumerable<StockItem>? SearchStocks(string input, bool searchByIsin);
}