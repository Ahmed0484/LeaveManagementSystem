using LeaveManagementSystem.Data;

namespace LeaveManagementSystem.Services
{
    public interface IUsersService
    {
        Task<List<ApplicationUser>> GetEmployees();
        Task<ApplicationUser> GetLoggedInUser();
        Task<ApplicationUser> GetUserById(string userId);
    }
}
