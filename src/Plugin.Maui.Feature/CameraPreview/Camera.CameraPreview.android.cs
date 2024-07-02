using AndroidX.Camera.Core;
using AndroidX.Camera.View;

namespace Plugin.Maui.Camera;

// This file handles the camera preview
public partial class CameraHandler
{
    private void CreateCameraPreview()
    {
        _previewView = new PreviewView(Context)
        {
            LayoutParameters = new(Android.Views.ViewGroup.LayoutParams.MatchParent, Android.Views.ViewGroup.LayoutParams.MatchParent)
        };
    }

    private void SetupPreview()
    {
        _preview = new Preview.Builder().Build();
        _preview.SetSurfaceProvider(_previewView.SurfaceProvider);
    }
}
