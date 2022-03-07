using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareInstallationContracts.BindingModels;
using SoftwareInstallationContracts.StoragesContracts;
using SoftwareInstallationContracts.ViewModels;
using SoftwareInstallationDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace SoftwareInstallationDatabaseImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        public List<WarehouseViewModel> GetFullList()
        {
            using var context = new SoftwareInstallationDatabase();
            return context.Warehouses
                .Include(rec => rec.WarehouseComponents)
                .ThenInclude(rec => rec.Component)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }
        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new SoftwareInstallationDatabase();
            return context.Warehouses
            .Include(rec => rec.WarehouseComponents)
            .ThenInclude(rec => rec.Component)
            .Where(rec => rec.WarehouseName.Contains(model.WarehouseName))
            .ToList()
            .Select(CreateModel)
            .ToList();           
        }
        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new SoftwareInstallationDatabase();
            var warehouse = context.Warehouses
            .Include(rec => rec.WarehouseComponents)
            .ThenInclude(rec => rec.Component)
            .FirstOrDefault(rec => rec.WarehouseName.Equals(model.WarehouseName) || rec.Id == model.Id);
            return warehouse != null ? CreateModel(warehouse) : null;
        }
        public void Insert(WarehouseBindingModel model)
        {
            using var context = new SoftwareInstallationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Warehouse warehouse = new Warehouse()
                {
                    WarehouseName = model.WarehouseName,
                    ResponsiblePerson = model.ResponsiblePerson,
                    DateCreate = model.DateCreate
                };
                context.Warehouses.Add(warehouse);
                context.SaveChanges();
                CreateModel(model, warehouse, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(WarehouseBindingModel model)
        {
            using var context = new SoftwareInstallationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(WarehouseBindingModel model)
        {
            using var context = new SoftwareInstallationDatabase();
            Warehouse element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Warehouses.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse, SoftwareInstallationDatabase context)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.ResponsiblePerson = model.ResponsiblePerson;
            warehouse.DateCreate = model.DateCreate;
            if (model.Id.HasValue)
            {
                var warehouseComponents = context.WarehouseComponents.Where(rec =>
                rec.WarehouseId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.WarehouseComponents.RemoveRange(warehouseComponents.Where(rec =>
                !model.WarehouseComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateComponent in warehouseComponents)
                {
                    updateComponent.Count =
                    model.WarehouseComponents[updateComponent.ComponentId].Item2;
                    model.WarehouseComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var wc in model.WarehouseComponents)
            {
                context.WarehouseComponents.Add(new WarehouseComponent
                {
                    WarehouseId = warehouse.Id,
                    ComponentId = wc.Key,
                    Count = wc.Value.Item2,
                });
                try
                {
                    context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
            return warehouse;
        }
        private static WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            return new WarehouseViewModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                ResponsiblePerson = warehouse.ResponsiblePerson,
                DateCreate = warehouse.DateCreate,
                WarehouseComponents = warehouse.WarehouseComponents
            .ToDictionary(recWC => recWC.ComponentId,
            recWC => (recWC.Component?.ComponentName, recWC.Count))
            };
        }
        public bool WriteOffFromWarehouses(Dictionary<int, (string, int)> components, int writeOffCount)
        {
            using var context = new SoftwareInstallationDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                foreach (KeyValuePair<int, (string, int)> component in components)
                {
                    int count = component.Value.Item2 * writeOffCount;
                    IEnumerable<WarehouseComponent> warehouseComponents = context.WarehouseComponents
                        .Where(warehouse => warehouse.ComponentId == component.Key);
                    foreach (WarehouseComponent warehouseComponent in warehouseComponents)
                    {
                        if (warehouseComponent.Count <= count)
                        {
                            count -= warehouseComponent.Count;
                            context.WarehouseComponents.Remove(warehouseComponent);
                        }
                        else
                        {
                            warehouseComponent.Count -= count;
                            count = 0;
                            break;
                        }
                    }
                    if (count != 0)
                    {
                        throw new Exception("Недостаточно компонентов на складе");
                    }
                }
                context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
