namespace Plugin.Maui.Camera;

public class CameraUsageConfiguration
{
    public bool UsePicture { get; private set; }
    public bool UseVideo { get; private set; }

    public CameraUsageConfiguration AddUsage(CameraUsage usage)
    {
        _ = usage switch
        {
            CameraUsage.Picture => UsePicture = true,
            CameraUsage.Video => UseVideo = true,
            _ => throw new ArgumentOutOfRangeException(nameof(usage), usage, null)
        };

        return this;
    }
}
