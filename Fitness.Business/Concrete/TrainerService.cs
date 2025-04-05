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
        public async Task DeclineTrainer(string trainerId)
        {

            var trainer = await _userManager.Users.FirstOrDefaultAsync(t => t.Id == trainerId && !t.IsApproved);

            if (trainer != null)
            {

                await _userManager.DeleteAsync(trainer);
            }
            else
            {
                throw new Exception("Trainer not found in pending trainers!");
            }
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


        public async Task DeleteTrainer(int trainerId)
        {
            var trainer = await _trainerDal.Get(t => t.Id == trainerId);
            if (trainer == null)
            {
                throw new Exception("Trainer not found");
            }

            var identityUser = await _userManager.FindByIdAsync(trainer.IdentityTrainerId);
            if (identityUser != null)
            {

                var result = await _userManager.DeleteAsync(identityUser);
                if (!result.Succeeded)
                {
                    throw new Exception("Trainer silinərkən xəta baş verdi: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            await _trainerDal.Delete(trainer);
        }
        public async Task<List<TrainerGetDto>> GetAllTrainers()
        {
            var trainers = await _trainerDal.GetList();

            var trainerDtos = trainers.Select(trainer => new TrainerGetDto
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Email = trainer.Email,
                MobileTelephone = trainer.MobileTelephone,
                CreatedDate = trainer.CreatedDate,
                Experience=trainer.Experience,
                Salary=trainer.Salary,
                ImageUrl = trainer.ImageUrl != null ? _fileService.GetFileUrl(trainer.ImageUrl) : null
            }).ToList();

            return trainerDtos;
        }

        public async Task<TrainerGetDto> GetTrainerById(int id)
        {
            var trainer = await _trainerDal.Get(t => t.Id == id);


            var trainerDto = new TrainerGetDto
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Email = trainer.Email,
                MobileTelephone = trainer.MobileTelephone,
                CreatedDate = trainer.CreatedDate,
                Experience = trainer.Experience,
                Salary = trainer.Salary,
                ImageUrl = trainer.ImageUrl != null ? _fileService.GetFileUrl(trainer.ImageUrl) : null
            };

            return trainerDto;
        }

        public async Task UpdateTrainer(int trainerId,TrainerUpdateDto trainerUpdateDto)
        {
            var trainer = await _trainerDal.Get(t => t.Id == trainerId);
            if (trainer == null)
            {
                throw new Exception("Trainer not found");
            }

            var identityUser = await _userManager.FindByIdAsync(trainer.IdentityTrainerId);
            if (identityUser == null)
            {
                throw new Exception("Identity user not found");
            }
            if (trainerUpdateDto.MobileTelephone != null)
            {
                trainer.MobileTelephone = trainerUpdateDto.MobileTelephone;

            }

            if (!string.IsNullOrEmpty(trainerUpdateDto.Name))
            {
                trainer.Name = trainerUpdateDto.Name;
                identityUser.FullName = trainerUpdateDto.Name;
            }
 
            if (!string.IsNullOrEmpty(trainerUpdateDto.Email))
            {
                trainer.Email = trainerUpdateDto.Email;
                identityUser.Email = trainerUpdateDto.Email;
                identityUser.UserName = trainerUpdateDto.Email;

            }
            if (!string.IsNullOrEmpty(trainerUpdateDto.Description))
            {
                trainer.Description = trainerUpdateDto.Description;
            }

            if (!string.IsNullOrEmpty(trainerUpdateDto.Experience))
            {
                trainer.Experience = trainerUpdateDto.Experience;
            }

            if (!string.IsNullOrEmpty(trainerUpdateDto.Speciality))
            {
                trainer.Speciality = trainerUpdateDto.Speciality;
            }

            if (trainerUpdateDto.Salary.HasValue)
            {
                trainer.Salary = trainerUpdateDto.Salary.Value;
            }

            if (trainerUpdateDto.IsApproved)
            {
                trainer.IsApproved = trainerUpdateDto.IsApproved;
                identityUser.IsApproved = trainerUpdateDto.IsApproved;
            }


            if (trainerUpdateDto.ImageUrl != null)
            {
                string imageUrl = await _fileService.UploadFileAsync(trainerUpdateDto.ImageUrl);
                trainer.ImageUrl = imageUrl;
            }
            if (trainerUpdateDto.IsActive)
            {
                trainer.IsActive = trainerUpdateDto.IsActive;

            }
            if (!string.IsNullOrEmpty(trainerUpdateDto.NewPassword))
            {
                if (!string.IsNullOrEmpty(trainerUpdateDto.CurrentPassword))
                {

                    var checkPassword = await _userManager.CheckPasswordAsync(identityUser, trainerUpdateDto.CurrentPassword);

                    if (!checkPassword)
                    {
                        throw new Exception("Current password is incorrect!");
                    }
                    var token = await _userManager.GeneratePasswordResetTokenAsync(identityUser);
                    var result = await _userManager.ResetPasswordAsync(identityUser, token,trainerUpdateDto.NewPassword);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to change password: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                    }

                }

                else
                {
                    throw new Exception("Current password is required to change the password!");
                }


            }
            trainer.UpdatedDate = DateTime.Now;
            await _userManager.UpdateAsync(identityUser);
            await _trainerDal.Update(trainer);
        }
    }
}
