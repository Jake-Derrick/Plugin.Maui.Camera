namespace Plugin.Maui.Camera;

public interface ICameraHandler
{
    void StartCamera();

    void ChangeCameraDirection(CameraDirection direction);

    Task<byte[]> TakePhoto();

    void SetFlash(Flash flash);

    void StartVideo(string fileName);
    void PauseVideo();
    void ResumeVideo();
    void StopVideo();
    void MuteVideo(bool shouldMute);
}
