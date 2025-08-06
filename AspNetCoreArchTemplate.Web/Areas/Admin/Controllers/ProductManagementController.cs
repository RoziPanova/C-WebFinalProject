using AspNetCoreArchTemplate.Services.Core;
using AspNetCoreArchTemplate.Services.Core.Admin.Interfaces;
using AspNetCoreArchTemplate.Services.Core.Interfaces;
using AspNetCoreArchTemplate.Web.ViewModels.Admin.ProductManagement;
using AspNetCoreArchTemplate.Web.ViewModels.Admin.Products;
using AspNetCoreArchTemplate.Web.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace AspNetCoreArchTemplate.Web.Areas.Admin.Controllers
{
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
            var categories = await productManagementService
                .GetAllCategoriesAsync();



            return View(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductManagementViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = (await productManagementService.GetAllCategoriesAsync());
                return View(model);
            }

            await productManagementService.AddProductAsync(model);
            TempData["SuccessMessage"] = "Product added successfully!";
            return RedirectToAction("Index");
        }

    }
}
