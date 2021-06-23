using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SampleChatSignalRCore.Web.Models;
using SampleChatSignalRCore.Web.Models.ViewModels;

namespace SampleChatSignalRCore.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = "Your application description page.";
            var messages = LoadMessageJson();
            ChatViewModel chatViewModel = new ChatViewModel()
            {
                rooms = LoadRoomsJson(),
                messages = messages
            };

            return View(chatViewModel);
        }
        public IActionResult GetChatSection(string userName, int roomId)
        {
            ViewData["userName"] = userName;
            var messages = LoadMessageJson();
            if (roomId > 0)
            {
                messages = messages.Where(x => x.RoomId == roomId).ToList();
            }
            else
            {
                messages = messages.Where(x => x.RoomId == null || x.RoomId == 0).ToList();
            }
            return PartialView("ChatList", messages);
        }

        public IActionResult SetChatRoom(int roomId)
        {
 
            var rooms = LoadRoomsJson();
            ViewData["RoomId"] = roomId;
            return PartialView("ChatRoomsList", rooms);
        }

        public IActionResult UpdateChatFile(Message message)
        {
            var messages = LoadMessageJson();
            messages.Add(message);
            using (StreamWriter file = System.IO.File.CreateText(@"D:\messages.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, messages);
            }
            return Ok();
        }
        public IActionResult AddChatRoom(Room room)
        {
            var messages = LoadRoomsJson();
            messages.Add(room);
            using (StreamWriter file = System.IO.File.CreateText(@"D:\rooms.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, messages);
            }
            return Ok();
        }

        public List<Message> LoadMessageJson()
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


        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
