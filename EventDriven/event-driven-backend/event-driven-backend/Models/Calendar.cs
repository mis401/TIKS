﻿using System.ComponentModel.DataAnnotations;

namespace event_driven_backend.Models
{
    public class Calendar
    {
        [Key]
        public int ID { get; set; }
        [JsonIgnore]
        public List<Event> Events { get; set; } = new List<Event>();

        //pripad zajednici 1:1, ima eventove lista
    }
}
