using BITC.CMS.Data.Entity;
using BITC.Library.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Service
{
    public class ModuleService : BaseService<Module>, IModuleService
    {
        #region Declaration

        private readonly IRepositoryAsync<Module> _repository;

        #endregion

        #region Declaration

        public ModuleService(IRepositoryAsync<Module> repository)
            : base(repository)
        {
            _repository = repository;
        }

        #endregion

        public Module FindModuleByName(string _moduleName)
        {
            return _repository.Query(i => i.ModuleName == _moduleName).Include(i => i.Settings).Select().FirstOrDefault();
        }
    }
}
