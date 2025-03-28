using AutoMapper;
using Fitness.Business.Abstract;
using Fitness.DataAccess.Abstract;
using Fitness.Entities.Concrete;
using Fitness.Entities.Models;
using FitnessManagement.Dtos;
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
            trainer.JoinedDate = trainerDto.JoinedDate ?? DateTime.UtcNow; 
            trainer.MobileTelephone = trainerDto.MobileTelephone;
            await _trainerDal.Add(trainer);


            //await _userManager.AddToRoleAsync(identityTrainer, "Trainer");
            if (!await _roleManager.RoleExistsAsync("Trainer"))
            {
                //await _roleManager.CreateAsync(new IdentityRole("User"));
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
