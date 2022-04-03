﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SoftwareInstallationContracts.Enums;

namespace SoftwareInstallationContracts.ViewModels
{
    /// Заказ
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        [DisplayName("Пакеты установки")]
        public string PackageName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }
        [DisplayName("Статус")]
        public string Status { get; set; }
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }
        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
