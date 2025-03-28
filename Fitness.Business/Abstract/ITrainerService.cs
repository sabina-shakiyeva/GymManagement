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
        Task<List<Trainer>> GetAllTrainers();
        Task<TrainerDto> GetTrainerById(int id);
        Task DeleteTrainer(int id);
        Task UpdateTrainer(TrainerUpdateDto trainerUpdateDto);
    }
}
