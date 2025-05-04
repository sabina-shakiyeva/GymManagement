using AutoMapper;
using Fitness.Business.Abstract;
using Fitness.DataAccess.Abstract;
using Fitness.Entities.Concrete;
using Fitness.Entities.Models.Workout;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Business.Concrete
{
    public class WorkoutPlanService: IWorkoutPlanService
    {
        private readonly IWorkoutPlanDal _planDal;
        private readonly IMapper _mapper;

        public WorkoutPlanService(IWorkoutPlanDal planDal, IMapper mapper)
        {
            _planDal = planDal;
            _mapper = mapper;
        }

        public async Task<List<WorkoutPlanGetDto>> GetAllPlansAsync()
        {
            var plans = await _planDal.GetList(
                filter: null,
                include: q => q.Include(p => p.WorkoutDays)
                               .ThenInclude(d => d.Exercises)
                               .Include(p => p.PackageWorkouts)
            );
            return _mapper.Map<List<WorkoutPlanGetDto>>(plans);
        }
        public async Task<WorkoutPlanGetDto> GetPlanByIdAsync(int id)
        {
            var plan = await _planDal.Get(
                p => p.Id == id,
                include: q => q.Include(p => p.WorkoutDays)
                               .ThenInclude(d => d.Exercises)
                               .Include(p => p.PackageWorkouts)
            );

            if (plan == null)
                throw new Exception("Workout plan not found");

            return _mapper.Map<WorkoutPlanGetDto>(plan);
        }
        public async Task AddPlanAsync(WorkoutPlanDto dto)
        {
            var plan = _mapper.Map<WorkoutPlan>(dto);

            plan.PackageWorkouts = dto.PackageIds.Select(pid => new PackageWorkout
            {
                PackageId = pid,
                WorkoutPlan = plan
            }).ToList();

            await _planDal.Add(plan);
        }
        public async Task UpdatePlanAsync(int id, WorkoutPlanDto dto)
        {
            var existing = await _planDal.Get(
                p => p.Id == id,
                include: q => q.Include(p => p.WorkoutDays)
                               .ThenInclude(d => d.Exercises)
                               .Include(p => p.PackageWorkouts)
            );

            if (existing == null)
                throw new Exception("Workout plan not found");

            existing.WorkoutDays.Clear();
            existing.PackageWorkouts.Clear();

            _mapper.Map(dto, existing);

            existing.PackageWorkouts = dto.PackageIds.Select(pid => new PackageWorkout
            {
                PackageId = pid,
                WorkoutPlanId = existing.Id
            }).ToList();

            await _planDal.Update(existing);
        }
        public async Task DeletePlanAsync(int id)
        {
            var plan = await _planDal.Get(p => p.Id == id);
            if (plan == null)
                throw new Exception("Workout plan not found");

            await _planDal.Delete(plan);
        }
    }
}
