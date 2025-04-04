using Fitness.Entities.Concrete;
using FitnessManagement.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;


namespace FitnessManagement.Data

{

    public class GymDbContext :IdentityDbContext<ApplicationUser>

    {

        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options) { }


        public DbSet<Admin> Admins { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Trainer> Trainers { get; set; }

        public DbSet<Workout> Workouts { get; set; }

        public DbSet<Package> Packages { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Equipment> Equipments { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<TrainerSchedule> TrainerSchedules { get; set; }

        public DbSet<UserWorkout> UserWorkouts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            base.OnModelCreating(modelBuilder);

          
            modelBuilder.Entity<User>()

           .HasOne(u => u.Package)

           .WithMany(s => s.Users)

           .HasForeignKey(u => u.PackageId)

           .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<User>()
              .HasOne(u => u.Trainer) 
              .WithMany(t => t.Users) 
              .HasForeignKey(u => u.TrainerId) 
              .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<Workout>()

      .HasOne(w => w.Trainer)

      .WithMany(t => t.Workouts)

      .HasForeignKey(w => w.TrainerId);

            modelBuilder.Entity<UserWorkout>()

       .HasKey(uw => new { uw.UserId, uw.WorkoutId });

            modelBuilder.Entity<UserWorkout>()

       .HasOne(uw => uw.User)

       .WithMany(u => u.UserWorkouts)

       .HasForeignKey(uw => uw.UserId)

        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserWorkout>()

                .HasOne(uw => uw.Workout)

                .WithMany(w => w.UserWorkouts)

                .HasForeignKey(uw => uw.WorkoutId)

                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Attendance>()

    .HasOne(a => a.User)

    .WithMany()

    .HasForeignKey(a => a.UserId)

    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TrainerSchedule>()

    .HasOne(ts => ts.Trainer)

    .WithMany(t => t.TrainerSchedules)

    .HasForeignKey(ts => ts.TrainerId)

    .OnDelete(DeleteBehavior.Cascade);


        }

    }

}

