using AutoMapper;
using Fitness.Business.Abstract;
using Fitness.DataAccess.Abstract;
using Fitness.Entities.Models.Trainer;
using FitnessManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Business.Concrete
{
    public class TrainerScheduleService:ITrainerScheduleService
    {
        private readonly ITrainerScheduleDal _trainerScheduleDal;
        private readonly IMapper _mapper;

        public TrainerScheduleService(ITrainerScheduleDal trainerScheduleDal,IMapper mapper)
        {
            _trainerScheduleDal = trainerScheduleDal;
            _mapper = mapper;
        }
        public async Task<TrainerScheduleDto> CreateScheduleAsync(TrainerScheduleCreateDto dto)
        {
            var schedule = new TrainerSchedule
            {
                TrainerId = dto.TrainerId,
                //burada problem var duzelt
                UserId = dto.UserId,
                GroupId = dto.GroupId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            await _trainerScheduleDal.Add(schedule);
            return _mapper.Map<TrainerScheduleDto>(schedule);
        }

        public async Task<List<TrainerScheduleDto>> GetAllAsync()
        {
            var schedules = await _trainerScheduleDal.GetAllAsync();
            return _mapper.Map<List<TrainerScheduleDto>>(schedules);
        }

        public async Task<TrainerScheduleDto> GetByIdAsync(int id)
        {
            var schedule = await _trainerScheduleDal.GetByIdAsync(id);
            return _mapper.Map<TrainerScheduleDto>(schedule);
        }

        public async Task<List<TrainerScheduleDto>> GetByUserIdAsync(int userId)
        {
            var list = await _trainerScheduleDal.GetList(s => s.UserId == userId);
            return _mapper.Map<List<TrainerScheduleDto>>(list);
        }

        public async Task<List<TrainerScheduleDto>> GetByGroupIdAsync(int groupId)
        {
            var list = await _trainerScheduleDal.GetList(s => s.GroupId == groupId);
            return _mapper.Map<List<TrainerScheduleDto>>(list);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _trainerScheduleDal.GetByIdAsync(id);
            if (entity == null) return false;

            await _trainerScheduleDal.Delete(entity);
            return true;
        }

        public async Task<TrainerScheduleDto> UpdateAsync(TrainerScheduleUpdateDto dto)
        {
            var entity = await _trainerScheduleDal.GetByIdAsync(dto.Id);
            if (entity == null) return null;

            entity.StartTime = dto.StartTime;
            entity.EndTime = dto.EndTime;
            entity.Description = dto.Description;

            await _trainerScheduleDal.Update(entity);
            return _mapper.Map<TrainerScheduleDto>(entity);
        }

    }
}
    