using BITC.CMS.Data.Entity;
using BITC.Library.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Service
{
    public class MenuService : BaseService<Menu>, IMenuService
    {
        #region Declaration

        private readonly IRepositoryAsync<Menu> _repository;

        #endregion

        #region Constructor

        public MenuService(IRepositoryAsync<Menu> repository)
            : base(repository)
        {
            _repository = repository;
        }

        #endregion

        public IQueryable<Menu> FindMenuInactive(string _culture)
        {
            return _repository.Queryable(i => i.Culture == _culture && !i.Inactive);
        }
    }
}
