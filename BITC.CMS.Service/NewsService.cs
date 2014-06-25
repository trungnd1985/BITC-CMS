using BITC.CMS.Data.Entity;
using BITC.Library.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Service
{
    public class NewsService : BaseService<News>, INewsService
    {
        #region Declaration

        private readonly IRepositoryAsync<News> _repository;
        private readonly IRepositoryAsync<NewsCategory> _categoryRepository;

        #endregion

        #region Constructor

        public NewsService(IRepositoryAsync<News> repository, IRepositoryAsync<NewsCategory> categoryRepository)
            : base(repository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        #endregion

        public override void Insert(News entity)
        {
            NewsCategory _category;

            for (int i = 0; i < entity.NewsCategoriesID.Count; i++)
            {
                _category = _categoryRepository.Find(entity.NewsCategoriesID[i]);

                if (_category != null)
                {
                    entity.NewsCategories.Add(_category);
                }
            }

            base.Insert(entity);
        }


        public News GetNews(int id)
        {
            return _repository.Query(i => i.NewsID == id).Include(i => i.NewsCategories).Select().FirstOrDefault();
        }

        public IQueryable<News> FindByCulture(string _culture)
        {
            return _repository.Queryable(i => i.Culture == _culture);
        }

        public void Update(int id, News _model)
        {
            var _current = GetNews(id);

            if (_current != null)
            {
                _current.ModifiedDate = DateTime.Now;
                _current.Title = _model.Title;
                _current.SortOrder = _model.SortOrder;
                _current.NewsImage = _model.NewsImage;
                _current.Inactive = _model.Inactive;

                foreach (var item in _model.NewsCategoriesID)
                {
                    if (_current.NewsCategories.FirstOrDefault(i => i.NewsCategoryID == item) == null)
                    {
                        var _category = _categoryRepository.Find(item);
                        _current.NewsCategories.Add(_category);
                    }
                }

                base.Update(_current);
            }
        }
    }
}
