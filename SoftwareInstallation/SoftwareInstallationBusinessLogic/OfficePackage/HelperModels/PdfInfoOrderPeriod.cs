using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareInstallationContracts.ViewModels;


namespace SoftwareInstallationBusinessLogic.OfficePackage.HelperModels
{
    public class PdfInfoOrderPeriod
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportOrdersPeriodViewModel> Orders { get; set; }
    }
}
