using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SoftwareInstallationContracts.Attributes;


namespace SoftwareInstallationContracts.ViewModels
{
    /// Компонент, требуемый для изготовления изделия
    public class ComponentViewModel
    {
        [Column(title: "Номер", width: 70)]
        public int Id { get; set; }
        [Column(title: "Название компонента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ComponentName { get; set; }
    }
}
