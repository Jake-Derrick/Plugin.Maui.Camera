namespace Plugin.Maui.Camera.Sample;

public partial class MainPage : ContentPage
{
    bool isVideoStarted = false;
    public MainPage()
    {
        InitializeComponent();
        RequestPermissions();

        Camera.AddUsage(CameraUsage.Picture)
              .AddUsage(CameraUsage.Video);
    }

    private async void RequestPermissions()
    {
        await Permissions.RequestAsync<Permissions.Camera>();
        await Permissions.RequestAsync<Permissions.Microphone>();
        await Permissions.RequestAsync<Permissions.StorageWrite>();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Camera.CameraDirection = Camera.CameraDirection == CameraDirection.Front ? CameraDirection.Back : CameraDirection.Front;
    }

    private void ChangeFlash(object sender, EventArgs e)
    {
        Camera.Flash = Camera.Flash == Flash.On ? Flash.Off : Flash.On;
    }

    private async void TakePicture(object sender, EventArgs e)
    {
        var imageBytes = await Camera.TakePhoto();
        DisplayImage.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
    }

    private async void StartVideo(object sender, EventArgs e)
    {
        if (!isVideoStarted)
            Camera.StartVideo("test-video-1");
        else
            Camera.StopVideo();

        isVideoStarted = true;
    }

}
