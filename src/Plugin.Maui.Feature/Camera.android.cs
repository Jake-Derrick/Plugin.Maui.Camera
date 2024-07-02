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

public partial class CameraHandler : ViewHandler<CameraView, PreviewView>
{
    private PreviewView _previewView;
    private ProcessCameraProvider _cameraProvider;
    private ILifecycleOwner _lifecycleOwner;
    private CameraSelector _cameraSelector;
    private AndroidX.Camera.Core.ICamera _camera;
    private ImageCapture _imageCapture;
    private Preview _preview;

    protected override PreviewView CreatePlatformView()
    {
        CreateCameraPreview();
        _lifecycleOwner = Context as ILifecycleOwner;
        StartCameraPreview();
        return _previewView;
    }

    public async Task StartCameraPreview(CameraDirection cameraDirection = CameraDirection.Back) // TODO: rename this to StartCamera
    {
        var cameraProviderFuture = ProcessCameraProvider.GetInstance(Context);

        cameraProviderFuture.AddListener(new Runnable(() =>
        {
            _cameraProvider = cameraProviderFuture.Get().JavaCast<ProcessCameraProvider>();
            _cameraSelector = new CameraSelector.Builder()
                .RequireLensFacing(cameraDirection.ToAndroid())
                .Build();

            SetupPreview();
            SetupImageCapture();

            _cameraProvider.UnbindAll();
            _camera = _cameraProvider.BindToLifecycle(_lifecycleOwner, _cameraSelector, _preview, _imageCapture);
        }), ContextCompat.GetMainExecutor(Context));
    }

    public async Task ChangeCameraDirection(CameraDirection direction) => await StartCameraPreview(direction);

    public void SetFlash(Flash flash) => _imageCapture.FlashMode = GetFlash(flash);
    private int GetFlash(Flash flash) => _camera.CameraInfo.HasFlashUnit ? flash.ToAndroid() : ImageCapture.FlashModeOff;

    protected override void ConnectHandler(PreviewView nativeView)
    {
        base.ConnectHandler(nativeView);
        SetupPreview();
    }

    protected override void DisconnectHandler(PreviewView platformView)
    {
        base.DisconnectHandler(platformView);
    }


}