namespace Plugin.Maui.Camera;

/// <summary>
/// TODO: Provide relevant comments for your APIs
/// </summary>
public interface ICamera
{
    /// <summary>
    /// Starts the camera
    /// </summary>
    Task StartCameraPreview(CameraDirection cameraDirection = CameraDirection.Back);

    /// <summary>
    /// Changes the camera direction
    /// </summary>
    Task ChangeCameraDirection(CameraDirection direction);
}