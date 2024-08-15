using AutoMapper;
using Workspace.Entities;

namespace Workspace.BLL.Logic
{
    public class InviteProfile : Profile
    {
        public InviteProfile()
        {

            //маппинг из InviteRollDTO в InviteResponse
            CreateMap<InviteRollDTO, InviteResponse>()
             .ForMember(dest => dest.Id, src => src.MapFrom(x => x.InviteId))
             .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.UserId))
             .ForMember(dest => dest.MartId, src => src.MapFrom(x => x.MartId))
             .ForMember(dest => dest.IsOppened, src => src.MapFrom(x => x.IsOpened))
             .ForMember(dest => dest.MartName, src => src.MapFrom(x => x.MartName))
             .ForMember(dest => dest.InviteText, src => src.MapFrom(x => x.InviteComments))
             .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.UserName));

            //маппинг из InviteRequest в Invite
            CreateMap<InviteRequest, Invite>()
             .ForMember(dest => dest.InviteText, src => src.MapFrom(x => x.InviteText))
             .ForMember(dest => dest.WorkspaceMartObj, src => src.MapFrom(x => x.MartId))
             .ForMember(dest => dest.IsOppened, src => src.MapFrom(x => x.IsOppened));

            //маппинг из InviteRollDTO в InviteResponse
            CreateMap<InviteRollDTO, InviteResponse>()
             .ForMember(dest => dest.Id, src => src.MapFrom(x => x.InviteId))
             .ForMember(dest => dest.InviteText, src => src.MapFrom(x => x.InviteComments))
             .ForMember(dest => dest.MartId, src => src.MapFrom(x => x.MartId))
             .ForMember(dest => dest.IsOppened, src => src.MapFrom(x => x.IsOpened));

            //маппинг из InviteDTO в InviteResponse
            CreateMap<InviteRollDTO, InviteResponse>()
             .ForMember(dest => dest.Id, src => src.MapFrom(x => x.InviteId))
             .ForMember(dest => dest.InviteText, src => src.MapFrom(x => x.InviteComments))
             .ForMember(dest => dest.MartId, src => src.MapFrom(x => x.MartId))
             .ForMember(dest => dest.IsOppened, src => src.MapFrom(x => x.IsOpened));

            //маппинг из InviteDetailRequest в InviteDetailDTO
            CreateMap<InviteDetailRequest, InviteDetailDTO>()
             .ForMember(dest => dest.Comments, src => src.MapFrom(x => x.Comments))
             .ForMember(dest => dest.InviteID, src => src.MapFrom(x => x.InviteID))
             .ForMember(dest => dest.UserID, src => src.MapFrom(x => x.UserID));
        }
    }
}
