using Fitness.Business.Abstract;
using Fitness.DataAccess.Abstract;
using Fitness.Entities.Concrete;
using FitnessManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Business.Concrete
{
    public class AdminService:IAdminService
    {
        private readonly IAdminDal _adminDal;

        public AdminService(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public async Task AddAdminAsync(ApplicationUser admin)
        {
            var passwordHash = admin.PasswordHash;
            var passwordSalt = admin.SecurityStamp;
            var saltBytes = Encoding.UTF8.GetBytes(passwordSalt);
            var hashBytes = Encoding.UTF8.GetBytes(passwordHash);

            var existingAdmin = await _adminDal.Get(a => a.IdentityAdminId == admin.Id);
            if (existingAdmin == null)
            {
                var newAdmin = new Admin
                {
                    IdentityAdminId = admin.Id,
                    Name = admin.FullName,
                    Email = admin.Email,
                    PasswordHash = hashBytes,
                    PasswordSalt = saltBytes
                };

                await _adminDal.Add(newAdmin);
            }
        }
    }
}
