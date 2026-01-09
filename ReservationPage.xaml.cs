using Microsoft.Maui.ApplicationModel;
using PensiuneApp2.Models;
using Plugin.LocalNotification;
using System.Numerics;
using System.Reflection;



namespace PensiuneApp2;

public partial class ReservationPage : ContentPage
{
    public ReservationPage()
    {
        InitializeComponent();
        BindingContext = new Reservation
        {
            CheckIn = DateTime.Now,
            CheckOut = DateTime.Now.AddDays(1)
        };
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Verificam si cerem permisiunea pentru notificari 
        if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
        {
            await LocalNotificationCenter.Current.RequestNotificationPermission();
        }

        // 1. Incarcam camerele in Picker
        var rooms = await App.Database.GetRoomsAsync();
        RoomPicker.ItemsSource = (System.Collections.IList)rooms;
        RoomPicker.ItemDisplayBinding = new Binding("RoomNumber");

        var clients = await App.Database.GetClientsAsync();
        ClientPicker.ItemsSource = (System.Collections.IList)clients;
        ClientPicker.ItemDisplayBinding = new Binding("NumeComplet");

        // 2. Actualizam lista de rezervari
        listViewReservations.ItemsSource = await App.Database.GetReservationsAsync();
    }

    // CRUD - Create / Update
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var reservation = (Reservation)BindingContext;
        Room selectedRoom = (RoomPicker.SelectedItem as Room);
        Client selectedClient = (Client)ClientPicker.SelectedItem;

        // Verificam sa fie selectate ambele (Camera si Client)
        if (selectedRoom != null && selectedClient != null)
        {
            reservation.RoomID = selectedRoom.ID;
            reservation.ClientID = selectedClient.ID;

            // Salvam rezervarea in baza de date 
            await App.Database.SaveReservationAsync(reservation);

            // --- LOGICA NOTIFICARE  ---
            DateTime checkInDate = reservation.CheckIn;
            DateTime notificationTime = checkInDate.AddDays(-2);

            // CAZUL 1: Rezervarea este peste mai mult de 2 zile. Programam pentru viitor.
            if (notificationTime > DateTime.Now)
            {
                var request = new NotificationRequest
                {
                    NotificationId = 1000 + reservation.ID, // ID unic 
                    Title = "Memento Rezervare",
                    // Includem detaliile cerute: Persoana si Camera
                    Description = $"Client: {selectedClient.NumeComplet} | Camera: {selectedRoom.RoomNumber} | Incepe in 2 zile.",
                    Schedule = new NotificationRequestSchedule
                    {
                       NotifyTime = notificationTime 
                    }
                };
                await LocalNotificationCenter.Current.Show(request); 
            }
            // CAZUL 2: Rezervarea este in mai putin de 2 zile (ex: azi sau maine).
            // Folosim .Date pentru a fi siguri ca trimite notificarea chiar daca ora e apropiata.
            else if (checkInDate.Date >= DateTime.Today)
            {
                var request = new NotificationRequest
                {
                    NotificationId = 1000 + reservation.ID,
                    Title = "Rezervare Confirmata",
                    // Includem detaliile cerute: Persoana si Camera
                    Description = $"Rezervare pentru {selectedClient.NumeComplet} | Camera: {selectedRoom.RoomNumber} | Data: {checkInDate:dd/MM/yyyy}",
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddSeconds(1) // Notificare imediata [cite: 380]
                    }
                };
                await LocalNotificationCenter.Current.Show(request);
            }

            await DisplayAlert("Succes", "Rezervarea a fost salvata!", "OK");

            // Resetam formularul
            BindingContext = new Reservation
            {
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(1)
            };

            // Actualizam lista de rezervari 
            listViewReservations.ItemsSource = await App.Database.GetReservationsAsync();
        }
        else
        {
           
            await DisplayAlert("Eroare", "Va rugam sa selectati o camera si un client din lista.", "OK");
        }
    }

    // CRUD - Update 
    void OnReservationSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            BindingContext = e.SelectedItem as Reservation;
        }
    }

    // CRUD - Delete
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var res = (Reservation)BindingContext;

        // Verificam daca rezervarea exista in baza de date 
        if (res.ID != 0)
        {
            await App.Database.DeleteReservationAsync(res);

            // Resetam formularul
            BindingContext = new Reservation { CheckIn = DateTime.Now, CheckOut = DateTime.Now.AddDays(1) };

            // Refresh la lista
            listViewReservations.ItemsSource = await App.Database.GetReservationsAsync();
        }
        else
        {
            await DisplayAlert("Eroare", "Selectati o rezervare din lista pentru a o sterge.", "OK");
        }
    }
}