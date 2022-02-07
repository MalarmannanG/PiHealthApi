using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity
{
    public class AppUser : BaseEntity
    {
        public AppUser()
        {
            Tokens = new List<UserToken>();
            Appointments = new List<Appointment>();
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }       
        public string UserType { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string SerialNumber { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }   

        public DateTimeOffset? LastLoggedIn { get; set; }

        public virtual ICollection<UserToken> Tokens { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }


    }
}
