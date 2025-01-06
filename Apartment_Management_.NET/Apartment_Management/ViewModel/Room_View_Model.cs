using Apartment_Management.Model;
using Apartment_Management.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Apartment_Management.ViewModel
{
	internal class Room_View_Model
	{
        private readonly FirebaseService _firebaseService;
        public Room Room { get; set; }
        public ObservableCollection<Dweller> DwellerList { get;set; }
        public ObservableCollection<Stuff> StuffList { get; set; }

        public Room_View_Model() { }
        public Room_View_Model(MainWindowViewModel mainWindow, Room room)
        {
            DwellerList = new ObservableCollection<Dweller>();
            StuffList = new ObservableCollection<Stuff>();
            _firebaseService = new FirebaseService();
            Room = new Room(room);
            GetAsync(DwellerList, StuffList);
        }

        private async void GetAsync(ObservableCollection<Dweller> dwellerList, ObservableCollection<Stuff> stuffList)
        {
            var Dwellers = await _firebaseService.GetDataAsync<Dweller>("Dwellers", "DwellerID");
            var RoomDwellers = Dwellers.Where(dweller => dweller.RoomID == Room.RoomID);
            foreach (var dweller in RoomDwellers)
            {
                dwellerList.Add(dweller);
            }
            var HasStuff = await _firebaseService.GetDataAsync<HasStuff>("HasStuffs", "HasStuffID");
            var RoomHasStuff = HasStuff.Where(hasstuff => hasstuff.RoomID == Room.RoomID);
            var Stuff = await _firebaseService.GetDataAsync<Stuff>("Stuffs", "StuffID");
            var RoomStuff = Stuff.Where(stuff => RoomHasStuff.Any(hasstuff => hasstuff.StuffID == stuff.StuffID))
                     .Select(stuff =>
                     {
                         var number = RoomHasStuff
                             .Where(hasstuff => hasstuff.StuffID == stuff.StuffID)
                             .Select(hasstuff => hasstuff.Number)
                             .FirstOrDefault();

                         stuff.Stuff_Number = number; 
                         return stuff;
                     }
            );
            foreach (var stuff in RoomStuff)
            {
                stuffList.Add(stuff);
            }
        }
    }
}
