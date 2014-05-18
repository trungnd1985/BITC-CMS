using BITC.CMS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Repository
{
    public class SettingRepository : IRepository<Setting>
    {

        BITCEntities _dataContext;

        public SettingRepository(BITCEntities context)
        {
            _dataContext = context;
        }

        public Setting SingleOrDefault(System.Linq.Expressions.Expression<Func<Setting, bool>> predicate)
        {
            return _dataContext.Settings.FirstOrDefault(predicate);
        }

        public IQueryable<Setting> Query()
        {
            return _dataContext.Settings.AsQueryable();
        }

        public IQueryable<Setting> Query(System.Linq.Expressions.Expression<Func<Setting, bool>> predicate)
        {
            return Query().Where(predicate);
        }

        public void Insert(Setting _entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Setting _entity)
        {
            var _setting = _dataContext.Settings.FirstOrDefault(i => i.SettingKey == _entity.SettingKey && i.ModuleID == _entity.ModuleID);

            if (_setting != null)
            {
                _setting.SettingValue = _entity.SettingValue;
            }

            _dataContext.Entry(_setting).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(Setting _entity)
        {
            throw new NotImplementedException();
        }
    }
}
