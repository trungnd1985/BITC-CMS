using BITC.CMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Service
{
    public interface IModuleService:IService<Module>
    {
        Module FindModuleByName(string _moduleName);
    }
}
