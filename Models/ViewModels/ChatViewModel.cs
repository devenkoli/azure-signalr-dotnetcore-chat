using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleChatSignalRCore.Web.Models.ViewModels
{
    public class ChatViewModel
    {
        public List<Message> messages { get; set; }
        public List<Room> rooms { get; set; }

    }
}
