using DiscountManagement.Application.Contract.ColleagueDiscount;
using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        [TempData] public string Mesagee { get; set; }
        public List<InventoryViewModel> inventoryViewModels;
        public InventorySearchModel SearchModel;

        public SelectList Product;
        private readonly IInventoryApplication _inventoryApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(IInventoryApplication inventoryApplication, IProductApplication productApplication)
        {
            _inventoryApplication = inventoryApplication;
            _productApplication = productApplication;
        }

        public void OnGet(InventorySearchModel searchModel)
        {
            Product = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            inventoryViewModels = _inventoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory()
            {
                Product = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);

        }

        public IActionResult OnGetEdit(int Id)
        {
            var customerDiscount = _inventoryApplication.GetDetail(Id);
            customerDiscount.Product = _productApplication.GetProducts();
            return Partial("./Edit", customerDiscount);
        }

        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _inventoryApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetInCrease(int Id)
        {
            var command = new InCreaseInventory()
            {
                InventoryId = Id
            };
            return Partial("Increase", command);
        }

        public JsonResult OnPostInCrease(InCreaseInventory command)
        {
            var result = _inventoryApplication.InCrease(command);
            //return new JsonResult(result);
            return new JsonResult(result);
        }
        public IActionResult OnGetReduce(int Id)
        {
            var command = new ReduceInventory()
            {
                InventoryId = Id
            };
            return Partial("Reduce", command);
        }

        public JsonResult OnPostReduce(ReduceInventory command)
        {
            var result = _inventoryApplication.Reduce(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetLogs(int Id)
        {
            var log = _inventoryApplication.GetOperationLog(Id);
            return Partial("OperationLog", log);
        }
    }
}
