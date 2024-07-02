namespace Plugin.Maui.Camera;

public interface ICameraHandler
{
    Task StartCameraPreview(CameraDirection cameraDirection = CameraDirection.Back);

    Task ChangeCameraDirection(CameraDirection direction);

    Task<byte[]> TakePhoto();

    void SetFlash(Flash flash);
}
