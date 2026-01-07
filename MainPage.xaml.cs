namespace PensiuneApp2;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // Navigare către pagina de Rezervări
    async void OnReservationsClicked(object sender, EventArgs e)
    {
        // Animație simplă de apăsare
        var border = (Border)sender;
        await border.ScaleTo(0.95, 100);
        await border.ScaleTo(1.0, 100);

        // Navigare (folosim rutele din AppShell sau instanțiere directă)
        // Varianta sigură:
        await Navigation.PushAsync(new ReservationPage());

        // Dacă ai rute definite în AppShell, poți folosi și:
        // await Shell.Current.GoToAsync("//Rezervari");
    }

    // Navigare către pagina de Camere
    async void OnRoomsClicked(object sender, EventArgs e)
    {
        var border = (Border)sender;
        await border.ScaleTo(0.95, 100);
        await border.ScaleTo(1.0, 100);

        await Navigation.PushAsync(new RoomPage());
    }

    // Navigare către pagina de Clienți
    async void OnClientsClicked(object sender, EventArgs e)
    {
        var border = (Border)sender;
        await border.ScaleTo(0.95, 100);
        await border.ScaleTo(1.0, 100);

        await Navigation.PushAsync(new ClientPage());
    }
}