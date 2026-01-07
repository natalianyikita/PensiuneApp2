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

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var rooms = await App.Database.GetRoomsAsync();
        RoomPicker.ItemsSource = (System.Collections.IList)rooms;
        RoomPicker.ItemDisplayBinding = new Binding("RoomNumber");
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var reservation = (Reservation)BindingContext;

        Room selectedRoom = (RoomPicker.SelectedItem as Room);

        if (selectedRoom != null)
        {
            reservation.RoomID = selectedRoom.ID;
            await App.Database.SaveReservationAsync(reservation);

            await DisplayAlert("Succes", "Rezervarea a fost salvat?!", "OK");
            await Navigation.PopAsync(); 
        }
        else
        {
            await DisplayAlert("Eroare", "Va rugam sa selectati o camera.", "OK");
        }
    }
}