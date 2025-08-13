namespace AspNetCoreArchTemplate.Web.Areas.Admin.Controllers
{
    using AspNetCoreArchTemplate.Services.Core.Admin.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Admin.ProductManagement;
    using AspNetCoreArchTemplate.Web.ViewModels.Admin.Products;
    using Microsoft.AspNetCore.Mvc;

    [AutoValidateAntiforgeryToken]
    public class ProductManagementController : BaseAdminController
    {
        private readonly IProductManagementService productManagementService;
        public ProductManagementController(IProductManagementService productManagementService)
        {
            this.productManagementService = productManagementService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductManagementIndexViewModel> allProducts = await this.productManagementService
                .GetAllProductsAsync();

            return this.View(allProducts);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string? productId)
        {
            if (string.IsNullOrWhiteSpace(productId))
            {
                return NotFound();
            }

            try
            {
                ProductManagementFormInputModel? product = await
                    productManagementService
                    .GetProductByIdAsync(productId);
                if (product == null)
                {
                    return NotFound();
                }

                // Populate dropdown categories
                IEnumerable<CategoryDropDownViewModel> categories = await productManagementService.GetAllCategoriesAsync();
                product.Categories = categories;

                return this.View(product);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductManagementFormInputModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Re-populate categories if validation fails
                    model.Categories = await productManagementService.GetAllCategoriesAsync();
                    return this.View(model);
                }

                bool isEdited = await productManagementService.UpdateAsync(model);
                if (!isEdited)
                {
                    return NotFound();
                }

                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return this.RedirectToAction(nameof(Index), "Home");
            }


        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                AddProductManagementViewModel model = new AddProductManagementViewModel
                {
                    Categories = await productManagementService.GetAllCategoriesAsync()
                };

                return this.View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return this.RedirectToAction(nameof(Index), "Home");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddProductManagementViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Categories = await productManagementService.GetAllCategoriesAsync();
                    return this.View(model);
                }

                bool isAdded = await productManagementService.AddProductAsync(model);
                if (!isAdded)
                {
                    ModelState.AddModelError("", "Failed to add product.");
                    model.Categories = await productManagementService.GetAllCategoriesAsync();
                    return this.View(model);
                }

                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return this.RedirectToAction(nameof(Index), "Home");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest();
                }

                bool isDeleted = await productManagementService.DeleteAsync(id);

                if (!isDeleted)
                {
                    return NotFound();
                }

                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return this.RedirectToAction(nameof(Index), "Home");
            }

        }


    }
}
