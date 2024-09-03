using LeaveManagementSystem.Models.LeaveAllocations;
using System.ComponentModel;

namespace LeaveManagementSystem.Models.LeaveRequests
{
    public class ReviewLeaveRequestVM : LeaveRequestReadOnlyVM
    {
        public EmployeeListVM Employee { get; set; } = new EmployeeListVM();

        [DisplayName("Additional Information")]
        public string RequestComments { get; set; }
    }
}