using LeaveManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Services
{
    public class PeriodsService:IPeriodsService
    {
        private readonly ApplicationDbContext _context;

        public PeriodsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Period> GetCurrentPeriod()
        {
            var currentDate = DateTime.Now;
            var period = await _context.Periods
                .SingleAsync(q => q.EndDate.Year == currentDate.Year);
            return period;
        }
    }
}
