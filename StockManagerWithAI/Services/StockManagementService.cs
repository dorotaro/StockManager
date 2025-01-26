using StockManagerWithAI.Models;
using StockManagerWithAI.Persistence.Repositories;

namespace StockManagerWithAI.Services
{
    public class StockManagementService : IStockManager
    {
        private readonly IStockRepository _stockRepository;

        public StockManagementService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public bool AddStock(StockItem item)
        {
            if (_stockRepository.GetById(item.ISIN) != null)
            {
                throw new ApplicationException($"Save operation failed. A stock item with ISIN: {item.ISIN} already exists.");
            }

            return _stockRepository.Add(item) == 1;
        }

        public bool UpdateStockQuantity(string isin, decimal newQuantity)
        {
            var existingItem = _stockRepository.GetById(isin);

            if (existingItem == null)
            {
                throw new ApplicationException($"Update operation failed. A stock item with ISIN: {isin} does not exist.");
            }

            return _stockRepository.UpdateQuantity(isin, newQuantity) == 1;
        }

        public bool UpdateStockPrice(string isin, decimal newPrice)
        {
            var existingItem = _stockRepository.GetById(isin);

            if (existingItem == null)
            {
                throw new ApplicationException($"Update operation failed. A stock item with ISIN: {isin} does not exist.");
            }

            return _stockRepository.UpdatePrice(isin, newPrice) == 1;
        }

        public IEnumerable<StockItem> GetAllStocks()
        {
            return _stockRepository.GetAll() ?? Enumerable.Empty<StockItem>();
        }

        public StockItem GetStockById(string isin)
        {
            var result = _stockRepository.GetById(isin);

            if (result == null)
            {
                throw new ApplicationException($"Retrieve operation failed. A stock item with ISIN: {isin} does not exist.");
            }

            return result;
        }

        public IEnumerable<StockItem> GetStocksByMaxQuantity(decimal maxQuantityValue)
        {
            return _stockRepository.GetByMaxQuantity(maxQuantityValue);
        }

        public IEnumerable<StockItem> SearchStocks(string input, bool searchByIsin)
        {
            if (searchByIsin)
            {
                var item = GetStockById(input);
                return new List<StockItem> { item };
            }

            return GetAllStocks().Where(stock => stock.Name.ToLower().Contains(input.ToLower())).ToList();
        }
    }
}