using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SoftwareInstallationContracts.Attributes;



namespace SoftwareInstallationContracts.ViewModels
{
    // Изделие, изготавливаемое в магазине
    public class PackageViewModel
    {
        [Column(title: "Номер", width: 70)]
        public int Id { get; set; }
        [Column(title: "Название пакета установки", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string PackageName { get; set; }
        [Column(title: "Цена", width: 100)]
        public decimal Price { get; set; }
        [Column(title: "Компоненты", width: 150)]
        public Dictionary<int, (string, int)> PackageComponents { get; set; }
    }
}
