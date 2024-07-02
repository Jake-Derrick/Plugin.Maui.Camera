namespace Plugin.Maui.Camera;

/// <summary>
/// TODO: Provide relevant comments for your APIs
/// </summary>
public interface ICamera
{
    CameraDirection CameraDirection { get; set; }

    Task<byte[]> TakePhoto();

    Flash Flash { get; set; }
}