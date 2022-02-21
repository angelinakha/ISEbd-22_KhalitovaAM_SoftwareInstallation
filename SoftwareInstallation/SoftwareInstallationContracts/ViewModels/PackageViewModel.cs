using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace SoftwareInstallationContracts.ViewModels
{
    // Изделие, изготавливаемое в магазине
    public class PackageViewModel
    {
        public int Id { get; set; }
        [DisplayName("Пакеты установки")]
        public string PackageName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        [DisplayName("Компоненты")]
        public Dictionary<int, (string, int)> PackageComponents { get; set; }
    }
}
