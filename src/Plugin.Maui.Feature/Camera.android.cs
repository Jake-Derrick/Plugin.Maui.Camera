using AndroidX.Camera.Core;
using AndroidX.Camera.Lifecycle;
using AndroidX.Camera.View;
using AndroidX.Core.Content;
using AndroidX.Lifecycle;
using Java.Interop;
using Java.Lang;
using Microsoft.Maui.Handlers;
using Plugin.Maui.Camera.Mappings;

namespace Plugin.Maui.Camera;

public partial class CameraHandler : ViewHandler<CameraView, PreviewView>, ICamera
{
    private PreviewView _previewView;
    private ProcessCameraProvider _cameraProvider;
    private AndroidX.Camera.Core.ICamera _camera;

    protected override PreviewView CreatePlatformView()
    {
        _previewView = new PreviewView(Context)
        {
            LayoutParameters = new(Android.Views.ViewGroup.LayoutParams.MatchParent, Android.Views.ViewGroup.LayoutParams.MatchParent)
        };
        return _previewView;
    }

    public async Task StartCameraPreview(CameraDirection cameraDirection = CameraDirection.Back)
    {
        var cameraProviderFuture = ProcessCameraProvider.GetInstance(Context);

        cameraProviderFuture.AddListener(new Runnable(() =>
        {
            _cameraProvider = cameraProviderFuture.Get().JavaCast<ProcessCameraProvider>();
            var preview = new Preview.Builder().Build();

            preview.SetSurfaceProvider(_previewView.SurfaceProvider);

            var cameraSelector = new CameraSelector.Builder()
                .RequireLensFacing(cameraDirection.ToAndroid())
                .Build();

            var lifecycleOwner = Context as ILifecycleOwner;

            _cameraProvider.UnbindAll();
            _camera = _cameraProvider.BindToLifecycle(lifecycleOwner, cameraSelector, preview);
        }), ContextCompat.GetMainExecutor(Context));
    }

    public async Task ChangeCameraDirection(CameraDirection direction) => await StartCameraPreview(direction);

    protected override void ConnectHandler(PreviewView nativeView)
    {
        base.ConnectHandler(nativeView);
        StartCameraPreview();
    }

    protected override void DisconnectHandler(PreviewView platformView)
    {
        base.DisconnectHandler(platformView);
    }


}