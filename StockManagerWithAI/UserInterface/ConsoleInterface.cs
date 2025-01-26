using StockManagerWithAI.Models;
using StockManagerWithAI.Services;
using StockManagerWithAI.UserInterface.InputOutput;
using System;

namespace StockManagerWithAI.UserInterface
{
    public class ConsoleInterface
    {
        private readonly IStockManager _stockManagerService;

        public ConsoleInterface(IStockManager stockManagerService)
        {
            _stockManagerService = stockManagerService;
        }

        public bool Start()
        {
            var option = ListInitialUserOptions();

            if (int.TryParse(option, out var responseAsInt0) && responseAsInt0 == 0)
            {
                return false;
            }

            if (!int.TryParse(option, out var optionAsInt) || optionAsInt <= 0 || optionAsInt > 7)
            {
                Console.WriteLine("Invalid input, please choose again...");
                Start();
                return TryAgain();
            }

            ActOnInitialUserOption(optionAsInt);

            return TryAgain();
        }

        private string ListInitialUserOptions()
        {
            Console.WriteLine("Please choose an action and press key for the Stock Items: ");
            Console.WriteLine("1 - For Listing All Stock Items");
            Console.WriteLine("2 - For Retrieving A Stock Item by ID (ISIN)");
            Console.WriteLine("3 - For Inserting a new Stock Item");
            Console.WriteLine("4 - For Updating the Price of an Existing Stock Item");
            Console.WriteLine("5 - For Updating the Quantity of an Existing Stock Item");
            Console.WriteLine("6 - For Searching of Existing Stock Items By Name or Partial ISIN");
            Console.WriteLine("7 - For Searching of Existing Stock Items By Maximum Quantity");
            Console.WriteLine("0 - To Exit");

            return Console.ReadLine();
        }

        private void ActOnInitialUserOption(int chosenAction)
        {
            try
            {
                switch (chosenAction)
                {
                    case 1:
                        HandleOperationOutcome(GetAllStocksAction());
                        break;
                    case 2:
                        HandleOperationOutcome(GetStockAction());
                        break;
                    case 3:
                        HandleOperationOutcome(AddStockAction());
                        break;
                    case 4:
                        HandleOperationOutcome(UpdateStockPriceAction());
                        break;
                    case 5:
                        HandleOperationOutcome(UpdateStockQuantityAction());
                        break;
                    case 6:
                        HandleOperationOutcome(SearchStocksAction());
                        break;
                    case 7:
                        HandleOperationOutcome(GetStockByMaxQuantityAction());
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private bool TryAgain()
        {
            Console.WriteLine("Would you like to execute another Stock Operation?");
            Console.WriteLine("Press 1 to continue...");
            Console.WriteLine("Press 0 to exit...");

            var input = Console.ReadLine();

            if (!int.TryParse(input, out var inputAsInt) || inputAsInt is not (0 or 1))
            {
                Console.WriteLine("Invalid input");
                return TryAgain();
            }

            return inputAsInt == 1;
        }

        private void HandleOperationOutcome(bool success)
        {
            if (success)
            {
                Console.WriteLine("Operation finalized successfully");
            }
            else
            {
                Console.WriteLine("Operation unsuccessful");
            }
        }

        private bool GetAllStocksAction()
        {
            Console.WriteLine("Starting Retrieve All Stock Items operation...");

            var result = _stockManagerService.GetAllStocks()?.ToList();

            if (result == null || !result.Any())
            {
                Console.WriteLine("No stocks found");
                return false;
            }

            Console.WriteLine($"Retrieved {result.Count} stocks: ");

            foreach (var stock in result)
            {
                Console.WriteLine(stock.ToString());
                Console.WriteLine();
            }

            return true;
        }

        private bool GetStockAction()
        {
            Console.WriteLine("Starting Retrieve Stock Item by ISIN operation...");
            var isin = ConsoleIO.ReadIsin();
            var result = _stockManagerService.GetStockById(isin);

            if (result == null)
            {
                Console.WriteLine("No stocks found");
                return false;
            }

            Console.WriteLine($"Retrieved {result.Name} stock: ");
            Console.WriteLine(result.ToString());
            return true;
        }

        private bool AddStockAction()
        {
            Console.WriteLine("Starting Save new Stock Item operation...");

            var isin = ConsoleIO.ReadIsin();
            var name = ConsoleIO.ReadName();
            var quantity = ConsoleIO.ReadQuantity();
            var price = ConsoleIO.ReadPrice();

            var item = new StockItem
            {
                Name = name,
                ISIN = isin,
                Price = price,
                Quantity = quantity
            };

            return _stockManagerService.AddStock(item);
        }

        private bool UpdateStockPriceAction()
        {
            Console.WriteLine("Starting Update Stock Price operation...");

            var isin = ConsoleIO.ReadIsin();
            var newPrice = ConsoleIO.ReadPrice();

            var success = _stockManagerService.UpdateStockPrice(isin, newPrice);
            Console.WriteLine($"Successfully updated {isin} stock price to {newPrice}");

            return success;
        }

        private bool UpdateStockQuantityAction()
        {
            Console.WriteLine("Starting Update Stock Quantity operation...");

            var isin = ConsoleIO.ReadIsin();
            var newQuantity = ConsoleIO.ReadQuantity();

            var success = _stockManagerService.UpdateStockQuantity(isin, newQuantity);
            Console.WriteLine($"Successfully updated {isin} stock quantity to {newQuantity}");

            return success;
        }

        private bool SearchStocksAction()
        {
            Console.WriteLine("Starting Search Stock operation...");

            var input = ConsoleIO.ReadInputForSearching();

            var searchByIsin = false;

            if (input.Length == 12)
            {
                Console.WriteLine($"Is input : {input} an exact Stock Isin?");
                Console.WriteLine("Press Y for YES");
                Console.WriteLine("Press N for NO");

                var input2 = Console.ReadLine();

                if (input2.ToUpper().Equals("Y"))
                {
                    searchByIsin = true;
                }
            }

            var result = _stockManagerService.SearchStocks(input, searchByIsin)?.ToList();

            if (result == null || !result.Any())
            {
                Console.WriteLine("No stocks found");
                return false;
            }

            Console.WriteLine($"Retrieved {result.Count} stocks: ");

            foreach (var stock in result)
            {
                Console.WriteLine(stock.ToString());
                Console.WriteLine();
            }

            return true;
        }

        private bool GetStockByMaxQuantityAction()
        {
            Console.WriteLine("Starting Retrieve Stock Items By Maximum Quantity...");

            var maxQuantity = ConsoleIO.ReadQuantity();
            var result = _stockManagerService.GetStocksByMaxQuantity(maxQuantity)?.ToList();

            if (result == null || !result.Any())
            {
                Console.WriteLine("No stocks found");
                return false;
            }

            Console.WriteLine($"Retrieved {result.Count} stocks: ");

            foreach (var stock in result)
            {
                Console.WriteLine(stock.ToString());
                Console.WriteLine();
            }

            return true;
        }
    }
}
