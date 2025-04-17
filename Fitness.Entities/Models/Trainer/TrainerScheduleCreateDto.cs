﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Entities.Models.Trainer
{
    public class TrainerScheduleCreateDto
    {
        public int TrainerId { get; set; }
        public int? UserId { get; set; }     
        public int? GroupId { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
    }
}
