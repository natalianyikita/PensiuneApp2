using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;


namespace PensiuneApp2.Models
{
    public class Reservation
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(Room))] 
        public int RoomID { get; set; }

        [ManyToOne] 
        public Room Room { get; set; }

        [ForeignKey(typeof(Client))] 
        public int ClientID { get; set; }

        [ManyToOne] 
        public Client Client { get; set; }

        public DateTime CheckIn { get; set; } 
        public DateTime CheckOut { get; set; }
    }
}