using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SoftwareInstallationContracts.Attributes;


namespace SoftwareInstallationContracts.ViewModels
{
    public class ClientViewModel
    {
        [Column(title: "Номер", width: 70)]
        public int Id { get; set; }
        [Column(title: "ФИО клиента", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string ClientFIO { get; set; }
        [Column(title: "Логин", width: 100)]
        public string Email { get; set; }
        [Column(title: "Пароль", width: 100)]
        public string Password { get; set; }
    }
}
