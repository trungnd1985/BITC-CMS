using BITC.CMS.Data.Entity;
using BITC.Library.Pattern;
using BITC.Web.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BITC.CMS.Service
{
    public class BlogService : BaseService<BlogEntry>, IBlogService
    {
        private readonly IRepositoryAsync<BlogEntry> _repository;
        private readonly IRepositoryAsync<BlogTag> _tagRepository;

        public BlogService(IRepositoryAsync<BlogEntry> repository, IRepositoryAsync<BlogTag> tagRepository)
            : base(repository)
        {
            _repository = repository;
            _tagRepository = tagRepository;
        }

        public override void Insert(BlogEntry entity)
        {
            entity.Culture = CultureHelper.GetCurrentCulture();
            entity.CreatedBy = HttpContext.Current.User.Identity.Name;
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedBy = HttpContext.Current.User.Identity.Name;
            entity.ModifiedDate = DateTime.Now;

            var arrTagSelected = entity.SelectedTags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var lstTag = _tagRepository.Queryable(i => entity.SelectedTags.Contains(i.TagName));

            string currentTagName;

            for (int i = 0; i < arrTagSelected.Length; i++)
            {
                currentTagName = arrTagSelected[i];

                var newTag = lstTag.FirstOrDefault(t => t.TagName == currentTagName);

                if (newTag == null)
                {
                    newTag = new BlogTag();
                    newTag.TagName = arrTagSelected[i];
                    newTag.Culture = CultureHelper.GetCurrentCulture();
                    newTag.Slug = newTag.TagName.ToSlug();
                    newTag.BlogEntries.Add(entity);
                    _tagRepository.Insert(newTag);
                }
                else
                {
                    entity.BlogTags.Add(newTag);
                }
            }

            base.Insert(entity);
        }

        public void Update(int id, BlogEntry entity)
        {
            var currentEntry = _repository.Query(i => i.BlogEntryID == id).Include(i => i.BlogTags).Select().FirstOrDefault();

            currentEntry.ModifiedBy = HttpContext.Current.User.Identity.Name;
            currentEntry.ModifiedDate = DateTime.Now;
            currentEntry.PublishDate = entity.PublishDate;
            currentEntry.BlogTags.Clear();

            var arrTagSelected = entity.SelectedTags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var lstTag = _tagRepository.Queryable(i => entity.SelectedTags.Contains(i.TagName));

            string currentTagName;

            for (int i = 0; i < arrTagSelected.Length; i++)
            {
                currentTagName = arrTagSelected[i];

                var newTag = lstTag.FirstOrDefault(t => t.TagName == currentTagName);

                if (newTag == null)
                {
                    newTag = new BlogTag();
                    newTag.TagName = arrTagSelected[i];
                    newTag.Culture = CultureHelper.GetCurrentCulture();
                    newTag.Slug = newTag.TagName.ToSlug();
                    newTag.BlogEntries.Add(currentEntry);
                    _tagRepository.Insert(newTag);
                }
                else
                {
                    currentEntry.BlogTags.Add(newTag);
                }
            }

            base.Update(currentEntry);
        }

        public BlogEntry GetBlogEntry(int id)
        {
            return _repository.Query().Include(m => m.BlogTags).Select().SingleOrDefault(i => i.BlogEntryID == id);
        }

        public IQueryable<BlogEntry> FindByCulture(string _culture)
        {
            return _repository.Queryable(i => i.Culture == _culture);
        }

        public IEnumerable<BlogEntry> GetBlogEntriesList(string _culture, string _tag, int _pageIndex, int _pageSize, out int _totalCount)
        {
            int _entriesCount = 0;
            var _currentDate = DateTime.Now;
            var _entries = _repository.Query(i => i.Culture == _culture
                                                    && (string.IsNullOrEmpty(_tag) ? true : i.BlogTags.Where(j => j.Slug == _tag).Count() > 0)
                                                    && (i.PublishDate.HasValue && (i.PublishDate.Value.CompareTo(_currentDate) == 0 || i.PublishDate.Value.CompareTo(_currentDate) == -1)))
                                        .Include(i => i.BlogTags)
                                        .OrderBy(q => q.OrderByDescending(i => i.PublishDate))
                                        .SelectPage(_pageIndex, _pageSize, out _entriesCount);
            _totalCount = _entriesCount;

            return _entries;
        }

        public IEnumerable<BlogEntry> GetRecentBlogEntriesList(string _culture, int _excludeId, int topCount)
        {
            var _currentDate = DateTime.Now;
            return _repository.Query(i => i.Culture == _culture
                                        && (i.PublishDate.HasValue && (i.PublishDate.Value.CompareTo(_currentDate) == 0 || i.PublishDate.Value.CompareTo(_currentDate) == -1))
                                        && i.BlogEntryID != _excludeId)
                                .OrderBy(q => q.OrderByDescending(i => i.PublishDate))
                                .Select().Skip(0).Take(topCount);
        }
    }
}
