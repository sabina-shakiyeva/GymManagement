using Fitness.Entities.Concrete;
using Fitness.Entities.Models;
using FitnessManagement.Dtos;
using FitnessManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Business.Abstract
{
    public interface ITrainerService
    {
        Task AddTrainer(TrainerDto trainerDto);
        Task<List<TrainerGetDto>> GetAllTrainers();
        Task<TrainerGetDto> GetTrainerById(int id);
        Task DeleteTrainer(int id);
        Task UpdateTrainer(int trainerId,TrainerUpdateDto trainerUpdateDto);
        Task<List<ApplicationUser>> GetPendingTrainers();
        Task ApproveTrainer(string trainerId);
        Task DeclineTrainer(string trainerId);
        ///
        Task<List<UserGetDto>> GetAllUsersByTrainer(int trainerId);
        Task<UserGetDto> GetUserByIdForTrainer(int userId, int trainerId);
        Task UpdateUserByTrainer(int userId, int trainerId, UserUpdateDto userUpdateDto);
        Task DeleteUserByTrainer(int userId, int trainerId);
       

    }
}
