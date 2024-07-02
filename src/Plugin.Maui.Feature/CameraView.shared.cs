namespace Plugin.Maui.Camera;

/// <summary>
/// The Camera
/// </summary>
public class CameraView : View
{

    public static readonly BindableProperty CameraDirectionProperty = BindableProperty.Create(nameof(CameraDirection), typeof(CameraDirection), typeof(CameraView), CameraDirection.Back, propertyChanged: OnCameraDirectionChanged);

    public CameraDirection CameraDirection
    {
        get { return (CameraDirection)GetValue(CameraDirectionProperty); }
        set { SetValue(CameraDirectionProperty, value); }
    }

    private static void OnCameraDirectionChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (oldValue != newValue && bindable is CameraView view && view.Handler is CameraHandler handler)
        {
            handler.ChangeCameraDirection((CameraDirection)newValue);
        }

    }
}
