using BITC.CMS.Data.Entity;
using BITC.Library.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Service
{
    public class SettingService : BaseService<Setting>, ISettingService
    {

        #region Declaration

        private readonly IRepositoryAsync<Setting> _repository;

        #endregion

        #region Declaration

        public SettingService(IRepositoryAsync<Setting> repository)
            : base(repository)
        {
            _repository = repository;
        }

        #endregion
               
    }
}
