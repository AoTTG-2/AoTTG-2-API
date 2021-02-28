using AoTTG2.IDS.Controllers.NSwag;
using AoTTG2.IDS.Data.Dao;
using AutoMapper;

namespace AoTTG2.IDS.AutoMapper
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            CreateMap<Report, ReportDao>().ReverseMap();
        }
    }
}
