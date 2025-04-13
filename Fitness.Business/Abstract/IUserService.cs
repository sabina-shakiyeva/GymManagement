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
    public interface IUserService
    {
        Task AddUser(UserDto userDto);
        Task<List<UserGetDto>> GetAllUsers();
        Task<UserGetDto> GetUserById(int id);
        Task DeleteUser(int id);
        Task UpdateUser(int userId,UserUpdateDto userUpdateDto);
        Task<List<ApplicationUser>> GetPendingUsers();
        Task ApproveUser(string userId);
        Task DeclineUser(string userId);
        Task<List<TopUserDto>> GetTop10UsersByPointsAsync();
        Task<UserPackageTrainerDto> GetUserPackageTrainer(int id);
        Task UpdateUserPackageTrainer(int id, UserPackageTrainerUpdateDto dto);
      



    }
}
