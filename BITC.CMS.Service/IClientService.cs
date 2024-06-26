﻿using BITC.CMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITC.CMS.Service
{
    public interface IClientService : IService<Client>
    {
        IQueryable<Client> FindAvailableClient();
    }
}
