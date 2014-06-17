using BITC.CMS.Data.Entity;
using BITC.Library.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Service
{
    public class ClientService : BaseService<Client>, IClientService
    {
        #region Declaration

        private readonly IRepositoryAsync<Client> _repository;

        #endregion

        #region Constructor

        public ClientService(IRepositoryAsync<Client> repository)
            : base(repository)
        {
            _repository = repository;
        }

        #endregion
    }
}
