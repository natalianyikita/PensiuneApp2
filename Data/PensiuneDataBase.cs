using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using PensiuneApp2.Models;

namespace PensiuneApp2.Data
{
    public class PensiuneDataBase
    {
        readonly SQLiteAsyncConnection _database;

        public PensiuneDataBase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Room>().Wait(); 
            _database.CreateTableAsync<Reservation>().Wait();
            _database.CreateTableAsync<Client>().Wait();
        }

        // Exemplu operație Read (Get all rooms)
        public Task<List<Room>> GetRoomsAsync() => _database.Table<Room>().ToListAsync();

        //Operație Create/Update Room
        public Task<int> SaveRoomAsync(Room room)
        {
            return room.ID != 0 ? _database.UpdateAsync(room) : _database.InsertAsync(room);

        }


        //Operații pentru Camere

        public Task<int> DeleteRoomAsync(Room room)
        {
            return _database.DeleteAsync(room); 
        }

        //Operații pentru Rezervări

        public Task<List<Reservation>> GetReservationsAsync()
        {
            return _database.Table<Reservation>().ToListAsync(); 
        }


        //Operații pentru Rezervări

        public Task<int> SaveReservationAsync(Reservation reservation)
        {
            if (reservation.ID != 0)
            {
                return _database.UpdateAsync(reservation);
            }
            else
            {
                return _database.InsertAsync(reservation);
            }
        }


        public Task<List<Client>> GetClientsAsync()
        {
            return _database.Table<Client>().ToListAsync();
        }

        public Task<int> SaveClientAsync(Client client)
        {
            if (client.ID != 0)
                return _database.UpdateAsync(client);
            else
                return _database.InsertAsync(client);
        }

        public Task<int> DeleteClientAsync(Client client)
        {
            return _database.DeleteAsync(client);
        }

    }
}
