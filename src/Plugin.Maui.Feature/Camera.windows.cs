
namespace Plugin.Maui.Camera;

public partial class CameraHandler : ICamera
{
    public Task ChangeCameraDirection(CameraDirection direction)
    {
        throw new NotImplementedException();
    }

    // TODO Implement your Windows specific code
    public Task StartCameraPreview(CameraDirection cameraDirection = CameraDirection.Back)
    {
        throw new NotImplementedException();
    }
}