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
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using (SoftwareInstallationDatabase context = new SoftwareInstallationDatabase())
            {
                return context.Orders
                .Include(rec => rec.Package)
                .Include(rec => rec.Implementer)
                .Include(rec => rec.Client)
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    ClientFIO = rec.Client.ClientFIO,
                    ImplementerId = rec.ImplementerId,
                    ImplementerFIO = rec.ImplementerId.HasValue ? rec.Implementer.ImplementerFIO : string.Empty,
                    PackageId = rec.PackageId,
                    PackageName = rec.Package.PackageName,
                    Count = rec.Count,
                    Sum = rec.Sum,
                    Status = Enum.GetName(rec.Status),
                    DateCreate = rec.DateCreate,
                    DateImplement = rec.DateImplement,
                })
                .ToList();
            }
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new SoftwareInstallationDatabase();
            return context.Orders
            .Include(rec => rec.Package)
             .Include(rec => rec.Client)
            .Include(rec => rec.Implementer)
            .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue &&
            rec.DateCreate.Date == model.DateCreate.Date) ||
             (model.DateFrom.HasValue && model.DateTo.HasValue &&
            rec.DateCreate.Date >= model.DateFrom.Value.Date && rec.DateCreate.Date <=
            model.DateTo.Value.Date) ||
             (model.ClientId.HasValue && rec.ClientId == model.ClientId) ||
            (model.SearchStatus.HasValue && model.SearchStatus.Value ==
            rec.Status) ||
            (model.ImplementerId.HasValue && rec.ImplementerId == model.ImplementerId && model.Status == rec.Status))
           .Select(rec => new OrderViewModel
           {
               Id = rec.Id,
               PackageId = rec.PackageId,
               PackageName = rec.Package.PackageName,
               ClientId = rec.ClientId,
               ClientFIO = rec.Client.ClientFIO,
               ImplementerId = rec.ImplementerId,
               ImplementerFIO = rec.Implementer.ImplementerFIO,
               Count = rec.Count,
               Sum = rec.Sum,
               Status = Enum.GetName(rec.Status),
               DateCreate = rec.DateCreate,
               DateImplement = rec.DateImplement,
           })
            .ToList();
        }
    
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (SoftwareInstallationDatabase context = new SoftwareInstallationDatabase())
            {
                Order order = context.Orders.Include(rec => rec.Package)
                .Include(rec => rec.Implementer)
                .Include(rec => rec.Client)
                .FirstOrDefault(rec => rec.Id == model.Id);
                return order != null ?
                new OrderViewModel
                {
                    Id = order.Id,
                    PackageId = order.PackageId,
                    PackageName = order.Package.PackageName,
                    ImplementerId = order.ImplementerId,
                    ClientId = order.ClientId,
                    ClientFIO = order.Client.ClientFIO,                  
                    Count = order.Count,
                    Sum = order.Sum,
                    Status = Enum.GetName(order.Status),
                    DateCreate = order.DateCreate,
                    DateImplement = order.DateImplement,
                } :
                null;
            }
        }
        public void Insert(OrderBindingModel model)
        {
            using (SoftwareInstallationDatabase context = new SoftwareInstallationDatabase())
            {
                Order order = new Order
                {
                    PackageId = model.PackageId,
                    Count = model.Count,
                    ImplementerId = model.ImplementerId,
                    ClientId = (int)model.ClientId,
                    Sum = model.Sum,
                    Status = model.Status,
                    DateCreate = model.DateCreate,
                    DateImplement = model.DateImplement,
                };
                context.Orders.Add(order);
                context.SaveChanges();
                CreateModel(model, order);
                context.SaveChanges();
            }
        }
        public void Update(OrderBindingModel model)
        {
            using (SoftwareInstallationDatabase context = new SoftwareInstallationDatabase())
            {
                Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                element.PackageId = model.PackageId;
                element.Count = model.Count;
                element.Sum = model.Sum;
                element.ImplementerId = model.ImplementerId;
                element.Status = model.Status;
                element.DateCreate = model.DateCreate;
                element.DateImplement = model.DateImplement;
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using var context = new SoftwareInstallationDatabase();
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Orders.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Order CreateModel(OrderBindingModel model, Order order)
        {
            if (model == null)
            {
                return null;
            }
            using (SoftwareInstallationDatabase context = new SoftwareInstallationDatabase())
            {
                Package element = context.Packages.FirstOrDefault(rec => rec.Id == model.PackageId);
                Implementer impl = context.Implementers.FirstOrDefault(rec => rec.Id == model.ImplementerId);
                if (impl != null)
                {
                    if (impl.Orders == null)
                    {
                        impl.Orders = new List<Order>();
                        context.Implementers.Update(impl);
                        context.SaveChanges();
                    }
                    impl.Orders.Add(order);
                }
                if (element != null)
                {
                    if (element.Orders == null)
                    {
                        element.Orders = new List<Order>();
                    }
                    element.Orders.Add(order);
                    context.Packages.Update(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }

            return order;

        }
        
    }
}
