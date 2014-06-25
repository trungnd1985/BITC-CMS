using BITC.CMS.Data.Entity;
using BITC.Library.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Service
{
    public class NewsCategoryService : BaseService<NewsCategory>, INewsCategoryService
    {
        #region Declaration

        private readonly IRepositoryAsync<NewsCategory> _repository;

        #endregion

        #region Constructor

        public NewsCategoryService(IRepositoryAsync<NewsCategory> repository)
            : base(repository)
        {
            _repository = repository;
        }

        #endregion

        public IQueryable<NewsCategory> FindByCulture(string _culture)
        {
            throw new NotImplementedException();
        }

        public IQueryable<NewsCategory> LoadAllNewsCategoryByTerm(string _culture, string _term)
        {
            throw new NotImplementedException();
        }
    }
}
