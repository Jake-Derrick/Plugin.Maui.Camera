namespace Plugin.Maui.Camera;

/// <summary>
/// TODO: Provide relevant comments for your APIs
/// </summary>
public interface ICamera
{
    void StartCamera();
    ICamera AddUsage(CameraUsage usage);

    CameraDirection CameraDirection { get; set; }

    Task<byte[]> TakePhoto();

    Flash Flash { get; set; }

    void StartVideo(string fileName);
    void PauseVideo();
    void ResumeVideo();
    void StopVideo();
    void MuteVideo(bool shouldMute);
}