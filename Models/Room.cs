using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace PensiuneApp2.Models
{
    public class Room
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Unique]
        public string RoomNumber { get; set; }
        public string Type { get; set; } // ex: Single, Double

        [OneToMany]
        public List<Reservation> Reservations { get; set; }
    }
}
