using AutoMapper;
using Workspace.Entities;

namespace Workspace.BLL.Logic
{
    public class InviteProfile : Profile
    {
        public InviteProfile()
        {

            //маппинг из InviteResponse в InviteRollDTO
            CreateMap<InviteRollDTO, InviteResponse>()
             .ForMember(dest => dest.Id, src => src.MapFrom(x => x.InviteId))
             .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.UserId))
             .ForMember(dest => dest.MartId, src => src.MapFrom(x => x.MartId))
             .ForMember(dest => dest.IsOppened, src => src.MapFrom(x => x.IsOpened))
             .ForMember(dest => dest.MartName, src => src.MapFrom(x => x.MartName))
             .ForMember(dest => dest.InviteText, src => src.MapFrom(x => x.InviteComments))
             .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.UserName));
        }
    }
}
