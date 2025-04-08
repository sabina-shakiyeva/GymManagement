using Fitness.Business.Abstract;
using Fitness.DataAccess.Abstract;
using Fitness.Entities.Concrete;
using Fitness.Entities.Models;
using FitnessManagement.Entities;
using FitnessManagement.Services;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileService _fileService;

        public AdminService(IAdminDal adminDal, UserManager<ApplicationUser> userManager, IFileService fileService)
        {
            _adminDal = adminDal;
            _userManager = userManager;
            _fileService = fileService;

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

        public async Task UpdateAdminAsync(int adminId, AdminUpdateDto adminUpdateDto)
        {
            var admin = await _adminDal.Get(a => a.Id == adminId);
            if (admin == null)
                throw new Exception("Admin not found");

            var identityUser = await _userManager.FindByIdAsync(admin.IdentityAdminId);
            if (identityUser == null)
                throw new Exception("Identity user not found");

            if (!string.IsNullOrEmpty(adminUpdateDto.Name))
            {
                admin.Name = adminUpdateDto.Name;
                identityUser.FullName = adminUpdateDto.Name;
            }
            if (!string.IsNullOrEmpty(adminUpdateDto.Email))
            {
                admin.Email = adminUpdateDto.Email;
                identityUser.Email = adminUpdateDto.Email;
                identityUser.UserName = adminUpdateDto.Email;
            }

            if (adminUpdateDto.ImageUrl != null)
            {
                string imageUrl = await _fileService.UploadFileAsync(adminUpdateDto.ImageUrl);
                admin.ImageUrl = imageUrl;
            }
            if (!string.IsNullOrEmpty(adminUpdateDto.NewPassword))
            {
                if (!string.IsNullOrEmpty(adminUpdateDto.CurrentPassword))
                {
                    var checkPassword = await _userManager.CheckPasswordAsync(identityUser, adminUpdateDto.CurrentPassword);
                    if (!checkPassword)
                        throw new Exception("Current password is incorrect!");

                    var token = await _userManager.GeneratePasswordResetTokenAsync(identityUser);
                    var result = await _userManager.ResetPasswordAsync(identityUser, token, adminUpdateDto.NewPassword);

                    if (!result.Succeeded)
                        throw new Exception("Failed to change password: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
                else
                {
                    throw new Exception("Current password is required to change the password!");
                }
            }

            admin.UpdatedDate = DateTime.Now;

            await _userManager.UpdateAsync(identityUser);
            await _adminDal.Update(admin);
        }

    }
    
}
