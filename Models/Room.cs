using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SampleChatSignalRCore.Web.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string CreatedBy { get; set; }

        [Required]
        public virtual ApplicationUser UserAccount { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}