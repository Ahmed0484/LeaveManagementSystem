using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Models.LeaveTypes
{
    public class LeaveTypeCreateVM
    {
        [Required]
        [Length(4,150,ErrorMessage="You Violated length requirements")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(1,90)]
        public int Days { get; set; }
    }
}
