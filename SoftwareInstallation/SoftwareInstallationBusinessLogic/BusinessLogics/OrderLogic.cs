using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SoftwareInstallationContracts.BindingModels;
using SoftwareInstallationContracts.BusinessLogicsContracts;
using SoftwareInstallationContracts.StoragesContracts;
using SoftwareInstallationContracts.ViewModels;
using SoftwareInstallationContracts.Enums;


namespace SoftwareInstallationBusinessLogic.BusinessLogics
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderStorage _orderStorage;
        private readonly IWarehouseStorage _warehouseStorage;
        private readonly IPackageStorage _packageStorage;
        private readonly object locker = new();
        public OrderLogic(IOrderStorage orderStorage, IWarehouseStorage warehouseStorage, IPackageStorage packageStorage)
        {
            _orderStorage = orderStorage;
            _packageStorage = packageStorage;
            _warehouseStorage = warehouseStorage;
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return _orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderViewModel> { _orderStorage.GetElement(model) };
            }
            return _orderStorage.GetFilteredList(model);
        }
        public void CreateOrder(CreateOrderBindingModel model)
        {
            _orderStorage.Insert(new OrderBindingModel
            {
                PackageId = model.PackageId,
                ClientId = model.ClientId,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Принят
            });
        }
        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            lock (locker)
            {
                OrderStatus status = OrderStatus.Выполняется;
                var order = _orderStorage.GetElement(new OrderBindingModel
                {
                    Id = model.OrderId
                });
                if (order == null)
                {
                    throw new Exception("Заказ не найден");
                }
                if (!order.Status.Equals("Принят") && !order.Status.Equals("Требуются_материалы"))
                {
                    throw new Exception("Невозможно обработать заказ, заказ не в статусе  \"Принят\" или  \"Требуются_материалы\"");
                }
                var package = _packageStorage.GetElement(new PackageBindingModel { Id = order.PackageId });
                if (!_warehouseStorage.WriteOffFromWarehouses(package.PackageComponents, order.Count))
                {
                    status = OrderStatus.Требуются_материалы;
                    model.ImplementerId = null;
                }
                _orderStorage.Update(new OrderBindingModel
                {
                    Id = order.Id,
                    ClientId = order.ClientId,
                    PackageId = order.PackageId,
                    ImplementerId = model.ImplementerId,
                    Sum = order.Sum,
                    Count = order.Count,
                    DateCreate = order.DateCreate,
                    DateImplement = DateTime.Now,
                    Status = status
                });
            }
        }
        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.OrderId
            });
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            if (order.Status.Equals("Требуются_материалы"))
            {
                return;
            }
            if (!order.Status.Equals("Выполняется"))
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                PackageId = order.PackageId,
                ImplementerId = order.ImplementerId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Готов
            });
        }
        public void DeliveryOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
            if (order == null)
            {
                throw new Exception("Заказ не найден");
            }
            if (!order.Status.Equals("Готов"))
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                PackageId = order.PackageId,
                ImplementerId = order.ImplementerId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Выдан
            });
        }
    }
}
