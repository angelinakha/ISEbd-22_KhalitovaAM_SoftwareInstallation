using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using SoftwareInstallationContracts.BindingModels;
using SoftwareInstallationContracts.BusinessLogicsContracts;
using SoftwareInstallationContracts.ViewModels;

namespace SoftwareInstallationRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseLogic _warehouse;
        private readonly IComponentLogic _component;

        public WarehouseController(IWarehouseLogic warehouse, IComponentLogic component)
        {
            _warehouse = warehouse;
            _component = component;
        }

        [HttpGet]
        public List<WarehouseViewModel> GetWarehouseList() => _warehouse.Read(null)?.ToList();

        [HttpGet]
        public WarehouseViewModel GetWarehouse(int warehouseId) => _warehouse.Read(new WarehouseBindingModel { Id = warehouseId })?[0];

        [HttpGet]
        public List<ComponentViewModel> GetComponents() => _component.Read(null);

        [HttpPost]
        public void CreateOrUpdateWarehouse(WarehouseBindingModel model) => _warehouse.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteWarehouse(WarehouseBindingModel model) => _warehouse.Delete(model);

        [HttpPost]
        public void AddComponent(WarehouseReplenishmentBindingModel model) => _warehouse.Replenishment(model);
    }
}
