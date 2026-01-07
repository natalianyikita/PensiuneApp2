using PensiuneApp2.Models;

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

    // Aici am unit cele dou? metode OnAppearing
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // 1. Înc?rc?m camerele în Picker
        var rooms = await App.Database.GetRoomsAsync();
        RoomPicker.ItemsSource = (System.Collections.IList)rooms;
        RoomPicker.ItemDisplayBinding = new Binding("RoomNumber");

        // 2. Actualiz?m lista de rezerv?ri (CRUD - Read)
        listViewReservations.ItemsSource = await App.Database.GetReservationsAsync();
    }

    // CRUD - Create / Update
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var reservation = (Reservation)BindingContext;
        Room selectedRoom = (RoomPicker.SelectedItem as Room);

        if (selectedRoom != null)
        {
            reservation.RoomID = selectedRoom.ID;
            await App.Database.SaveReservationAsync(reservation);

            await DisplayAlert("Succes", "Rezervarea a fost salvata!", "OK");

            // Reset?m formularul pentru o nou? introducere
            BindingContext = new Reservation
            {
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(1)
            };

            // Actualiz?m lista de jos
            listViewReservations.ItemsSource = await App.Database.GetReservationsAsync();
        }
        else
        {
            await DisplayAlert("Eroare", "V? rug?m s? selecta?i o camer?.", "OK");
        }
    }

    // CRUD - Update (Preg?tire date)
    void OnReservationSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            BindingContext = e.SelectedItem as Reservation;
            // Not?: Picker-ul nu se va selecta automat doar prin BindingContext simplu,
            // dar datele text (Nume, Date) se vor completa.
        }
    }

    // CRUD - Delete
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var res = (Reservation)BindingContext;

        // Verific?m dac? rezervarea exist? în baza de date (are ID)
        if (res.ID != 0)
        {
            await App.Database.DeleteReservationAsync(res);

            // Reset?m formularul
            BindingContext = new Reservation { CheckIn = DateTime.Now, CheckOut = DateTime.Now.AddDays(1) };

            // Reîmprosp?t?m lista
            listViewReservations.ItemsSource = await App.Database.GetReservationsAsync();
        }
        else
        {
            await DisplayAlert("Eroare", "Selecta?i o rezervare din list? pentru a o ?terge.", "OK");
        }
    }
}