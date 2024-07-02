namespace Plugin.Maui.Camera.Sample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        RequestPermissions();
    }

    private async void RequestPermissions()
    {
        await Permissions.RequestAsync<Permissions.Camera>();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        CameraPreview.CameraDirection = CameraPreview.CameraDirection == CameraDirection.Front ? CameraDirection.Back : CameraDirection.Front;
    }
}
