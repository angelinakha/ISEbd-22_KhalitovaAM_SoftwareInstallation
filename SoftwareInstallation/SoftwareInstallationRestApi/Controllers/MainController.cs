using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftwareInstallationContracts.BindingModels;
using SoftwareInstallationContracts.BusinessLogicsContracts;
using SoftwareInstallationContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareInstallationRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IPackageLogic _package;
        public MainController(IOrderLogic order, IPackageLogic package)
        {
            _order = order;
            _package = package;
        }
        [HttpGet]
        public List<PackageViewModel> GetProductList() => _package.Read(null)?.ToList();
        [HttpGet]
        public PackageViewModel GetProduct(int packageId) => _package.Read(new PackageBindingModel
        { Id = packageId })?[0];
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel
        { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) =>
       _order.CreateOrder(model);
    }
}
