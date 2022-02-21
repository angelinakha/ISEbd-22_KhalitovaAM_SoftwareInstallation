﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using SoftwareInstallationContracts.Enums;
using SoftwareInstallationFileImplement.Models;

namespace SoftwareInstallationFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string ComponentFileName = "Component.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string PackageFileName = "Package.xml";
        private readonly string WarehouseFileName = "Warehouse.xml";

        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Package> Packages { get; set; }
        public List<Warehouse> Warehouses { get; set; }
        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Packages = LoadPackages();
            Warehouses = LoadWarehouses();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        public static void SaveAll()
        {
            GetInstance().SaveComponents();
            GetInstance().SaveOrders();
            GetInstance().SavePackages();
            GetInstance().SaveWarehouses();
        }
        ~FileDataListSingleton()
        {
            SaveComponents();
            SaveOrders();
            SavePackages();
            SaveWarehouses();
        }
        private List<Component> LoadComponents()
        {
            var list = new List<Component>();
            if (File.Exists(ComponentFileName))
            {
                var xDocument = XDocument.Load(ComponentFileName);
                var xElements = xDocument.Root.Elements("Component").ToList(); 
                foreach (var elem in xElements)
                {
                    list.Add(new Component
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ComponentName = elem.Element("ComponentName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();

            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);

                var xElements = xDocument.Root.Elements("Order").ToList();

                foreach (var order in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(order.Attribute("Id").Value),
                        PackageId = Convert.ToInt32(order.Element("PackageId").Value),
                        Count = Convert.ToInt32(order.Element("Count").Value),
                        Sum = Convert.ToDecimal(order.Element("Sum").Value),
                        Status = (OrderStatus)Convert.ToInt32(order.Element("Status").Value),
                        DateCreate = Convert.ToDateTime(order.Element("DateCreate").Value),
                        DateImplement = !string.IsNullOrEmpty(order.Element("DateImplement").Value) ? Convert.ToDateTime(order.Element("DateImplement").Value) : (DateTime?)null
                    });
                }
            }
            return list;
        }
        private List<Package> LoadPackages()
        {
            var list = new List<Package>();
            if (File.Exists(PackageFileName))
            {
                var xDocument = XDocument.Load(PackageFileName);
                var xElements = xDocument.Root.Elements("Package").ToList();
                foreach (var elem in xElements)
                {
                    var packComp = new Dictionary<int, int>();
                    foreach (var component in
                   elem.Element("PackageComponents").Elements("PackageComponent").ToList())
                    {
                        packComp.Add(Convert.ToInt32(component.Element("Key").Value),
                       Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Package
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        PackageName = elem.Element("PackageName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        PackageComponents = packComp
                    });
                }
            }
            return list;
        }
        private List<Warehouse> LoadWarehouses()
        {
            var list = new List<Warehouse>();
            if (File.Exists(WarehouseFileName))
            {
                var xDocument = XDocument.Load(WarehouseFileName);
                var xElements = xDocument.Root.Elements("Warehouse").ToList();
                foreach (var elem in xElements)
                {
                    var WHComp = new Dictionary<int, int>();
                    foreach (var component in
                   elem.Element("WarehouseComponents").Elements("WarehouseComponent").ToList())
                    {
                        WHComp.Add(Convert.ToInt32(component.Element("Key").Value),
                       Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Warehouse
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        WarehouseName = elem.Element("WarehouseName").Value,
                        ResponsiblePerson = elem.Element("ResponsiblePerson").Value,
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        WarehouseComponents = WHComp
                    });
                }
            }
            return list;
        }
        private void SaveComponents()
        {
            if (Components != null)
            {
                var xElement = new XElement("Components");
                foreach (var component in Components)
                {
                    xElement.Add(new XElement("Component",
                    new XAttribute("Id", component.Id),
                    new XElement("ComponentName", component.ComponentName)));
                }
                var xDocument = new XDocument(xElement); 
                xDocument.Save(ComponentFileName);
            }
        }
        private void SaveOrders()
        {
            if(Orders != null)
            {
                var xElement = new XElement("Orders");

                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                        new XAttribute("Id", order.Id),
                        new XElement("PackageId", order.PackageId),
                        new XElement("Count", order.Count),
                        new XElement("Sum", order.Sum),
                        new XElement("Status", (int)order.Status),
                        new XElement("DateCreate", order.DateCreate.ToString()),
                        new XElement("DateImplement", order.DateImplement.ToString())));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SavePackages()
        {
            if (Packages != null)
            {
                var xElement = new XElement("Packages");
                foreach (var package in Packages)
                {
                    var compElement = new XElement("PackageComponents");
                    foreach (var component in package.PackageComponents)
                    {
                        compElement.Add(new XElement("PackageComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Package",
                     new XAttribute("Id", package.Id),
                     new XElement("PackageName", package.PackageName),
                     new XElement("Price", package.Price),
                     compElement));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(PackageFileName);
            }
        }
        private void SaveWarehouses()
        {
            if (Warehouses != null)
            {
                var xElement = new XElement("Warehouses");
                foreach (var warehouse in Warehouses)
                {
                    var warehouseElement = new XElement("WarehouseComponents");
                    foreach (var component in warehouse.WarehouseComponents)
                    {
                        warehouseElement.Add(new XElement("WarehouseComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Warehouse",
                     new XAttribute("Id", warehouse.Id),
                     new XElement("WarehouseName", warehouse.WarehouseName),
                     new XElement("ResponsiblePerson", warehouse.ResponsiblePerson),
                     new XElement("DateCreate", warehouse.DateCreate),
                     warehouseElement));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(WarehouseFileName);
            }
        }
    }
}
