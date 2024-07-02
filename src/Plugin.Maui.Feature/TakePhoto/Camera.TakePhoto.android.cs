using AndroidX.Camera.Core;
using AndroidX.Core.Content;
using Plugin.Maui.Camera.Mappings;
using static AndroidX.Camera.Core.ImageCapture;

namespace Plugin.Maui.Camera;

// This file handles picture capturing
public partial class CameraHandler
{
    /// <inheritdoc />
    public async Task<byte[]> TakePhoto()
    {
        var tcs = new TaskCompletionSource<byte[]>();
        _imageCapture.TargetRotation = (int)_previewView.Display.Rotation; // Might need some work here to get proper rotation when app/device is portrait locked. _camera.CameraInfo.GetSensorRotationDegrees()?
        _imageCapture.TakePicture(ContextCompat.GetMainExecutor(Context), new ImageCaptureCallback(tcs.SetResult));
        return await tcs.Task;
    }

    private void SetupImageCapture()
    {
        _imageCapture = new ImageCapture.Builder()
            .SetTargetRotation((int)_previewView.Display.Rotation)
            .SetFlashMode(VirtualView.Flash.ToAndroid())
            .Build();
    }

    private class ImageCaptureCallback(Action<byte[]> onImageCaptured) : OnImageCapturedCallback
    {
        private readonly Action<byte[]> _onImageCaptured = onImageCaptured;

        public override void OnCaptureSuccess(IImageProxy image)
        {
            using var buffer = image.GetPlanes()[0].Buffer;
            byte[] bytes = new byte[buffer.Remaining()];
            buffer.Get(bytes);

            _onImageCaptured?.Invoke(bytes);
            image.Close();
        }

        public override void OnError(ImageCaptureException exception)
        {
            // Handle the error here
            Console.WriteLine("Image capture failed: " + exception.Message);
        }
    }
}
