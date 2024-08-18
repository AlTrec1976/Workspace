using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workspace.Entities;

namespace Workspace.BLL.Logic
{
    public class SendboxProfile : Profile
    {
        public SendboxProfile()
        {
            CreateMap<SendboxFullDTO, SendboxFullRequest>()
                .ForMember(dest => dest.InviteId, src => src.MapFrom(x => x.InviteId))
                .ForMember(dest => dest.MartId, src => src.MapFrom(x => x.MartId))
                .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.UserId))
                .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.UserName))
                .ForMember(dest => dest.MartName, src => src.MapFrom(x => x.MartName));
        }
    }
}
