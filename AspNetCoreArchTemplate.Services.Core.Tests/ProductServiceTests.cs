namespace AspNetCoreArchTemplate.Services.Core.Tests
{
    using Core.Interfaces;
    using Data.Models;
    using Data.Repository.Interfaces;
    using MockQueryable;
    using Moq;
    using Web.ViewModels.Products;
    using static GCommon.ApplicationConstants;

    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> productRepositoryMock;
        private IProductService productService;

        [SetUp]
        public void SetUp()
        {
            this.productRepositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            this.productService = new ProductService(this.productRepositoryMock.Object);
        }
        [Test]
        public void PassAlways()
        {
            // Test that will always pass to show that the SetUp is working
            Assert.Pass();
        }

        [Test]
        public async Task GetAllProductsAsyncShouldReturnEmptyCollectionWhenNoProductsExist()
        {

            List<Product> emptyProductList = new List<Product>();
            IQueryable<Product> emptyProductQueryable = emptyProductList
                .BuildMock();

            this.productRepositoryMock
                .Setup(pr => pr.GetAllAttached())
                .Returns(emptyProductQueryable);


            IEnumerable<ProductIndexViewModel>emptyProductIndexVm =
                await productService.GetAllProductsAsync();


            Assert.IsNotNull(emptyProductIndexVm);
            Assert.That(emptyProductIndexVm.Count(), Is.EqualTo(emptyProductList.Count));
        }
        [Test]
        public async Task GetAllProductsAsyncShouldReturnSameCollectionSizeWhenNonEmpty()
        {

            List<Product> productList = new List<Product>()
            {
                new Product()
                {
                    Id = Guid.Parse("3444f227-4170-4e59-8be3-4f0963da5e96"),
                    Name = "Rose bouquet",
                    Description = "Beautiful and romantic",
                    Price = 39.99m,
                    ImageUrl = "https//somesite.com/Roses.jpg",
                    ProductType = "Bouquet",
                    EventType = "Sale",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("5fe8418b-2413-43b0-9559-1a42947aff9a")
                },
                new Product()
                {
                    Id = Guid.Parse("a40fbfb6-cae1-4ae4-b200-ad92f5b3bc04"),
                    Name = "Wedding arrangement",
                    Description = "Elegant and special",
                    Price = 180.89m,
                    ImageUrl = null, // triggers NoImageUrl fallback
                    ProductType = "Arrangement",
                    EventType = null,
                    IsDeleted = false,
                    CategoryId = Guid.Parse("56e14b1f-365b-4fb0-85f2-1cdba2dbd38a")
                }
            };

            IQueryable<Product> productQueryable = productList.BuildMock();

            this.productRepositoryMock
                .Setup(pr => pr.GetAllAttached())
                .Returns(productQueryable);


            IEnumerable<ProductIndexViewModel> productIndexVm =
                await productService.GetAllProductsAsync();


            Assert.IsNotNull(productIndexVm);
            Assert.That(productIndexVm.Count(), Is.EqualTo(productList.Count()));
        }
        [Test]
        public async Task GetAllProductsAsyncShouldReturnSameDataInViewModels()
        {
            // Arrange
            List<Product> productList = new List<Product>()
            {
                new Product()
                {
                    Id = Guid.Parse("3444f227-4170-4e59-8be3-4f0963da5e96"),
                    Name = "Rose bouquet",
                    Description = "Beautiful and romantic",
                    Price = 39.99m,
                    ImageUrl = "https//somesite.com/Roses.jpg",
                    ProductType = "Bouquet",
                    EventType = "Sale",
                    IsDeleted = false,
                    CategoryId = Guid.Parse("5fe8418b-2413-43b0-9559-1a42947aff9a")
                },
                new Product()
                {
                    Id = Guid.Parse("a40fbfb6-cae1-4ae4-b200-ad92f5b3bc04"),
                    Name = "Wedding arrangement",
                    Description = "Elegant and special",
                    Price = 180.89m,
                    ImageUrl = null, // triggers NoImageUrl fallback
                    ProductType = "Arrangement",
                    EventType = null,
                    IsDeleted = false,
                    CategoryId = Guid.Parse("56e14b1f-365b-4fb0-85f2-1cdba2dbd38a")
                }
            };

            IQueryable<Product> productQueryable = productList.BuildMock();

            this.productRepositoryMock
                .Setup(pr => pr.GetAllAttached())
                .Returns(productQueryable);


            IEnumerable<ProductIndexViewModel> productIndexVm =
                await productService.GetAllProductsAsync();


            Assert.IsNotNull(productIndexVm);
            Assert.That(productIndexVm.Count(), Is.EqualTo(productList.Count));

            foreach (Product product in productList)
            {
                ProductIndexViewModel? productVm = productIndexVm
                    .FirstOrDefault(vm => vm.Id.ToLower() == product.Id.ToString().ToLower());

                Assert.IsNotNull(productVm);
                Assert.That(productVm!.Name, Is.EqualTo(product.Name), "Product name does not match between repository and ViewModel!");
                Assert.That(productVm.ProductType, Is.EqualTo(product.ProductType));

                // Special check for ImageUrl fallback
                if (product.ImageUrl == null)
                {
                    Assert.That(productVm.ImageUrl, Does.Contain(NoImageUrl));
                }
                else
                {
                    Assert.That(productVm.ImageUrl, Is.EqualTo(product.ImageUrl));
                }
            }
        }
        [Test]
        public async Task GetAllProductsAsyncShouldMapImageUrlWhenProvided()
        {

            Product product = new Product
            {
                Id = Guid.Parse("b2fddca8-39ff-4645-b4d7-5ba565d8a4c8"),
                Name = "Test Product",
                Description = "Description",
                Price = 50.00m,
                ImageUrl = "custom.jpg",
                ProductType = "TestType",
                EventType = null,
                IsDeleted = false,
                CategoryId = null
            };

            IQueryable<Product> productQueryable = new List<Product> { product }.BuildMock();

            this.productRepositoryMock
                 .Setup(r => r.GetAllAttached())
                 .Returns(productQueryable);


            IEnumerable<ProductIndexViewModel> productIndexViewModels = await productService
                .GetAllProductsAsync();
            ProductIndexViewModel? productIndexVm = productIndexViewModels.FirstOrDefault();


            Assert.IsNotNull(productIndexVm);
            Assert.That(productIndexVm!.ImageUrl, Is.EqualTo("custom.jpg"));
        }

        [Test]
        public async Task GetAllProductsAsyncShouldApplyNoImageUrlFallbackWhenImageUrlIsNull()
        {

            Product product = new Product
            {
                Id = Guid.Parse("b2fddca8-39ff-4645-b4d7-5ba565d8a4c8"),
                Name = "Test Product",
                Description = "Description",
                Price = 50.00m,
                ImageUrl = null,
                ProductType = "TestType",
                EventType = null,
                IsDeleted = false,
                CategoryId = null
            };

            IQueryable<Product> productQueryable = new List<Product> { product }.BuildMock();

            this.productRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(productQueryable);


            IEnumerable<ProductIndexViewModel> productIndexViewModels = await productService.GetAllProductsAsync();
            ProductIndexViewModel? productIndexVm = productIndexViewModels.FirstOrDefault();


            Assert.IsNotNull(productIndexVm);
            Assert.That(productIndexVm!.ImageUrl, Does.Contain(NoImageUrl));
        }
        [Test]
        public async Task GetProductDetailsByIdAsyncShouldReturnNullWhenIdIsInvalidGuid()
        {

            string invalidId = "not-a-guid";


            ProductDetailsViewModel productDetailsVm = await productService
                .GetProductDetailsByIdAsync(invalidId);


            Assert.IsNull(productDetailsVm);
            this.productRepositoryMock
                .Verify(r => r.GetAllAttached(), Times.Never);
        }

        [Test]
        public async Task GetProductDetailsByIdAsyncShouldReturnNullWhenProductNotFound()
        {

            Guid validId = Guid.Parse("e0c83d43-14bc-4d98-a5ef-e3d1b8fb16b9");
            IQueryable<Product> emptyList = new List<Product>().BuildMock();

            this.productRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(emptyList);

            ProductDetailsViewModel productDetailsVm = await productService
                .GetProductDetailsByIdAsync(validId.ToString());

            Assert.IsNull(productDetailsVm);
        }

        [Test]
        public async Task GetProductDetailsByIdAsyncShouldReturnMappedViewModelWhenProductExists()
        {

            Guid validId = Guid.Parse("e0c83d43-14bc-4d98-a5ef-e3d1b8fb16b9");

            Product product = new Product
            {
                Id = validId,
                Name = "Test Product",
                Description = "Description",
                Price = 50.00m,
                ImageUrl = "custom.jpg",
                ProductType = "TestType",
                EventType = "TestEvenType",
                IsDeleted = false,
                CategoryId = null
            };

            IQueryable<Product> productQuaryable = new List<Product> { product }.BuildMock();

            productRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(productQuaryable);


            ProductDetailsViewModel productDetailsVm = await productService.GetProductDetailsByIdAsync(validId.ToString());

            // Assert
            Assert.IsNotNull(productDetailsVm);
            Assert.That(productDetailsVm.Id, Is.EqualTo(product.Id.ToString()));
            Assert.That(productDetailsVm.Name, Is.EqualTo(product.Name));
            Assert.That(productDetailsVm.Price, Is.EqualTo(product.Price));
            Assert.That(productDetailsVm.Description, Is.EqualTo(product.Description));
            Assert.That(productDetailsVm.EventType, Is.EqualTo(product.EventType));
            Assert.That(productDetailsVm.ImageUrl, Is.EqualTo(product.ImageUrl));
            Assert.That(productDetailsVm.ProductType, Is.EqualTo(product.ProductType));
        }

        [Test]
        public async Task GetProductDetailsByIdAsyncShouldApplyNoImageUrlFallbackWhenImageUrlIsNull()
        {
            // Arrange
            Guid validId = Guid.NewGuid();

            Product product = new Product
            {
                Id = validId,
                Name = "Test Product",
                Description = "Description",
                Price = 50.00m,
                ImageUrl = null,
                ProductType = "TestType",
                EventType = "TestEvenType",
                IsDeleted = false,
                CategoryId = null
            };

            IQueryable<Product> productQuaryable = new List<Product> { product }.BuildMock();

            this.productRepositoryMock
                .Setup(r => r.GetAllAttached())
                .Returns(productQuaryable);

            // Act
            ProductDetailsViewModel productDetailsVm = await productService
                .GetProductDetailsByIdAsync(validId.ToString());

            // Assert
            Assert.IsNotNull(productDetailsVm);
            Assert.That(productDetailsVm.ImageUrl, Does.Contain(NoImageUrl));
        }
    }
}


