using BITC.Library.Data;
using BITC.Library.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.Web.Library.Mvc
{
    public class BitcController<T> : BitcController where T : BaseEntity
    {
        #region Declaration

        protected IRepositoryAsync<T> _repo;
        protected IUnitOfWorkAsync _unitOfWorkAsync;

        #endregion

        #region Constructor

        public BitcController(IRepositoryAsync<T> repo, IUnitOfWorkAsync uow)
        {
            _repo = repo;
            _unitOfWorkAsync = uow;
        }

        #endregion
    }
}
