﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Entities.Concrete
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public bool IsApproved { get; set; } = false;
        public bool? IsBlocked { get; set; }


    }
}
