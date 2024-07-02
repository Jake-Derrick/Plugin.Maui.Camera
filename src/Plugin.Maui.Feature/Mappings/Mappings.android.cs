using AndroidX.Camera.Core;

namespace Plugin.Maui.Camera.Mappings;

/// <summary>
/// Provides mappings between shared objects and Android specific implementations.
/// </summary>
internal static class Mappings
{
    internal static int ToAndroid(this CameraDirection direction) => direction switch
    {
        CameraDirection.Back => CameraSelector.LensFacingBack,
        CameraDirection.Front => CameraSelector.LensFacingFront,
        _ => CameraSelector.LensFacingUnknown
    };
}