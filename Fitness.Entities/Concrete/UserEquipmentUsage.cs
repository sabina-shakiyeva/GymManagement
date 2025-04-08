using Fitness.Core.Abstraction;
using FitnessManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Entities.Concrete
{
    public class UserEquipmentUsage:BaseEntity,IEntity
    {
        public int UserId { get; set; }
        public int EquipmentId { get; set; }

        public int? DurationInMinutes { get; set; } // dəqiqə ilə
        public int? Repetition { get; set; } // təkrar sayı
        public DateTime Date { get; set; }= DateTime.Now; // istifadə tarixi

        public User User { get; set; }
        public Equipment Equipment { get; set; }
    }
}
