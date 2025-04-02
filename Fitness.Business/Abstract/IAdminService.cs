﻿using Fitness.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Business.Abstract
{
    public interface IAdminService
    {
        Task AddAdminAsync(ApplicationUser admin);
    }
}
