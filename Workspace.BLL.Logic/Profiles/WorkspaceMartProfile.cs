using AutoMapper;
using Workspace.Entities;

namespace Workspace.BLL.Logic
{
    public class WorkspaceMartProfile : Profile
    {
        public WorkspaceMartProfile()
        {
            //маппинг из WorkspaceMartRequest в WorkspaceMart
            CreateMap<WorkspaceMartRequest, WorkspaceMart>()
//             .ForPath(dest => dest.Owner.Id, src => src.MapFrom(x => x.OwnerId))
             .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name));

            //маппинг из WorkspaceMartRequest в WorkspaceMartDTO
            CreateMap<WorkspaceMartRequest, WorkspaceMartDTO>()
             .ForMember(dest => dest.OwnerId, src => src.MapFrom(x => x.OwnerId))
             .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name));

            //маппинг из WorkspaceMartDTO в WorkspaceMartResponse
            CreateMap<WorkspaceMartDTO, WorkspaceMartResponse>()
             .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
             .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
             .ForMember(dest => dest.OwnerId, src => src.MapFrom(x => x.OwnerId));

            //маппинг из WorkspaceMartDTO в WorkspaceMart
            CreateMap<WorkspaceMartDTO, WorkspaceMart>()
//             .ForPath(dest => dest.Owner.Id, src => src.MapFrom(x => x.Id))
             .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
             .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name));
        }
    }
}
