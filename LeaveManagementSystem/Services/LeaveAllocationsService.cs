using AutoMapper;
using LeaveManagementSystem.Common;
using LeaveManagementSystem.Data;
using LeaveManagementSystem.Models.LeaveAllocations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Services
{
    public class LeaveAllocationsService : ILeaveAllocationsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        private readonly IPeriodsService _periodsService;

        public LeaveAllocationsService(ApplicationDbContext context,
            IUsersService usersService, IMapper mapper,
            IPeriodsService periodsService)
        {
            _context = context;
            _usersService = usersService;
            _mapper = mapper;
            _periodsService = periodsService;
        }

        public async Task AllocateLeave(string employeeId)
        {
            // get all the leave types that not allocated for this emp
            var leaveTypes = await _context.LeaveTypes
           .Where(q => !q.LeaveAllocations!.Any(x => x.EmployeeId == employeeId))
           .ToListAsync();

            // get the current period based on the year
            var period =await  _periodsService.GetCurrentPeriod();
            var monthsRemaining = period.EndDate.Month - DateTime.Now.Month;

            // foreach leave type, create an allocation entry
            foreach (var leaveType in leaveTypes)
            {
                var accuralRate = decimal.Divide(leaveType.NumberOfDays, 12);
                var leaveAllocation = new LeaveAllocation
                {
                    EmployeeId = employeeId,
                    LeaveTypeId = leaveType.Id,
                    PeriodId = period.Id,
                    Days = (int)Math.Ceiling(accuralRate * monthsRemaining)
                };

                _context.Add(leaveAllocation);
            }

            await _context.SaveChangesAsync();
        }





        public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userId)
        {
            var user = string.IsNullOrEmpty(userId)
                ? await _usersService.GetLoggedInUser()
                : await _usersService.GetUserById(userId);

            var allocations = await GetAllocations(user!.Id);
            var allocationVmList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);
            var leaveTypesCount = await _context.LeaveTypes.CountAsync();

            var employeeVm = new EmployeeAllocationVM
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                LeaveAllocations = allocationVmList,
                IsCompletedAllocation = leaveTypesCount == allocations.Count
            };

            return employeeVm;
        }


        public async Task<List<EmployeeListVM>> GetEmployees()
        {
            var users = await _usersService.GetEmployees();
            var employees = _mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(users.ToList());

            return employees;
        }
        private async Task<List<LeaveAllocation>> GetAllocations(string? userId)
        {
            var period = await _periodsService.GetCurrentPeriod();
            var leaveAllocations = await _context.LeaveAllocations
               .Include(q => q.LeaveType)
               .Include(q => q.Period)
               .Where(q => q.EmployeeId == userId && q.PeriodId==period.Id)
               .ToListAsync();
            return leaveAllocations;
        }

        public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId)
        {
            var allocation = await _context.LeaveAllocations
                   .Include(q => q.LeaveType)
                   .Include(q => q.Employee)
                   .FirstOrDefaultAsync(q => q.Id == allocationId);

            var model = _mapper.Map<LeaveAllocationEditVM>(allocation);

            return model;
        }

        public async Task EditAllocation(LeaveAllocationEditVM allocationEditVm)
        {
            //var leaveAllocation = await GetEmployeeAllocation(allocationEditVm.Id);
            //if (leaveAllocation == null)
            //{
            //    throw new Exception("Leave allocation record does not exist.");
            //}
            //leaveAllocation.Days = allocationEditVm.Days;
            // option 1 _context.Update(leaveAllocation);
            // option 2 _context.Entry(leaveAllocation).State = EntityState.Modified;
            // await _context.SaveChangesAsync();

            await _context.LeaveAllocations
                .Where(q => q.Id == allocationEditVm.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(e => e.Days, allocationEditVm.Days));
        }

        public async Task<LeaveAllocation> GetCurrentAllocation(int leaveTypeId, string employeeId)
        {
            var period = await _periodsService.GetCurrentPeriod();
            var allocation = await _context.LeaveAllocations
                    .FirstAsync(q => q.LeaveTypeId == leaveTypeId
                    && q.EmployeeId == employeeId
                    && q.PeriodId == period.Id);
            return allocation;
        }

        private async Task<bool> AllocationExists(string userId, int periodId, int leaveTypeId)
        {
            return await _context.LeaveAllocations.AnyAsync(q =>
                q.EmployeeId == userId
                && q.LeaveTypeId == leaveTypeId
                && q.PeriodId == periodId
            );
        }
    }
}
