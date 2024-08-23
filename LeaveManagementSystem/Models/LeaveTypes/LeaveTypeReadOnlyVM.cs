using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Models.LeaveTypes
{
    public class LeaveTypeReadOnlyVM:BaseLeaveTypeVM
    {
        
        public string Name { get; set; } = string.Empty;
        [Display(Name ="Max allocation of days")]
        public int Days { get; set; }
    }
}
