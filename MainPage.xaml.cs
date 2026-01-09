namespace PensiuneApp2;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // Navigare catre pagina de Rezervari
    async void OnReservationsClicked(object sender, EventArgs e)
    {
        // Animatie simplă de apasare
        var border = (Border)sender;
        await border.ScaleTo(0.95, 100);
        await border.ScaleTo(1.0, 100);


        await Navigation.PushAsync(new ReservationPage());


    }

    // Navigare catre pagina de Camere
    async void OnRoomsClicked(object sender, EventArgs e)
    {
        var border = (Border)sender;
        await border.ScaleTo(0.95, 100);
        await border.ScaleTo(1.0, 100);

        await Navigation.PushAsync(new RoomPage());
    }

    // Navigare catre pagina de Clienti
    async void OnClientsClicked(object sender, EventArgs e)
    {
        var border = (Border)sender;
        await border.ScaleTo(0.95, 100);
        await border.ScaleTo(1.0, 100);

        await Navigation.PushAsync(new ClientPage());
    }
}