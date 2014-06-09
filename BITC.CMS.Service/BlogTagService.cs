using BITC.CMS.Data.Entity;
using BITC.Library.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Service
{
    public class BlogTagService : BaseService<BlogTag>, IBlogTagService
    {
        #region Declaration

        private readonly IRepositoryAsync<BlogTag> _repository;

        #endregion

        #region Declaration

        public BlogTagService(IRepositoryAsync<BlogTag> repository)
            : base(repository)
        {
            _repository = repository;
        }

        #endregion
    }
}
