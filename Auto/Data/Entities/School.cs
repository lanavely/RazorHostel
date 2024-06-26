﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Auto.Data.Entities.Bookings;

namespace Auto.Data.Entities
{
    public class School
    {
        public int SchoolId { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Адрес")]
        public string Address { get; set; }

        [Phone]
        [DisplayName("Телефон")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [DisplayName("Электронная почта")]
        public string Email { get; set; }

        public Schedule? Schedule { get; set; }
        
        public List<AppUser>? Users { get; set; }
        
        public List<Booking>? Bookings { get; set; }
    }
}
