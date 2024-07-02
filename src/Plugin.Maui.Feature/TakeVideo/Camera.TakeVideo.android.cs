using Android.Content;
using Android.Provider;
using AndroidX.Camera.Video;
using AndroidX.Core.Util;

namespace Plugin.Maui.Camera;

// This file is for Android Video Capture
public partial class CameraHandler
{
    private Recorder _recorder;
    private Recording _recording;

    public void SetupVideoCapture()
    {
        var qualitySelector = QualitySelector.From(Quality.Highest); // TODO: Actual add quality selection
        _recorder = new Recorder.Builder()
            .SetExecutor(_mainExecutor)
            .SetQualitySelector(qualitySelector)
            .Build();

        _videoCapture = VideoCapture.WithOutput(_recorder); //TODO: Mirror options
    }

    public void StartVideo(string fileName)
    {
        var contentValues = new ContentValues();
        contentValues.Put(MediaStore.Video.Media.InterfaceConsts.DisplayName, $"{fileName}.mp4");

        var mediaStore = new MediaStoreOutputOptions.Builder(Context.ContentResolver, MediaStore.Video.Media.ExternalContentUri)
            .SetContentValues(contentValues)
            .Build();

        //var recording = _videoCapture.Output as Recorder; // Maybe this instead of _recorder
        var pendingRecording = _recorder
            .PrepareRecording(Context, mediaStore)
            .WithAudioEnabled();

        _recording = pendingRecording.Start(_mainExecutor, new VideoConsumer());
    }

    public void PauseVideo() => _recording?.Pause();
    public void ResumeVideo() => _recording?.Resume();
    public void StopVideo() => _recording?.Stop();
    public void MuteVideo(bool shouldMute) => _recording?.Mute(shouldMute);



    public class VideoConsumer() : Java.Lang.Object, IConsumer
    {

        public void Accept(Java.Lang.Object? obj)
        {
            if (obj is null)
                return;

            var videoRecordEvent = obj as VideoRecordEvent;

            // Any use to tapping into these events?
            //if (videoRecordEvent is VideoRecordEvent.Start) //Start, Resume, Pause, Status (statistics)

            if (videoRecordEvent is VideoRecordEvent.Finalize final) // Used for the recording result and includes information such as the URI of the final file along with any related errors.
            {
                // Note sure what I want to return here yet
                return;
            }
        }
    }
}
