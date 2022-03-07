using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareInstallationContracts.BindingModels;
using SoftwareInstallationContracts.StoragesContracts;
using SoftwareInstallationContracts.ViewModels;
using SoftwareInstallationFileImplement.Models;

namespace SoftwareInstallationFileImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        private readonly FileDataListSingleton source;
        public WarehouseStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<WarehouseViewModel> GetFullList()
        {
            return source.Warehouses
            .Select(CreateModel)
            .ToList();
        }
        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Warehouses
            .Where(rec => rec.WarehouseName.Contains(model.WarehouseName))
            .Select(CreateModel)
            .ToList();
        }
        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var warehouse = source.Warehouses
            .FirstOrDefault(rec => rec.WarehouseName == model.WarehouseName || rec.Id
           == model.Id);
            return warehouse != null ? CreateModel(warehouse) : null;
        }
        public void Insert(WarehouseBindingModel model)
        {
            int maxId = source.Warehouses.Count > 0 ? source.Warehouses.Max(rec => rec.Id) : 0;
            var element = new Warehouse
            {
                Id = maxId + 1,
                WarehouseComponents = new Dictionary<int, int>(),
                DateCreate = DateTime.Now
            };
            source.Warehouses.Add(CreateModel(model, element));
        }
        public void Update(WarehouseBindingModel model)
        {
            var element = source.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }
        public void Delete(WarehouseBindingModel model)
        {
            Warehouse element = source.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Warehouses.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.ResponsiblePerson = model.ResponsiblePerson;
            warehouse.DateCreate = model.DateCreate;
            // удаляем убранные
            foreach (var key in warehouse.WarehouseComponents.Keys.ToList())
            {
                if (!model.WarehouseComponents.ContainsKey(key))
                {
                    warehouse.WarehouseComponents.Remove(key);
                }
            }
            // обновляем существуюущие и добавляем новые
            foreach (var component in model.WarehouseComponents)
            {
                if (warehouse.WarehouseComponents.ContainsKey(component.Key))
                {
                    warehouse.WarehouseComponents[component.Key] =
                   model.WarehouseComponents[component.Key].Item2;
                }
                else
                {
                    warehouse.WarehouseComponents.Add(component.Key,
                   model.WarehouseComponents[component.Key].Item2);
                }
            }
            return warehouse;
        }
        private WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            return new WarehouseViewModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                ResponsiblePerson = warehouse.ResponsiblePerson,
                DateCreate = warehouse.DateCreate,
                WarehouseComponents = warehouse.WarehouseComponents
                            .ToDictionary(recPC => recPC.Key, recPC =>
                        (source.Components.FirstOrDefault(recC => recC.Id ==
                 recPC.Key)?.ComponentName, recPC.Value))
            };
        }
        public bool WriteOffFromWarehouses(Dictionary<int, (string, int)> components, int writeOffCount)
        {
            foreach (var component in components)
            {
                int count = source.Warehouses.Where(comp => comp.WarehouseComponents.ContainsKey(component.Key)).Sum(comp => comp.WarehouseComponents[component.Key]);

                if (count < component.Value.Item2 * writeOffCount)
                {
                    return false;
                }
            }
            foreach (var component in components)
            {
                int count = component.Value.Item2 * writeOffCount;
                IEnumerable<Warehouse> warehouses = source.Warehouses.Where(comp => comp.WarehouseComponents.ContainsKey(component.Key));

                foreach (Warehouse warehouse in warehouses)
                {
                    if (warehouse.WarehouseComponents[component.Key] <= count)
                    {
                        count -= warehouse.WarehouseComponents[component.Key];
                        warehouse.WarehouseComponents.Remove(component.Key);
                    }
                    else
                    {
                        warehouse.WarehouseComponents[component.Key] -= count;
                        count = 0;
                    }
                    if (count == 0)
                    {
                        break;
                    }
                }
            }
            return true;
        }
    }
}
