using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using StockManagerWithAI.Models;
using StockManagerWithAI.Persistence.Repositories;
using StockManagerWithAI.Services;

namespace StockManager.Tests.Services.Tests
{
    [TestFixture]
    public class StockManagementServiceTests
    {
        private Mock<IStockRepository> _mockStockRepository;
        private StockManagementService _service;

        [SetUp]
        public void Setup()
        {
            _mockStockRepository = new Mock<IStockRepository>();
            _service = new StockManagementService(_mockStockRepository.Object);
        }

        [Test]
        public void AddStock_ShouldThrowException_WhenStockItemExists()
        {
            var stockItem = new StockItem { ISIN = "ABC123" };
            _mockStockRepository.Setup(repo => repo.GetById("ABC123")).Returns(stockItem);

            var ex = Assert.Throws<ApplicationException>(() => _service.AddStock(stockItem));
            Assert.AreEqual("Save operation failed. A stock item with ISIN: ABC123 already exists.", ex.Message);
        }

        [Test]
        public void AddStock_ShouldAddStock_WhenStockItemDoesNotExist()
        {
            var stockItem = new StockItem { ISIN = "ABC123" };
            _mockStockRepository.Setup(repo => repo.GetById("ABC123")).Returns((StockItem)null);
            _mockStockRepository.Setup(repo => repo.Add(stockItem)).Returns(1);

            var result = _service.AddStock(stockItem);

            Assert.IsTrue(result);
            _mockStockRepository.Verify(repo => repo.Add(stockItem), Times.Once);
        }

        [Test]
        public void UpdateStockQuantity_ShouldThrowException_WhenStockItemDoesNotExist()
        {
            _mockStockRepository.Setup(repo => repo.GetById("ABC123")).Returns((StockItem)null);

            var ex = Assert.Throws<ApplicationException>(() => _service.UpdateStockQuantity("ABC123", 10));
            Assert.AreEqual("Update operation failed. A stock item with ISIN: ABC123 does not exist.", ex.Message);
        }

        [Test]
        public void UpdateStockQuantity_ShouldUpdateQuantity_WhenStockItemExists()
        {
            var stockItem = new StockItem { ISIN = "ABC123" };
            _mockStockRepository.Setup(repo => repo.GetById("ABC123")).Returns(stockItem);
            _mockStockRepository.Setup(repo => repo.UpdateQuantity("ABC123", 10)).Returns(1);

            var result = _service.UpdateStockQuantity("ABC123", 10);

            Assert.IsTrue(result);
            _mockStockRepository.Verify(repo => repo.UpdateQuantity("ABC123", 10), Times.Once);
        }

        [Test]
        public void UpdateStockPrice_ShouldThrowException_WhenStockItemDoesNotExist()
        {
            _mockStockRepository.Setup(repo => repo.GetById("ABC123")).Returns((StockItem)null);

            var ex = Assert.Throws<ApplicationException>(() => _service.UpdateStockPrice("ABC123", 100));
            Assert.AreEqual("Update operation failed. A stock item with ISIN: ABC123 does not exist.", ex.Message);
        }

        [Test]
        public void UpdateStockPrice_ShouldUpdatePrice_WhenStockItemExists()
        {
            var stockItem = new StockItem { ISIN = "ABC123" };
            _mockStockRepository.Setup(repo => repo.GetById("ABC123")).Returns(stockItem);
            _mockStockRepository.Setup(repo => repo.UpdatePrice("ABC123", 100)).Returns(1);

            var result = _service.UpdateStockPrice("ABC123", 100);

            Assert.IsTrue(result);
            _mockStockRepository.Verify(repo => repo.UpdatePrice("ABC123", 100), Times.Once);
        }

        [Test]
        public void GetAllStocks_ShouldReturnAllStocks()
        {
            var stockItems = new List<StockItem> { new StockItem { ISIN = "ABC123" }, new StockItem { ISIN = "DEF456" } };
            _mockStockRepository.Setup(repo => repo.GetAll()).Returns(stockItems);

            var result = _service.GetAllStocks();

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(item => item.ISIN == "ABC123"));
            Assert.IsTrue(result.Any(item => item.ISIN == "DEF456"));
        }

        [Test]
        public void GetStockById_ShouldThrowException_WhenStockItemDoesNotExist()
        {
            _mockStockRepository.Setup(repo => repo.GetById("ABC123")).Returns((StockItem)null);

            var ex = Assert.Throws<ApplicationException>(() => _service.GetStockById("ABC123"));
            Assert.AreEqual("Retrieve operation failed. A stock item with ISIN: ABC123 does not exist.", ex.Message);
        }

        [Test]
        public void GetStockById_ShouldReturnStockItem_WhenStockItemExists()
        {
            var stockItem = new StockItem { ISIN = "ABC123" };
            _mockStockRepository.Setup(repo => repo.GetById("ABC123")).Returns(stockItem);

            var result = _service.GetStockById("ABC123");

            Assert.AreEqual(stockItem, result);
        }

        [Test]
        public void GetStocksByMaxQuantity_ShouldReturnStocks_WhenStocksExist()
        {
            var stockItems = new List<StockItem> { new StockItem { ISIN = "ABC123", Quantity = 5 }, new StockItem { ISIN = "DEF456", Quantity = 10 } };
            _mockStockRepository.Setup(repo => repo.GetByMaxQuantity(10)).Returns(stockItems);

            var result = _service.GetStocksByMaxQuantity(10);

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void SearchStocks_ShouldReturnStockByISIN_WhenSearchByIsinIsTrue()
        {
            var stockItem = new StockItem { ISIN = "ABC123" };
            _mockStockRepository.Setup(repo => repo.GetById("ABC123")).Returns(stockItem);

            var result = _service.SearchStocks("ABC123", true);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(stockItem, result.First());
        }

        [Test]
        public void SearchStocks_ShouldReturnStockByName_WhenSearchByIsinIsFalse()
        {
            var stockItems = new List<StockItem> 
            { 
                new StockItem { ISIN = "ABC123", Name = "TestStock1" }, 
                new StockItem { ISIN = "DEF456", Name = "TestStock2" } 
            };
            _mockStockRepository.Setup(repo => repo.GetAll()).Returns(stockItems);

            var result = _service.SearchStocks("teststock", false);

            Assert.AreEqual(2, result.Count());
        }
    }
}
