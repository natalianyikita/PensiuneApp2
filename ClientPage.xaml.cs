using PensiuneApp2.Models;

namespace PensiuneApp2;

public partial class ClientPage : ContentPage
{
    public ClientPage()
    {
        InitializeComponent();
        // Initializam BindingContext cu un obiect Client nou
        BindingContext = new Client();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // incarcam lista de clienti din baza de date
        listView.ItemsSource = await App.Database.GetClientsAsync();
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var client = (Client)BindingContext;
        
        // Validare minima sa avem macar numele completat
        if (!string.IsNullOrEmpty(client.Nume))
        {
            await App.Database.SaveClientAsync(client);
            
            // resetam formularul
            BindingContext = new Client();
            
            // actualizam lista afisata
            listView.ItemsSource = await App.Database.GetClientsAsync();
        }
        else
        {
            await DisplayAlert("Eroare", "V? rug?m s? introduce?i numele clientului.", "OK");
        }
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var client = (Client)listView.SelectedItem;
        if (client != null)
        {
            await App.Database.DeleteClientAsync(client);
            listView.ItemsSource = await App.Database.GetClientsAsync();
        }
        else
        {
            await DisplayAlert("Aten?ie", "Selecta?i un client din list? pentru a-l ?terge.", "OK");
        }
    }
}