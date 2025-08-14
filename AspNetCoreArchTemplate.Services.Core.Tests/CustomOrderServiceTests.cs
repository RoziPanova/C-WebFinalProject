namespace AspNetCoreArchTemplate.Services.Core.Tests
{
    using Core;
    using Core.Interfaces;
    using Data.Models;
    using Data.Repository.Interfaces;
    using MockQueryable;
    using Moq;
    using Web.ViewModels.CustomOrder;

    using static GCommon.ApplicationConstants;

    [TestFixture]
    public class CustomOrderServiceTests
    {
        private Mock<ICustomOrderRepository> customOrderRepositoryMock;
        private ICustomOrderService customOrderService;

        [SetUp]
        public void SetUp()
        {
            customOrderRepositoryMock = new Mock<ICustomOrderRepository>(MockBehavior.Strict);
            customOrderService = new CustomOrderService(customOrderRepositoryMock.Object);
        }
        [Test]
        public void PassAlways()
        {
            // Test that will always pass to show that the SetUp is working
            Assert.Pass();
        }


        // ----------------------------
        // GetUserCustomOrdersAsync
        // ----------------------------
        [Test]
        public async Task GetUserCustomOrdersAsyncShouldReturnEmptyCollectionWhenNoOrdersFound()
        {
            IQueryable<CustomOrder> emptyOrders = new List<CustomOrder>().BuildMock();
            this.customOrderRepositoryMock
                .Setup(r => r.GetAllAttached()).Returns(emptyOrders);

            IEnumerable<CustomOrderListViewModel> customOrderListViews = await customOrderService
                .GetUserCustomOrdersAsync(Guid.NewGuid().ToString());

            Assert.IsNotNull(customOrderListViews);
            Assert.IsEmpty(customOrderListViews);
        }

        [Test]
        public async Task GetUserCustomOrdersAsyncShouldReturnOnlyOrdersForGivenUser()
        {
            IQueryable<CustomOrder> customOrder = new List<CustomOrder>
            {
                    new CustomOrder
                    {
                        Id = Guid.NewGuid(),
                        UserId = Guid.Parse("4595a228-bda1-4331-a990-a363b68e6111").ToString(),
                        UserName="Test",
                        PhoneNumber = "0879116989",
                        Address="TestAddress1",
                        RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow),
                        Details = "Order A"
                    },
                    new CustomOrder
                    {
                        Id = Guid.NewGuid(),
                        UserId = Guid.Parse("f73717d2-619d-4094-af39-c81c82cac970").ToString(),
                         UserName="Test2",
                        PhoneNumber = "0879116789",
                        Address="TestAddress2",
                        RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow),
                        Details = "Order B"
                    }
            }.BuildMock();

            this.customOrderRepositoryMock
                .Setup(r => r.GetAllAttached()).Returns(customOrder);

            IEnumerable<CustomOrderListViewModel> customOrderListViews = await customOrderService
                .GetUserCustomOrdersAsync(customOrder.First().UserId);

            Assert.That(customOrderListViews.Count(), Is.EqualTo(1));
            Assert.That(customOrderListViews.First().Details, Is.EqualTo("Order A"));
        }

        [Test]
        public async Task GetUserCustomOrdersAsyncShouldMapCorrectlyToViewModel()
        {
            string validGuid = Guid.Parse("4595a228-bda1-4331-a990-a363b68e6111").ToString();
            CustomOrder customOrder = new CustomOrder
            {
                Id = Guid.NewGuid(),
                UserId = validGuid,
                UserName = "Test",
                PhoneNumber = "0879116989",
                Address = "TestAddress1",
                RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Details = "Order A"
            };

            IQueryable<CustomOrder> orders = new List<CustomOrder> { customOrder }.BuildMock();
            this.customOrderRepositoryMock
                .Setup(r => r.GetAllAttached()).Returns(orders);

            IEnumerable<CustomOrderListViewModel> customOrderListViews = await customOrderService
                .GetUserCustomOrdersAsync(validGuid);

            CustomOrderListViewModel customOrderListView = customOrderListViews.First();
            Assert.That(customOrderListView.Id, Is.EqualTo(customOrder.Id.ToString()));
            Assert.That(customOrderListView.Details, Is.EqualTo(customOrder.Details));
            Assert.That(customOrderListView.RequestedDate, Is.EqualTo(customOrder.RequestedDate));
        }

        [Test]
        public async Task GetUserCustomOrdersAsyncShouldReturnMultipleOrdersWhenTheyBelongToUser()
        {
            string validGuid = Guid.Parse("4595a228-bda1-4331-a990-a363b68e6111").ToString();
            IQueryable<CustomOrder> customOrdersQueryable = new List<CustomOrder>
            {
                    new CustomOrder
                    {
                        Id = Guid.NewGuid(),
                        UserId = validGuid,
                        UserName="Test",
                        PhoneNumber = "0879116989",
                        Address="TestAddress1",
                        RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow),
                        Details = "Order A"
                    },
                    new CustomOrder
                    {
                        Id = Guid.NewGuid(),
                        UserId = validGuid,
                        UserName="Test",
                        PhoneNumber = "0879116989",
                        Address="TestAddress1",
                        RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow),
                        Details = "Order B"
                    }
            }.BuildMock();

            this.customOrderRepositoryMock
                .Setup(r => r.GetAllAttached()).Returns(customOrdersQueryable);

            IEnumerable<CustomOrderListViewModel> customOrderListViews = await customOrderService
                .GetUserCustomOrdersAsync(validGuid);

            Assert.That(customOrderListViews.Count(), Is.EqualTo(2));
        }

        // ----------------------------
        // AddCustomOrderAsync
        // ----------------------------
        [Test]
        public async Task AddCustomOrderAsyncShouldReturnTrueWhenOrderIsAdded()
        {
            string validGuid = Guid.Parse("4595a228-bda1-4331-a990-a363b68e6111").ToString();
            CustomOrderFormInputViewModel customOrderFormInput = new CustomOrderFormInputViewModel
            {
                UserName = "John",
                PhoneNumber = "0896332211",
                Address = "Somewhere",
                Details = "Details",
                RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(5)).ToString(AppDateFormat)
            };

            this.customOrderRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<CustomOrder>()))
                .Returns(Task.CompletedTask); // AddAsync returns Task

            this.customOrderRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask); // SaveChangesAsync returns Task

            // Act
            bool isCustomOrderAdded = await customOrderService
                .AddCustomOrderAsync(customOrderFormInput, validGuid);

            // Assert
            Assert.IsTrue(isCustomOrderAdded);

            // Optional: verify that the repository methods were called
            this.customOrderRepositoryMock.Verify(r => r.AddAsync(It.IsAny<CustomOrder>()), Times.Once);
            this.customOrderRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }


        [Test]
        public async Task AddCustomOrderAsyncShouldParseDateCorrectly()
        {
            string dateStr = DateTime.UtcNow.AddDays(10).ToString(AppDateFormat);
            string validGuid = Guid.Parse("4595a228-bda1-4331-a990-a363b68e6111").ToString();
            CustomOrderFormInputViewModel customOrderFormInput = new CustomOrderFormInputViewModel
            {
                UserName = "Jane",
                PhoneNumber = "54321",
                Address = "Elsewhere",
                Details = "Details",
                RequestedDate = dateStr
            };
            this.customOrderRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<CustomOrder>()))
                .Returns(Task.CompletedTask);

            this.customOrderRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            bool isCustomOrderAdded = await customOrderService
                .AddCustomOrderAsync(customOrderFormInput, validGuid);

            // Assert
            Assert.IsTrue(isCustomOrderAdded);

            this.customOrderRepositoryMock
                .Verify(r => r.AddAsync(It.Is<CustomOrder>(
                o => o.RequestedDate == DateOnly.ParseExact(customOrderFormInput.RequestedDate, AppDateFormat)
            )), Times.Once);

            this.customOrderRepositoryMock
                .Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task AddCustomOrderAsyncShouldAssignCorrectUserId()
        {
            string validGuid = Guid.Parse("4595a228-bda1-4331-a990-a363b68e6111").ToString();
            CustomOrderFormInputViewModel customOrderFormInput = new CustomOrderFormInputViewModel
            {
                UserName = "Tom",
                PhoneNumber = "0896332211",
                Address = "Addr",
                Details = "Details",
                RequestedDate = DateTime.UtcNow.AddDays(5).ToString(AppDateFormat)
            };
            this.customOrderRepositoryMock
               .Setup(r => r.AddAsync(It.IsAny<CustomOrder>()))
               .Returns(Task.CompletedTask); // AddAsync returns Task

            this.customOrderRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask); // SaveChangesAsync returns Task

            await customOrderService
                .AddCustomOrderAsync(customOrderFormInput, validGuid);

            this.customOrderRepositoryMock
                .Verify(r => r.AddAsync(It.Is<CustomOrder>(o => o.UserId == validGuid)));
            this.customOrderRepositoryMock
                .Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task AddCustomOrderAsyncShouldAlwaysReturnTrue()
        {
            string validGuid = Guid.Parse("4595a228-bda1-4331-a990-a363b68e6111").ToString();
            CustomOrderFormInputViewModel customOrderFormInput = new CustomOrderFormInputViewModel
            {
                UserName = "Will",
                PhoneNumber = "0896332211",
                Address = "Address",
                Details = "Details",
                RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(6)).ToString(AppDateFormat)
            };
            this.customOrderRepositoryMock
              .Setup(r => r.AddAsync(It.IsAny<CustomOrder>()))
              .Returns(Task.CompletedTask); // AddAsync returns Task

            this.customOrderRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask); // SaveChangesAsync returns Task

            bool isCustomOrderAdded = await customOrderService
                .AddCustomOrderAsync(customOrderFormInput, validGuid);

            Assert.That(isCustomOrderAdded, Is.True);
            // Arrange
            this.customOrderRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<CustomOrder>()))
                .Returns(Task.CompletedTask); // because AddAsync returns Task

            this.customOrderRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask); // SaveChangesAsync returns Task

        }

        // ----------------------------
        // GetCustomOrderForEditAsync
        // ----------------------------
        [Test]
        public async Task GetCustomOrderForEditAsyncShouldReturnNullWhenIdIsInvalid()
        {
            CustomOrderFormInputViewModel? customOrderFormInput = await customOrderService
                .GetCustomOrderForEditAsync("invalid-guid");
            Assert.IsNull(customOrderFormInput);
        }

        [Test]
        public async Task GetCustomOrderForEditAsyncShouldReturnNullWhenOrderNotFound()
        {
            IQueryable<CustomOrder> customOrdersQueryable = new List<CustomOrder>().BuildMock();
            customOrderRepositoryMock
                .Setup(r => r.GetAllAttached()).Returns(customOrdersQueryable);

            CustomOrderFormInputViewModel? customOrderFormInput = await customOrderService
                .GetCustomOrderForEditAsync(Guid.NewGuid().ToString());
            Assert.IsNull(customOrderFormInput);
        }

        [Test]
        public async Task GetCustomOrderForEditAsyncShouldMapCorrectlyWhenOrderExists()
        {
            CustomOrder customOrder = new CustomOrder
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse("4595a228-bda1-4331-a990-a363b68e6111").ToString(),
                UserName = "Test",
                PhoneNumber = "0896332211",
                Address = "Addr",
                RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(5)),
                Details = "Details"
            };

            IQueryable<CustomOrder> customOrdersQueryable = new List<CustomOrder> { customOrder }.BuildMock();
            customOrderRepositoryMock
                .Setup(r => r.GetAllAttached()).Returns(customOrdersQueryable);

            CustomOrderFormInputViewModel? customOrderFormInput = await customOrderService
                .GetCustomOrderForEditAsync(customOrder.Id.ToString());

            Assert.IsNotNull(customOrderFormInput);
            Assert.That(customOrderFormInput.UserName, Is.EqualTo(customOrder.UserName));
        }

        [Test]
        public async Task GetCustomOrderForEditAsyncShouldFormatDateCorrectly()
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7));
            CustomOrder customOrder = new CustomOrder
            {
                Id = Guid.NewGuid(),
                RequestedDate = date
            };

            IQueryable<CustomOrder> customOrdersQueryable = new List<CustomOrder> { customOrder }.BuildMock();
            this.customOrderRepositoryMock
                .Setup(r => r.GetAllAttached()).Returns(customOrdersQueryable);

            CustomOrderFormInputViewModel? customOrderFormInput = await customOrderService
                .GetCustomOrderForEditAsync(customOrder.Id.ToString());

            Assert.That(customOrderFormInput.RequestedDate, Is.EqualTo(date.ToString(AppDateFormat)));
        }

        // ----------------------------
        // UpdateCustomOrderAsync
        // ----------------------------
        [Test]
        public async Task UpdateCustomOrderAsyncShouldReturnFalseWhenIdIsInvalid()
        {
            bool customOrderFormInput = await customOrderService
                .UpdateCustomOrderAsync("invalid-guid", new CustomOrderFormInputViewModel());
            Assert.IsFalse(customOrderFormInput);
        }

        [Test]
        public async Task UpdateCustomOrderAsyncShouldReturnFalseWhenOrderNotFound()
        {
            IQueryable<CustomOrder> customOrdersQueryable = new List<CustomOrder>().BuildMock();
            customOrderRepositoryMock.Setup(r => r.GetAllAttached()).Returns(customOrdersQueryable);

            bool isCustomOrderUpdated = await customOrderService
                .UpdateCustomOrderAsync(Guid.NewGuid().ToString(), new CustomOrderFormInputViewModel());
            Assert.IsFalse(isCustomOrderUpdated);
        }

        [Test]
        public async Task UpdateCustomOrderAsyncShouldReturnFalseWhenRequestedDateTooClose()
        {
            CustomOrder customOrder = new CustomOrder
            {
                Id = Guid.NewGuid(),
                RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2))
            };

            IQueryable<CustomOrder> orders = new List<CustomOrder> { customOrder }.BuildMock();
            this.customOrderRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(orders);

            bool isCustomOrderUpdated = await customOrderService
                .UpdateCustomOrderAsync(customOrder.Id.ToString(), new CustomOrderFormInputViewModel());
            Assert.IsFalse(isCustomOrderUpdated);
        }

        [Test]
        public async Task UpdateCustomOrderAsyncShouldReturnTrueWhenUpdatedSuccessfully()
        {
            string customOrderId = Guid.NewGuid().ToString();
            CustomOrder existingOrder = new CustomOrder
            {
                Id = Guid.Parse(customOrderId),
                UserName = "Old Name",
                PhoneNumber = "000",
                Address = "Old Address",
                RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)),
                Details = "Old details"
            };

            CustomOrderFormInputViewModel updateModel = new CustomOrderFormInputViewModel
            {
                UserName = "New Name",
                PhoneNumber = "123",
                Address = "New Address",
                RequestedDate = DateTime.UtcNow.AddDays(15).ToString("yyyy-MM-dd"),
                Details = "New details"
            };

            // Setup repository mocks
            IQueryable<CustomOrder> customOrdersQueryable = new List<CustomOrder> { existingOrder }.BuildMock();
            this.customOrderRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(customOrdersQueryable);

            this.customOrderRepositoryMock
                .Setup(r => r.UpdateAsync(It.IsAny<CustomOrder>()))
                .ReturnsAsync(true);
            this.customOrderRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var isCustomOrderUpdated = await customOrderService
                .UpdateCustomOrderAsync(customOrderId, updateModel);

            // Assert
            Assert.IsTrue(isCustomOrderUpdated);
            this.customOrderRepositoryMock.Verify(r => r.UpdateAsync(It.Is<CustomOrder>(
                o => o.UserName == updateModel.UserName &&
                     o.Address == updateModel.Address &&
                     o.Details == updateModel.Details
            )), Times.Once);
            this.customOrderRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        // ----------------------------
        // GetCustomOrderDetailsAsync
        // ----------------------------
        [Test]
        public async Task GetCustomOrderDetailsAsyncShouldReturnNullWhenIdIsInvalid()
        {
            CustomOrderDetailsViewModel? customOrderDetailsView = await customOrderService
                .GetCustomOrderDetailsAsync("bad-guid");
            Assert.IsNull(customOrderDetailsView);
        }

        [Test]
        public async Task GetCustomOrderDetailsAsyncShouldReturnNullWhenOrderNotFound()
        {
            IQueryable<CustomOrder> customOrdersQueryable = new List<CustomOrder>().BuildMock();
            this.customOrderRepositoryMock
                .Setup(r => r.GetAllAttached()).Returns(customOrdersQueryable);

            CustomOrderDetailsViewModel? customOrderDetailsView = await customOrderService
                .GetCustomOrderDetailsAsync(Guid.NewGuid().ToString());
            Assert.IsNull(customOrderDetailsView);
        }

        [Test]
        public async Task GetCustomOrderDetailsAsyncShouldMapCorrectlyWhenOrderFound()
        {
            CustomOrder customOrder = new CustomOrder
            {
                Id = Guid.NewGuid(),
                UserName = "User",
                PhoneNumber = "0896332211",
                Address = "Addr",
                RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(5)),
                Details = "Some details"
            };

            IQueryable<CustomOrder> orders = new List<CustomOrder> { customOrder }.BuildMock();
            customOrderRepositoryMock.Setup(r => r.GetAllAttached()).Returns(orders);

            CustomOrderDetailsViewModel? customOrderDetailsView = await customOrderService
                .GetCustomOrderDetailsAsync(customOrder.Id.ToString());

            Assert.IsNotNull(customOrderDetailsView);
            Assert.That(customOrderDetailsView.UserName, Is.EqualTo(customOrder.UserName));
            Assert.That(customOrderDetailsView.Details, Is.EqualTo(customOrder.Details));
        }

        [Test]
        public async Task GetCustomOrderDetailsAsyncShouldReturnRequestedDateAccurately()
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7));
            CustomOrder customOrders = new CustomOrder
            {
                Id = Guid.NewGuid(),
                RequestedDate = date
            };

            IQueryable<CustomOrder> customOrdersQueryable = new List<CustomOrder> { customOrders }.BuildMock();
            this.customOrderRepositoryMock
                .Setup(r => r.GetAllAttached()).Returns(customOrdersQueryable);

            CustomOrderDetailsViewModel? customOrderDetailsView = await customOrderService
                .GetCustomOrderDetailsAsync(customOrders.Id.ToString());

            Assert.That(customOrderDetailsView.RequestedDate, Is.EqualTo(date));
        }

        // ----------------------------
        // DeleteCustomOrderAsync
        // ----------------------------
        [Test]
        public async Task DeleteCustomOrderAsyncShouldReturnFalseWhenIdIsInvalid()
        {
            bool isCustomOrderDeleted = await customOrderService
                .DeleteCustomOrderAsync("not-a-guid");
            Assert.IsFalse(isCustomOrderDeleted);
        }

        [Test]
        public async Task DeleteCustomOrderAsyncShouldReturnFalseWhenOrderNotFound()
        {
            IQueryable<CustomOrder> customOrdersQueryable = new List<CustomOrder>().BuildMock();
            this.customOrderRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(customOrdersQueryable);

            bool isCustomOrderDeleted = await customOrderService
                .DeleteCustomOrderAsync(Guid.NewGuid().ToString());
            Assert.IsFalse(isCustomOrderDeleted);
        }

        [Test]
        public async Task DeleteCustomOrderAsyncShouldReturnFalseWhenRequestedDateTooClose()
        {
            CustomOrder order = new CustomOrder
            {
                Id = Guid.NewGuid(),
                RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1))
            };

            IQueryable<CustomOrder> customOrdersQueryable = new List<CustomOrder> { order }.BuildMock();
            this.customOrderRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(customOrdersQueryable);

            bool isCustomOrderDeleted = await customOrderService
                .DeleteCustomOrderAsync(order.Id.ToString());

            Assert.IsFalse(isCustomOrderDeleted);
        }

        [Test]
        public async Task DeleteCustomOrderAsyncShouldReturnTrueWhenDeletedSuccessfully()
        {
            CustomOrder existingOrder = new CustomOrder
            {
                Id = Guid.NewGuid(),
                UserName = "John Doe",
                RequestedDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10))
            };

            IQueryable<CustomOrder> customOrdersQueryable = new List<CustomOrder> { existingOrder }.BuildMock();

            this.customOrderRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(customOrdersQueryable);

            this.customOrderRepositoryMock
                .Setup(r => r.HardDeleteAsync(It.IsAny<CustomOrder>()))
                .ReturnsAsync(true);

            this.customOrderRepositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Now call service method
            bool isCustomOrderDeleted = await customOrderService
                .DeleteCustomOrderAsync(existingOrder.Id.ToString());

            Assert.IsTrue(isCustomOrderDeleted);
        }
    }
}