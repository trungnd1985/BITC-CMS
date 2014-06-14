using BITC.CMS.Data.Entity;
using BITC.Library.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Service
{
    public class ProjectCategoryService : BaseService<ProjectCategory>, IProjectCategoryService
    {
        #region Declaration

        private readonly IRepositoryAsync<ProjectCategory> _repository;

        #endregion

        #region Constructor

        public ProjectCategoryService(IRepositoryAsync<ProjectCategory> repository)
            : base(repository)
        {
            _repository = repository;
        }

        #endregion

        public IQueryable<ProjectCategory> FindByCulture(string _culture)
        {
            return _repository.Queryable(i => i.Culture == _culture);
        }
    }
}
