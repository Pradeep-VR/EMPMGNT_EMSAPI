using AutoMapper;
using TEAMLIFTSS.Models;
using TEAMLIFTSS.Repos.TableModels;

namespace TEAMLIFTSS.Helper
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            //CreateMap<UsermasterModel, Usermaster>().ForMember(item => item.Empid, opt => opt.MapFrom(
            //    items => (items.Username.Length != 0) ? $"TL{items.Id}\\{items.Username}" : Convert.ToString(items.Id))).ReverseMap();

            //CreateMap<Attendancedetail, Attendancedetail>()
            //.ForMember(x => x.TransactionId, opt => opt.MapFrom(src => Guid.NewGuid()));

        }
    }
}
