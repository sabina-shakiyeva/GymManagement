using FitnessManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Entities.Models
{
    public class AttendanceViewDto
    {
        public DateTime AttendanceDate { get; set; }
        public AttendanceStatus Status { get; set; }
    }
}
