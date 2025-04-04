﻿using AutoMapper;
using Fitness.Entities.Models;
using FitnessManagement.Dtos;
using FitnessManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Business.Mappers
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Equipment, EquipmentDto>().ReverseMap();
            CreateMap<TakeAttendanceDto, Attendance>();

        }
    }
}
