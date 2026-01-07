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
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var room = (Room)BindingContext;
        if (!string.IsNullOrEmpty(room.RoomNumber))
        {
            await App.Database.SaveRoomAsync(room);
            BindingContext = new Room();
            listView.ItemsSource = await App.Database.GetRoomsAsync();
        }
    }


    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var room = listView.SelectedItem as Room;
        if (room != null)
        {
            await App.Database.DeleteRoomAsync(room);
            listView.ItemsSource = await App.Database.GetRoomsAsync();
        }
    }

}