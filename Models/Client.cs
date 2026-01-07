using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace PensiuneApp2.Models
{
    public class Client
    {
            [PrimaryKey, AutoIncrement]
            public int ID { get; set; }

            public string Nume { get; set; }
            public string Prenume { get; set; }
            public string Email { get; set; }
            public string Telefon { get; set; }

            [OneToMany] 
            public List<Reservation> Reservations { get; set; }
        }
}
