
namespace Plugin.Maui.Camera;

public partial class CameraHandler : ICamera
{
    public Task ChangeCameraDirection(CameraDirection direction)
    {
        throw new NotImplementedException();
    }

    // TODO Implement your .NET specific code.
    // This usually is a placeholder as .NET MAUI apps typically don't run on .NET generic targets unless through unit tests and such
    public Task StartCameraPreview(CameraDirection cameraDirection = CameraDirection.Back)
    {
        throw new NotImplementedException();
    }
}