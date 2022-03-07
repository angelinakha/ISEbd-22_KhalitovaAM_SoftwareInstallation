using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SoftwareInstallationContracts.BindingModels;
using SoftwareInstallationContracts.BusinessLogicsContracts;
using SoftwareInstallationContracts.StoragesContracts;
using SoftwareInstallationContracts.ViewModels;

namespace SoftwareInstallationBusinessLogic.BusinessLogics
{
    public class WarehouseLogic : IWarehouseLogic
    {
        private readonly IWarehouseStorage _warehouseStorage;
        private readonly IComponentStorage _componentStorage;

        public WarehouseLogic(IWarehouseStorage warehouseStorage, IComponentStorage componentStorage)
        {
            _warehouseStorage = warehouseStorage;
            _componentStorage = componentStorage;
        }
        public List<WarehouseViewModel> Read(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return _warehouseStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<WarehouseViewModel> { _warehouseStorage.GetElement(model) };
            }
            return _warehouseStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(WarehouseBindingModel model)
        {
            var element = _warehouseStorage.GetElement(new WarehouseBindingModel
            {
                WarehouseName = model.WarehouseName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Склад с таким названием уже есть");
            }
            if (model.Id.HasValue)
            {
                _warehouseStorage.Update(model);
            }
            else
            {
                _warehouseStorage.Insert(model);
            }
        }
        public void Delete(WarehouseBindingModel model)
        {
            var element = _warehouseStorage.GetElement(new WarehouseBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Склад не найден");
            }
            _warehouseStorage.Delete(model);
        }
        public void Replenishment(WarehouseReplenishmentBindingModel model)
        {
                var warehouse = _warehouseStorage.GetElement(new WarehouseBindingModel
                {
                    Id = model.WarehouseId
                });
                if (warehouse == null)
                {
                    throw new Exception("Склад не найден");
                }
                var component = _componentStorage.GetElement(new ComponentBindingModel
                {
                    Id = model.ComponentId
                });
                if (component == null)
                {
                    throw new Exception("Компонент не найден");
                }
                if (warehouse.WarehouseComponents.ContainsKey(model.ComponentId))
                {
                    warehouse.WarehouseComponents[model.ComponentId] =
                    (component.ComponentName, warehouse.WarehouseComponents[model.ComponentId].Item2 + model.Count);
                }
                else
                {
                    warehouse.WarehouseComponents.Add(component.Id, (component.ComponentName, model.Count));
                }
                _warehouseStorage.Update(new WarehouseBindingModel
                {
                    Id = warehouse.Id,
                    WarehouseName = warehouse.WarehouseName,
                    ResponsiblePerson = warehouse.ResponsiblePerson,
                    DateCreate = warehouse.DateCreate,
                    WarehouseComponents = warehouse.WarehouseComponents
                });
        }
    }
}
