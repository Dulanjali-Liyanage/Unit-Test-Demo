using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SampleProject.Controllers;
using SampleProject.Interfaces;
using SampleProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTestDemo
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Index_ReturnsAViewResult_WithAListofProducts()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.ListAsync())
                .ReturnsAsync(GetProducts());
            var controller = new ProductsController(mockRepo.Object);

            var result = controller.Index();

            // Assert
            var viewResult = await result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.IsAssignableFrom<List<Product>>(viewResult.ViewData.Model);

            var model = viewResult.ViewData.Model as List<Product>;
            
            Assert.AreEqual(3, model.Count);
        }

        [Test]
        public async Task Details_GetByIDViewResult()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(2))
                .ReturnsAsync(GetProductById());
            var controller = new ProductsController(mockRepo.Object);

            var result = controller.Details(2);

            var product = new Product()
            {
                ProductId = 2,
                ProductName = "A2"
            };

            //Assert
            var viewResult = await result as ViewResult;
            Assert.IsNotNull(viewResult);

            var model = viewResult.ViewData.Model as Product;
            Assert.AreEqual(product.ProductId,model.ProductId);
        }

        [Test]
        public async Task Details_ReturnNotFound_WhenProductIsNull()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(3))
                .ReturnsAsync(GetNullProduct());
            var controller = new ProductsController(mockRepo.Object);

            var result = controller.Details(3);

            //Assert
            var retunrNotFound = await result as NotFoundResult;
            Assert.IsNotNull(retunrNotFound);
        }

        [Test]
        public async Task WhenProductCreate_IfModelValid_ReturnToIndex()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.AddAsync(new Product() { ProductId = 4, ProductName = "A4" }))
                .Returns(Task.CompletedTask).Verifiable();
                
            var controller = new ProductsController(mockRepo.Object);

            var result = await controller.Create(new Product() { ProductId = 4, ProductName = "A4" });

            var redirectToActionResult = result as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(redirectToActionResult);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);

        }

        private List<Product> GetProducts ()
        {
            var products = new List<Product>();

            products.Add(new Product()
            {
                ProductId = 1,
                ProductName = "A1"
            });

            products.Add(new Product()
            {
                ProductId = 2,
                ProductName = "A2"
            });

            products.Add(new Product()
            {
                ProductId = 3,
                ProductName = "A3"
            });

            return products;
        }

        private Product GetProductById()
        {
            var product = new Product()
            {
                ProductId = 2,
                ProductName = "A2"
            };

            return product;
        }
        
        private Product GetNullProduct()
        {
            return null;
        }
    }
}