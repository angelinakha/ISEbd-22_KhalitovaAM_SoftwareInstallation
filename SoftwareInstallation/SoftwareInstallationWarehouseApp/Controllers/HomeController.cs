using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoftwareInstallationWarehouseApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using SoftwareInstallationContracts.BindingModels;
using SoftwareInstallationContracts.ViewModels;
using Microsoft.Extensions.Configuration;

namespace SoftwareInstallationWarehouseApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            if (Program.Enter == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIResponsible.GetRequest<List<WarehouseViewModel>>("api/warehouse/GetWarehouseList"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string password)
        {

            if (!string.IsNullOrEmpty(password))
            {
                if (_configuration["Password"] != password)
                {
                    throw new Exception("Неверный пароль");
                }
                Program.Enter = true;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль");
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (Program.Enter == false)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }

        [HttpPost]
        public void Create(string warehouseName, string responsiblePerson)
        {
            if (!string.IsNullOrEmpty(warehouseName) && !string.IsNullOrEmpty(responsiblePerson))
            {
                APIResponsible.PostRequest("api/warehouse/CreateOrUpdateWarehouse", new WarehouseBindingModel
                {
                    ResponsiblePerson = responsiblePerson,
                    WarehouseName = warehouseName,
                    DateCreate = DateTime.Now,
                    WarehouseComponents = new Dictionary<int, (string, int)>()
                });
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите название и ФИО ответственного лица");
        }

        [HttpGet]
        public IActionResult Update(int warehouseId)
        {
            if (Program.Enter == false)
            {
                return Redirect("~/Home/Enter");
            }
            WarehouseViewModel warehouse = APIResponsible.GetRequest<WarehouseViewModel>($"api/Warehouse/GetWarehouse?warehouseId={warehouseId}");
            ViewBag.Components = warehouse.WarehouseComponents.Values;
            ViewBag.WarehouseName = warehouse.WarehouseName;
            ViewBag.ResponsiblePerson = warehouse.ResponsiblePerson;
            return View();
        }

        [HttpPost]
        public void Update(int warehouseId, string warehouseName, string responsiblePerson)
        {
            if (String.IsNullOrEmpty(warehouseName) || String.IsNullOrEmpty(responsiblePerson))
            {
                return;
            }
            WarehouseViewModel warehouse = APIResponsible.GetRequest<WarehouseViewModel>($"api/Warehouse/GetWarehouse?warehouseId={warehouseId}");
            APIResponsible.PostRequest("api/Warehouse/CreateOrUpdateWarehouse", new WarehouseBindingModel
            {
                Id = warehouseId,
                WarehouseName = warehouseName,
                ResponsiblePerson = responsiblePerson,
                WarehouseComponents = warehouse.WarehouseComponents,
                DateCreate = DateTime.Now
            });
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult Delete()
        {
            if (Program.Enter == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Warehouse = APIResponsible.GetRequest<List<WarehouseViewModel>>("api/warehouse/GetWarehouseList");
            return View();
        }

        [HttpPost]
        public void Delete(int warehouseId)
        {
            var warehouse = APIResponsible.GetRequest<WarehouseViewModel>($"api/Warehouse/GetWarehouse?warehouseId={warehouseId}");
            APIResponsible.PostRequest("api/Warehouse/DeleteWarehouse", warehouse);
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult AddComponent()
        {
            if (Program.Enter == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Warehouse = APIResponsible.GetRequest<List<WarehouseViewModel>>("api/warehouse/GetWarehouseList");
            ViewBag.Component = APIResponsible.GetRequest<List<ComponentViewModel>>("api/warehouse/GetComponents");
            return View();
        }

        [HttpPost]
        public void AddComponent(int warehouseId, int componentId, int count)
        {
            APIResponsible.PostRequest("api/warehouse/AddComponent", new WarehouseReplenishmentBindingModel
            {
                WarehouseId = warehouseId,
                ComponentId = componentId,
                Count = count
            });
            Response.Redirect("Index");
        }
    }
}
