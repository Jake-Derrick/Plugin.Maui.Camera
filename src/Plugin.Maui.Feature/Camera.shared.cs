

namespace Plugin.Maui.Camera;

// TODO: Cleanup this file when other platforms are done
#if ANDROID
public partial class CameraHandler : ICameraHandler
{
    public static PropertyMapper<CameraView, CameraHandler> CameraViewMapper = new() { };

    public static CommandMapper<CameraView, CameraHandler> CameraCommandMapper = new() { };

    public CameraHandler() : base(CameraViewMapper) { }
    public CameraHandler(PropertyMapper mapper = null) : base(mapper ?? CameraViewMapper) { }

}
#else
public partial class CameraHandler : ICameraHandler
{
    public Task ChangeCameraDirection(CameraDirection direction)
    {
        throw new NotImplementedException();
    }

    public void MuteVideo(bool shouldMute)
    {
        throw new NotImplementedException();
    }

    public void PauseVideo()
    {
        throw new NotImplementedException();
    }

    public void ResumeVideo()
    {
        throw new NotImplementedException();
    }

    public void SetFlash(Flash flash)
    {
        throw new NotImplementedException();
    }

    public void StartCamera()
    {
        throw new NotImplementedException();
    }

    public void StartVideo(string fileName)
    {
        throw new NotImplementedException();
    }

    public void StopVideo()
    {
        throw new NotImplementedException();
    }

    public Task<byte[]> TakePhoto()
    {
        throw new NotImplementedException();
    }

    void ICameraHandler.ChangeCameraDirection(CameraDirection direction)
    {
        throw new NotImplementedException();
    }
}
#endif
