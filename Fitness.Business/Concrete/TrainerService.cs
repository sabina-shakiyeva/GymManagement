using AutoMapper;
using Fitness.Business.Abstract;
using Fitness.DataAccess.Abstract;
using Fitness.DataAccess.Concrete.EfEntityFramework;
using Fitness.Entities.Concrete;
using Fitness.Entities.Models;
using FitnessManagement.Dtos;
using FitnessManagement.Entities;
using FitnessManagement.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Business.Concrete
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerDal _trainerDal;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TrainerService(ITrainerDal trainerDal, IMapper mapper, IFileService fileService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _trainerDal = trainerDal;
            _mapper = mapper;
            _fileService = fileService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task ApproveTrainer(string trainerId)
        {
            
            var trainer = await _userManager.Users
                .Where(u => u.Id == trainerId && !u.IsApproved)  
                .FirstOrDefaultAsync();

            if (trainer == null)
            {
                throw new Exception("Trainer tapılmadı və ya artıq təsdiqlənib.");
            }

           
            trainer.IsApproved = true;

            var identityUser = await _userManager.FindByIdAsync(trainerId);
            if (identityUser == null)
            {
                throw new Exception("IdentityUser tapılmadı.");
            }

            var passwordHash = identityUser.PasswordHash;
            var passwordSalt = identityUser.SecurityStamp;

            var saltBytes = Encoding.UTF8.GetBytes(passwordSalt);
            var hashBytes = Encoding.UTF8.GetBytes(passwordHash);

         
            var existingTrainer = await _trainerDal.Get(t => t.IdentityTrainerId == trainer.Id);
            if (existingTrainer == null)
            {
                var newTrainer = new Trainer
                {
                    IdentityTrainerId = identityUser.Id,
                    Name = identityUser.FullName,
                    Email = identityUser.Email,
                    IsActive = true,
                    IsApproved = true,
                    PasswordHash = hashBytes,
                    PasswordSalt = saltBytes,
                };
                await _trainerDal.Add(newTrainer);
            }

           
            var result = await _userManager.UpdateAsync(identityUser);
            if (!result.Succeeded)
            {
                throw new Exception("Trainer məlumatları yenilənərkən səhv baş verdi.");
            }
        }

        public async Task<List<ApplicationUser>> GetPendingTrainers()
        {
            var allUsers = await _userManager.Users
                .Where(u => !u.IsApproved)
                .ToListAsync();

            var pendingTrainers = new List<ApplicationUser>();

            foreach (var user in allUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Trainer"))
                {
                    pendingTrainers.Add(user);
                }
            }

            return pendingTrainers;
        }



        public async Task AddTrainer(TrainerDto trainerDto)
        {
           
            var trainer = _mapper.Map<Trainer>(trainerDto);

            
            var identityTrainer = new ApplicationUser
            {
                FullName = trainerDto.Name,
                UserName = trainerDto.Email,
                Email = trainerDto.Email,
                IsApproved = trainerDto.IsApproved,
            };

            var result = await _userManager.CreateAsync(identityTrainer, trainerDto.Password);
            if (!result.Succeeded)
            {
                throw new Exception("Təlimçi əlavə edilə bilmədi: " +
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }

           
            trainer.IdentityTrainerId = identityTrainer.Id;

            
            var passwordHash = identityTrainer.PasswordHash;
            var passwordSalt = identityTrainer.SecurityStamp;
            var saltBytes = Encoding.UTF8.GetBytes(passwordSalt);
            var hashBytes = Encoding.UTF8.GetBytes(passwordHash);
            trainer.PasswordHash = hashBytes;
            trainer.PasswordSalt = saltBytes;

       
            if (trainerDto.ImageUrl != null)
            {
                string imageUrl = await _fileService.UploadFileAsync(trainerDto.ImageUrl);
                trainer.ImageUrl = imageUrl;
            }

           
            if (trainerDto.IsApproved != null) 
            {
                trainer.IsApproved = trainerDto.IsApproved;
            }
            trainer.Description = trainerDto.Description;
            trainer.Experience = trainerDto.Experience;
            trainer.Salary = trainerDto.Salary ?? 0; 
            //trainer.JoinedDate = trainerDto.JoinedDate ?? DateTime.UtcNow; 
            trainer.MobileTelephone = trainerDto.MobileTelephone;
            await _trainerDal.Add(trainer);


            if (!await _roleManager.RoleExistsAsync("Trainer"))
            {
                
                await _roleManager.CreateAsync(new IdentityRole("Trainer"));
            }
        }

        public Task DeleteTrainer(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Trainer>> GetAllTrainers()
        {
            throw new NotImplementedException();
        }

        public Task<TrainerDto> GetTrainerById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTrainer(TrainerUpdateDto trainerUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
