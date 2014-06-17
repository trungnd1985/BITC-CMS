using BITC.CMS.Data.Entity;
using BITC.Library.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BITC.CMS.Service
{
    public class ProjectService : BaseService<Project>, IProjectService
    {
        #region Declaration

        private readonly IRepositoryAsync<Project> _repository;
        private readonly IRepositoryAsync<ProjectCategory> _categoryRepository;

        #endregion

        #region Constructor

        public ProjectService(IRepositoryAsync<Project> repository, IRepositoryAsync<ProjectCategory> categoryRepository)
            : base(repository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        #endregion

        public override void Insert(Project entity)
        {
            ProjectCategory _category;

            for (int i = 0; i < entity.ProjectCategoriesID.Count; i++)
            {
                _category = _categoryRepository.Find(entity.ProjectCategoriesID[i]);

                if (_category != null)
                {
                    entity.ProjectCategories.Add(_category);
                }
            }

            base.Insert(entity);
        }

        public Project FindByKey(int id)
        {
            return _repository.Query(i => i.ProjectID == id).Include(i => i.ProjectCategories).Select().FirstOrDefault();
        }

        public IQueryable<Project> FindByCulture(string _culture)
        {
            return _repository.Queryable(i => i.Culture == _culture);
        }


        public void Update(int id, Project _model)
        {
            var _current = FindByKey(id);

            if (_current != null)
            {
                _current.ModifiedDate = DateTime.Now;
                _current.ProjectName = _model.ProjectName;
                _current.SortOrder = _model.SortOrder;
                _current.ProjectImages = _model.ProjectImages;
                _current.Year = _model.Year;
                _current.Location = _model.Location;
                _current.IsFeatured = _model.IsFeatured;
                _current.Inactive = _model.Inactive;
                _current.Description = _model.Description;
                _current.ClientID = _model.ClientID;

                foreach (var item in _model.ProjectCategoriesID)
                {
                    if (_current.ProjectCategories.FirstOrDefault(i => i.ProjectCategoryID == item) == null)
                    {
                        var _category = _categoryRepository.Find(item);
                        _current.ProjectCategories.Add(_category);
                    }
                }

                base.Update(_current);
            }
        }


        public IQueryable<Project> FindRecentProject(string _culture)
        {
            return _repository.Query(i => i.Culture == _culture && !i.Inactive && i.IsFeatured).Include(i => i.Client).OrderBy(i => i.OrderBy(p => p.SortOrder)).Select().AsQueryable();
        }


        public IQueryable<Project> FindAvailableProject(string _culture)
        {
            return _repository.Query(i => i.Culture == _culture && !i.Inactive).Include(i => i.ProjectCategories).OrderBy(i => i.OrderByDescending(p => p.Year)).Select().AsQueryable();
        }
    }
}
