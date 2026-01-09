using PensiuneApp2.Models;

namespace PensiuneApp2;

public partial class RoomPage : ContentPage
{
    public RoomPage()
    {
        InitializeComponent();
        BindingContext = new Room();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetRoomsAsync();

        var tipuriCamere = new List<string>
    {
        "Single",
        "Dubla",
        "Tripla",
        "Apartament"
    };

        TypePicker.ItemsSource = tipuriCamere;
    }

    // CREATE / UPDATE
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var room = (Room)BindingContext;
        // Validare simpl?
        if (!string.IsNullOrEmpty(room.RoomNumber))
        {
            await App.Database.SaveRoomAsync(room); 
            BindingContext = new Room(); // Reset?m formularul
            listView.ItemsSource = await App.Database.GetRoomsAsync();
        }
        else
        {
            await DisplayAlert("Eroare", "Introduceti numarul camerei", "OK");
        }
    }

    // PRELUARE DATE PENTRU UPDATE
    async void OnRoomSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            // Popul?m editorul cu datele camerei selectate
            BindingContext = e.SelectedItem as Room;
        }
    }

    // DELETE
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var room = (Room)BindingContext;

        // Verificam daca exista un ID valid (adica daca a fost selectata o camera existenta)
        if (room.ID != 0)
        {
            await App.Database.DeleteRoomAsync(room); 
            BindingContext = new Room(); // Reset?m formularul
            listView.ItemsSource = await App.Database.GetRoomsAsync();
        }
        else
        {
            await DisplayAlert("Atentie", "Selectati o camera din lista pentru a o sterge.", "OK");
        }
    }
}