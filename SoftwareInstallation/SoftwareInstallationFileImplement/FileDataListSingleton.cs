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
        private readonly string ClientFileName = "Client.xml";
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Package> Packages { get; set; }
        public List<Client> Clients { get; set; }
        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Packages = LoadPackages();
            Clients = LoadClients();
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
            GetInstance().SaveClients();
        }
        ~FileDataListSingleton()
        {
            SaveComponents();
            SaveOrders();
            SavePackages();
            SaveClients();
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
        private List<Client> LoadClients()
        {
            var list = new List<Client>();
            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientFIO = elem.Element("ClientFIO").Value,
                        Email = elem.Element("Email").Value,
                        Password = elem.Element("Password").Value,
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
        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");
                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("ClientFIO", client.ClientFIO),
                    new XElement("Email", client.Email),
                    new XElement("Password", client.Password)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }
    }
}
