using LeaveManagementSystem.Data;

namespace LeaveManagementSystem.Services
{
    public interface IPeriodsService
    {
        Task<Period> GetCurrentPeriod();
    }
}
