namespace Plugin.Maui.Camera;

/// <summary>
/// The Camera
/// </summary>
public class CameraView : View, ICamera
{

    public static readonly BindableProperty CameraDirectionProperty = BindableProperty.Create(nameof(CameraDirection), typeof(CameraDirection), typeof(CameraView), CameraDirection.Back, propertyChanged: OnCameraDirectionChanged);
    public static readonly BindableProperty CaptureFlashProperty = BindableProperty.Create(nameof(Flash), typeof(Flash), typeof(CameraView), Flash.Off, propertyChanged: OnFlashChanged);

    public CameraDirection CameraDirection
    {
        get { return (CameraDirection)GetValue(CameraDirectionProperty); }
        set { SetValue(CameraDirectionProperty, value); }
    }

    public Flash Flash
    {
        get { return (Flash)GetValue(CaptureFlashProperty); }
        set { SetValue(CaptureFlashProperty, value); }
    }

    public async Task<byte[]> TakePhoto()
    {
        if (Handler is not CameraHandler handler)
            return [];

        return await handler.TakePhoto();
    }

    private static void OnCameraDirectionChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (oldValue == newValue || bindable is not CameraView view || view.Handler is not CameraHandler handler)
            return;

        handler.ChangeCameraDirection((CameraDirection)newValue);
    }

    private static void OnFlashChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (oldValue == newValue || bindable is not CameraView view || view.Handler is not CameraHandler handler)
            return;

        handler.SetFlash((Flash)newValue);
    }
}
