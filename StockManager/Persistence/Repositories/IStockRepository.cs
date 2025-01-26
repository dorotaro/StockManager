using StockManager.Models;

namespace StockManager.Persistence.Repositories;

public interface IStockRepository
{
	/// <returns>Number of changes (number of rows) updated in Database</returns>
	int Add(StockItem stock);

	/// <returns>Number of changes (number of rows) updated in Database</returns>
	int UpdatePrice(string isin, decimal newPrice);

	/// <returns>Number of changes (number of rows) updated in Database</returns>
	int UpdateQuantity(string isin, decimal newQuantity);
	StockItem? GetById(string isin);
	IEnumerable<StockItem> GetAll();
	IEnumerable<StockItem> GetByMaxQuantity(decimal maxStockQuantity);
}