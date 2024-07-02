using AndroidX.Camera.Core;
using AndroidX.Camera.Lifecycle;
using AndroidX.Camera.Video;
using AndroidX.Camera.View;
using AndroidX.Core.Content;
using AndroidX.Lifecycle;
using Java.Interop;
using Java.Lang;
using Java.Util.Concurrent;
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
    private IExecutor _mainExecutor;

    // Camera use cases
    private Preview _preview;
    private ImageCapture _imageCapture;
    private VideoCapture _videoCapture;

    protected override PreviewView CreatePlatformView()
    {
        CreateCameraPreview();
        _lifecycleOwner = Context as ILifecycleOwner;
        _mainExecutor = ContextCompat.GetMainExecutor(Context);
        StartCamera();
        return _previewView;
    }

    public void StartCamera()
    {
        var cameraProviderFuture = ProcessCameraProvider.GetInstance(Context);

        cameraProviderFuture.AddListener(new Runnable(() =>
        {
            _cameraProvider = cameraProviderFuture.Get().JavaCast<ProcessCameraProvider>();
            _cameraSelector = new CameraSelector.Builder()
                .RequireLensFacing(VirtualView.CameraDirection.ToAndroid())
                .Build();

            SetupPreview();
            List<UseCase> useCases = [_preview];

            if (VirtualView.UsageConfiguration.UsePicture)
            {
                SetupImageCapture();
                useCases.Add(_imageCapture);
            }

            if (VirtualView.UsageConfiguration.UseVideo)
            {
                SetupVideoCapture();
                useCases.Add(_videoCapture);
            }

            _cameraProvider.UnbindAll();
            _camera = _cameraProvider.BindToLifecycle(_lifecycleOwner, _cameraSelector, useCases.ToArray());
        }), _mainExecutor);
    }

    public void ChangeCameraDirection(CameraDirection direction) => StartCamera();

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