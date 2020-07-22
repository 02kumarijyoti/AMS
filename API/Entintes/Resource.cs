using System;
using API.Entities;

namespace API.Entintes
{
    public class Resource :BaseEntity
    {
       // public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
      
    }
}