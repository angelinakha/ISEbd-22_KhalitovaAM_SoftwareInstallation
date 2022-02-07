using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareInstallationContracts.BindingModels;
using SoftwareInstallationContracts.ViewModels;


namespace SoftwareInstallationContracts.BusinessLogicsContracts
{
    public interface IPackageLogic
    {
        List<PackageViewModel> Read(PackageBindingModel model);
        void CreateOrUpdate(PackageBindingModel model);
        void Delete(PackageBindingModel model);
    }
}
