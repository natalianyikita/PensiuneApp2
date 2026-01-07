using PensiuneApp2.Models;

namespace PensiuneApp2;

public partial class ClientPage : ContentPage
{
    public ClientPage()
    {
        InitializeComponent();
        BindingContext = new Client();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetClientsAsync();
    }

    // CREATE / UPDATE
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var client = (Client)BindingContext;

        if (!string.IsNullOrEmpty(client.Nume))
        {
            await App.Database.SaveClientAsync(client);
            BindingContext = new Client(); // Resetare formular
            listView.ItemsSource = await App.Database.GetClientsAsync(); // Refresh lista
        }
        else
        {
            await DisplayAlert("Eroare", "Va rugam sa introduceti numele clientului.", "OK");
        }
    }

    // READ (Populare formular la selectare)
    async void OnClientSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            BindingContext = e.SelectedItem as Client;
        }
    }

    // DELETE
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var client = (Client)BindingContext;

        // Verificam daca clientul are un ID valid 
        if (client.ID != 0)
        {
            await App.Database.DeleteClientAsync(client);
            BindingContext = new Client(); // Resetare formular
            listView.ItemsSource = await App.Database.GetClientsAsync(); // Refresh lista
        }
        else
        {
            await DisplayAlert("Atentie", "Selectati un client din lista pentru a-l sterge.", "OK");
        }
    }
}