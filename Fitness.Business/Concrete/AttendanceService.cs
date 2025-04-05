using AutoMapper;
using Fitness.Business.Abstract;
using Fitness.DataAccess.Abstract;
using Fitness.Entities.Models;
using FitnessManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Business.Concrete
{
    public class AttendanceService:IAttendanceService
    {
        private readonly IAttendanceDal _attendanceDal;
        private readonly IMapper _mapper;

        public AttendanceService(IAttendanceDal attendanceDal, IMapper mapper)
        {
            _attendanceDal = attendanceDal;
            _mapper = mapper;
        }

        public async Task<List<AttendanceViewDto>> GetAttendanceByUserIdAsync(int userId)
        {
            var attendances = await _attendanceDal.GetList(a => a.UserId == userId);

            return attendances
                .Select(a => new AttendanceViewDto
                {
                    AttendanceDate = a.AttendanceDate,
                    Status = a.Status
                }).ToList();
        }

        public async Task TakeAttendanceAsync(TakeAttendanceDto dto)
        {
            var attendance = _mapper.Map<Attendance>(dto);
            attendance.AttendanceDate = dto.AttendanceDate.Date;
            await _attendanceDal.Add(attendance);
        }
    }
}
