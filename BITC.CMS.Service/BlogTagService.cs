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

        public IQueryable<BlogTag> FindByCulture(string _culture)
        {
            return _repository.Queryable(i => i.Culture == _culture);
        }

        public IQueryable<BlogTag> LoadAllTagsByTerm(string _culture, string _term)
        {
            return _repository.Queryable(i => i.Culture == _culture && i.TagName.StartsWith(_term));
        }
    }
}
