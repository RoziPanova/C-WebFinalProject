namespace AspNetCoreArchTemplate.Services.Core.Tests
{
    using Data.Models;
    using Data.Repository.Interfaces;
    using MockQueryable;
    using Moq;
    using NUnit.Framework;
    using Services.Core.Interfaces;
    using Web.ViewModels.Cart;

    namespace AspNetCoreArchTemplate.Tests.Services
    {
        [TestFixture]
        public class CartItemsServiceTests
        {
            private Mock<ICartItemsRepository> cartItemsRepositoryMock;
            private Mock<ICartRepository> cartRepositoryMock;
            private ICartItemsService cartItemsService;

            [SetUp]
            public void Setup()
            {
                cartItemsRepositoryMock = new Mock<ICartItemsRepository>(MockBehavior.Strict);
                cartRepositoryMock = new Mock<ICartRepository>(MockBehavior.Strict);
                cartItemsService = new CartItemsService(cartItemsRepositoryMock.Object, cartRepositoryMock.Object);
            }

            [Test]
            public void PassAlways()
            {
                // Test that will always pass to show that the SetUp is working
                Assert.Pass();
            }
            [Test]
            public async Task GetAllCartItemsAsyncShouldReturnCartItemsWhenUserIdIsValid()
            {
                // Arrange
                string userId = Guid.NewGuid().ToString();

                Product product1 = new Product { Id = Guid.NewGuid(), Name = "Product 1", Price = 100 };
                Product product2 = new Product { Id = Guid.NewGuid(), Name = "Product 2", Price = 150 };

                Cart cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Items = new List<CartItem>()
                };

                List<CartItem> cartItems = new List<CartItem>
                {
                    new CartItem { Cart = cart, Product = product1, Quantity = 2, ProductId = product1.Id },
                    new CartItem { Cart = cart, Product = product2, Quantity = 1, ProductId = product2.Id }
                };

                // Setup repository mock with BuildMock()
                this.cartItemsRepositoryMock
                    .Setup(r => r.GetAllAttached())
                    .Returns(cartItems.BuildMock());

                // Act
                IEnumerable<CartIndexViewModel> CartVm = await cartItemsService
                    .GetAllCartItemsAsync(userId);

                // Assert
                Assert.That(CartVm.Count(), Is.EqualTo(2));
                Assert.That(CartVm.First().ProductName, Is.EqualTo("Product 1"));
                Assert.That(CartVm.First().Total, Is.EqualTo(200));
                Assert.That(CartVm.Last().ProductName, Is.EqualTo("Product 2"));
                Assert.That(CartVm.Last().Total, Is.EqualTo(150));
            }

            [Test]
            public async Task GetAllCartItemsAsyncShouldReturnEmptyWhenNoItemsInCart()
            {
                // Arrange
                string userId = Guid.NewGuid().ToString();
                IQueryable<CartItem> cartItems = new List<CartItem>().BuildMock();
                this.cartItemsRepositoryMock
                    .Setup(repo => repo.GetAllAttached())
                    .Returns(cartItems);

                // Act
                IEnumerable<CartIndexViewModel> CartVm = await cartItemsService
                    .GetAllCartItemsAsync(userId);

                // Assert
                Assert.IsEmpty(CartVm);
            }
            [Test]
            public async Task GetAllCartItemsAsyncShouldReturnEmptyWhenUserIdIsNull()
            {
                // Arrange
                string userId = null;

                // Act
                IQueryable<CartItem> emptyCartItems = new List<CartItem>().BuildMock();

                // Setup the repository mock
                this.cartItemsRepositoryMock
                    .Setup(r => r.GetAllAttached())
                    .Returns(emptyCartItems);

                // Act
                IEnumerable<CartIndexViewModel> cartIndexViews = await cartItemsService.GetAllCartItemsAsync(userId);

                // Assert
                Assert.IsNotNull(cartIndexViews);
                Assert.IsEmpty(cartIndexViews);
            }
            [Test]
            public async Task GetAllCartItemsAsyncShouldReturnEmptyWhenUserIdIsEmpty()
            {
                // Arrange
                string userId = string.Empty;

                IQueryable<CartItem> emptyCartItems = new List<CartItem>().BuildMock();

                // Setup the mock
                this.cartItemsRepositoryMock
                    .Setup(r => r.GetAllAttached())
                    .Returns(emptyCartItems);

                // Act
                IEnumerable<CartIndexViewModel> cartIndexViews = await cartItemsService.GetAllCartItemsAsync(userId);

                // Assert
                Assert.IsNotNull(cartIndexViews);
                Assert.IsEmpty(cartIndexViews);
            }

            [Test]
            public async Task AddProductToCartAsyncShouldAddProductWhenProductIsValid()
            {
                // Arrange
                string productId = Guid.NewGuid().ToString();
                string userId = Guid.NewGuid().ToString();
                Cart cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>()
                };
                this.cartRepositoryMock
                    .Setup(repo => repo.GetAllAttached())
                    .Returns(new List<Cart> { cart }.BuildMock());
                this.cartRepositoryMock
                    .Setup(r => r.SaveChangesAsync())
                    .Returns(Task.CompletedTask); // or ReturnsAsync(1) if your method returns int


                // Act
                bool isProductAdded = await cartItemsService
                    .AddProductToCartAsync(productId, userId);

                // Assert
                Assert.IsTrue(isProductAdded);
                Assert.That(cart.Items.Count, Is.EqualTo(1));
                Assert.That(cart.Items.First().Quantity, Is.EqualTo(1));
            }
            [Test]
            public async Task AddProductToCartAsyncShouldNotAddProductWhenProductIdIsInvalid()
            {
                // Arrange
                string productId = "invalid-guid";
                string userId = Guid.NewGuid().ToString();

                // Act
                bool isProductAdded = await cartItemsService
                    .AddProductToCartAsync(productId, userId);

                // Assert
                Assert.IsFalse(isProductAdded);
            }
            [Test]
            public async Task AddProductToCartAsyncShouldNotAddProductWhenUserIdIsNull()
            {
                // Arrange
                string productId = Guid.NewGuid().ToString();
                string userId = null;

                // Act
                bool isProductAdded = await cartItemsService
                    .AddProductToCartAsync(productId, userId);

                // Assert
                Assert.IsFalse(isProductAdded);
            }
            [Test]
            public async Task AddProductToCartAsyncShouldNotAddProductWhenUserIdIsEmpty()
            {
                // Arrange
                string productId = Guid.NewGuid().ToString();
                string userId = string.Empty;

                // Act
                cartRepositoryMock
                    .Setup(r => r.GetAllAttached())
                    .Returns(new List<Cart>().BuildMock());

                cartRepositoryMock
                    .Setup(r => r.AddAsync(It.IsAny<Cart>()))
                    .Returns(Task.CompletedTask);

                cartRepositoryMock
                    .Setup(r => r.SaveChangesAsync())
                    .Returns(Task.CompletedTask);

                // Act
                bool isProductAdded = await cartItemsService
                    .AddProductToCartAsync(productId, userId);

                // Assert
                Assert.IsFalse(isProductAdded);
            }
            [Test]
            public async Task RemoveProductFromCartAsyncShouldRemoveProductWhenProductExists()
            {
                // Arrange
                string productId = Guid.NewGuid().ToString();
                string userId = Guid.NewGuid().ToString();
                CartItem cartItem = new CartItem
                {
                    ProductId = Guid.Parse(productId),
                    Quantity = 1
                };
                Cart cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>
                    {
                        cartItem
                    }
                };
                IQueryable<Cart> cartList = new List<Cart> { cart }.BuildMock();

                // Setup GetAllAttached() to return the mocked IQueryable
                cartRepositoryMock
                    .Setup(r => r.GetAllAttached())
                    .Returns(cartList);

                // Setup the item repository methods
                cartItemsRepositoryMock
                    .Setup(r => r.HardDeleteAsync(It.IsAny<CartItem>()))
                    .ReturnsAsync(true);

                cartItemsRepositoryMock
                    .Setup(r => r.SaveChangesAsync())
                    .Returns(Task.CompletedTask);

                // Act
                bool isProductRemoved = await cartItemsService
                    .RemoveProductFromCartAsync(productId, userId);

                // Assert
                Assert.IsTrue(isProductRemoved);
                Assert.IsEmpty(cart.Items);
            }

            [Test]
            public async Task RemoveProductFromCartAsyncShouldNotRemoveProductWhenProductDoesNotExist()
            {
                // Arrange
                string productId = Guid.NewGuid().ToString();
                string userId = Guid.NewGuid().ToString();
                Cart cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>()
                };
                this.cartRepositoryMock
                    .Setup(repo => repo.GetAllAttached())
                    .Returns(new List<Cart> { cart }
                    .BuildMock());

                // Act
                bool isProductRemoved = await cartItemsService
                    .RemoveProductFromCartAsync(productId, userId);

                // Assert
                Assert.IsFalse(isProductRemoved);
            }

            [Test]
            public async Task RemoveProductFromCartAsyncShouldNotRemoveProductWhenUserIdIsNull()
            {
                // Arrange
                string productId = Guid.NewGuid().ToString();
                string userId = null;

                // Act
                bool isProductRemoved = await cartItemsService
                    .RemoveProductFromCartAsync(productId, userId);

                // Assert
                Assert.IsFalse(isProductRemoved);
            }
            [Test]
            public async Task RemoveProductFromCartAsyncShouldNotRemoveProductWhenUserIdIsEmpty()
            {
                // Arrange
                string productId = Guid.NewGuid().ToString();
                string userId = string.Empty;

                // Act
                bool isProductRemoved = await cartItemsService
                    .RemoveProductFromCartAsync(productId, userId);

                // Assert
                Assert.IsFalse(isProductRemoved);
            }
            [Test]
            public async Task CheckoutAsyncShouldCheckoutCartWhenCartExists()
            {
                // Arrange
                string userId = Guid.NewGuid().ToString();
                Cart cart =
                    new Cart
                    {
                        UserId = userId,
                        IsCheckedOut = false,
                        Items = new List<CartItem>()
                    };
                this.cartRepositoryMock
                    .Setup(repo => repo.GetAllAttached())
                    .Returns(new List<Cart> { cart }
                    .BuildMock());

                // Act
                cartRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(new List<Cart> { cart }.BuildMock());

                cartRepositoryMock
                    .Setup(r => r.SaveChangesAsync())
                    .Returns(Task.CompletedTask);

                // Act
                bool isCartCheckedOut = await cartItemsService
                    .CheckoutAsync(userId);

                // Assert
                Assert.IsTrue(isCartCheckedOut);
                Assert.IsTrue(cart.IsCheckedOut);
            }

            [Test]
            public async Task CheckoutAsyncShouldNotCheckoutWhenCartIsAlreadyCheckedOut()
            {
                // Arrange
                string userId = Guid.NewGuid().ToString();
                Cart cart = new Cart
                {
                    UserId = userId
                    ,
                    IsCheckedOut = true,
                    Items = new List<CartItem>()
                };
                this.cartRepositoryMock
                    .Setup(repo => repo.GetAllAttached())
                    .Returns(new List<Cart> { cart }
                    .BuildMock());

                // Act
                bool isCartCheckedOut = await cartItemsService
                    .CheckoutAsync(userId);

                // Assert
                Assert.IsFalse(isCartCheckedOut);
            }
            [Test]
            public async Task CheckoutAsyncShouldNotCheckoutWhenUserIdIsNull()
            {
                // Arrange
                string userId = null;

                // Act
                bool isCartCheckedOut = await cartItemsService
                    .CheckoutAsync(userId);

                // Assert
                Assert.IsFalse(isCartCheckedOut);
            }
            [Test]
            public async Task CheckoutAsyncShouldNotCheckoutWhenUserIdIsEmpty()
            {
                // Arrange
                string userId = string.Empty;

                // Act
                bool isCartCheckedOut = await cartItemsService
                    .CheckoutAsync(userId);

                // Assert
                Assert.IsFalse(isCartCheckedOut);
            }

        }
    }
}
