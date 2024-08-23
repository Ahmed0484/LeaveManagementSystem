using AutoMapper;
using LeaveManagementSystem.Data;
using LeaveManagementSystem.Models.LeaveTypes;

namespace LeaveManagementSystem.MappingProfiles
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LeaveType, LeaveTypeReadOnlyVM>().ForMember(
                dest => dest.Days, opt => opt.MapFrom(src => src.NumberOfDays)
            );
            CreateMap<LeaveTypeCreateVM, LeaveType>().ForMember(
                dest => dest.NumberOfDays, opt => opt.MapFrom(src => src.Days)
            );
            CreateMap<LeaveTypeEditVM, LeaveType>().ForMember(
                dest => dest.NumberOfDays, opt => opt.MapFrom(src => src.Days)
            );
            CreateMap<LeaveType,LeaveTypeEditVM>().ForMember(
               dest => dest.Days, opt => opt.MapFrom(src => src.NumberOfDays)
           );
        }
    }
}
