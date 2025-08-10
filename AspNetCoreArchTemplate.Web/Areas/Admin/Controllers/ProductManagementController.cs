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

            return View(allProducts);
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
                var product = await
                    productManagementService
                    .GetByIdAsync(productId);
                if (product == null)
                {
                    return NotFound();
                }

                // Populate dropdown categories
                var categories = await productManagementService.GetAllCategoriesAsync();
                product.Categories = categories;

                return View(product);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(ProductManagementFormInputModel model)
        {

            if (!ModelState.IsValid)
            {
                // Re-populate categories if validation fails
                model.Categories = await productManagementService.GetAllCategoriesAsync();
                return View(model);
            }

            bool success = await productManagementService.UpdateAsync(model);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddProductManagementViewModel
            {
                Categories = await productManagementService.GetAllCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(AddProductManagementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await productManagementService.GetAllCategoriesAsync();
                return View(model);
            }

            var success = await productManagementService.AddProductAsync(model);
            if (!success)
            {
                ModelState.AddModelError("", "Failed to add product.");
                model.Categories = await productManagementService.GetAllCategoriesAsync();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            bool deleted = await productManagementService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
