using AutoMapper;
using SampleChatSignalRCore.Web.Models;
using SampleChatSignalRCore.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleChatSignalRCore.Web.Mappings
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomViewModel>();

            CreateMap<RoomViewModel, Room>();
        }
    }
}