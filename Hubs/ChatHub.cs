using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SampleChatSignalRCore.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SampleChatSignalRCore.Web.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message,int? roomId)
        {
            Guid guid = Guid.NewGuid();
            Random random = new Random();
            int i = random.Next();
            Message messageItem = new Message()
            {
                Content = message,
                UserName = user,
                Id = i,
                RoomId = roomId,
                Timestamp = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(),
            };
            UpdateChatFile(messageItem);
            //var readOnlyList = new ReadOnlyCollection<string>(UserHandler.ConnectedIds.ToList());
            //await Clients.Clients(readOnlyList).SendAsync("ReceiveMessage", user, message, roomId);
            await Clients.All.SendAsync("ReceiveMessage", user, message,roomId);
        }
        public async Task CreateRoom(string roomName,string userName)
        {
            Guid guid = Guid.NewGuid();
            Random random = new Random();
            int i = random.Next();
            Room room = new Room()
            {
                Id = i,
                Name = roomName,
                CreatedBy = userName

            };
            UpdateRoomFile(room);
            await Clients.All.SendAsync("RoomAdded", room);
        }

        public void UpdateRoomFile(Room room)
        {
            try
            {
                var rooms = LoadRoomsJson();
                rooms.Add(room);
                using (StreamWriter file = System.IO.File.CreateText(@"D:\rooms.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, rooms);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateChatFile(Message message)
        {
            try
            {
                var messages = LoadJson();
                messages.Add(message);
                using (StreamWriter file = System.IO.File.CreateText(@"D:\messages.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, messages);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public List<Message> LoadJson()
        {
            try
            {
                using (StreamReader r = new StreamReader(@"D:\messages.json"))
                {
                    string json = r.ReadToEnd();
                    List<Message> items = JsonConvert.DeserializeObject<List<Message>>(json);
                    return items ?? new List<Message>();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Message>();
            }

        }
        public List<Room> LoadRoomsJson()
        {
            try
            {
                using (StreamReader r = new StreamReader(@"D:\rooms.json"))
                {
                    string json = r.ReadToEnd();
                    List<Room> items = JsonConvert.DeserializeObject<List<Room>>(json);
                    return items ?? new List<Room>();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Room>();
            }
        }

        public override Task OnConnectedAsync()
        {
            //UserHandler.ConnectedIds.Add(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            //UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

    }
    public static class UserHandler
    {
        public static HashSet<string> ConnectedIds = new HashSet<string>();
    }
}