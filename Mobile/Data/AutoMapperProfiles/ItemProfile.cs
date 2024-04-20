using AutoMapper;
using Mobile.Data.Models;

namespace Mobile.Data.AutoMapperProfiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemDto>()
                .ForMember(dest => dest.NextScheduledDate, opt => opt.MapFrom(src => src.GetNextScheduledDateString));
        }
    }
}
