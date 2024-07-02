namespace Plugin.Maui.Camera;

// TODO: Cleanup this file when other platforms are done
#if ANDROID
public partial class CameraHandler
{
    public static PropertyMapper<CameraView, CameraHandler> CameraViewMapper = new() { };

    public static CommandMapper<CameraView, CameraHandler> CameraCommandMapper = new() { };

    public CameraHandler() : base(CameraViewMapper) { }
    public CameraHandler(PropertyMapper mapper = null) : base(mapper ?? CameraViewMapper) { }

}
#else
public partial class CameraViewHandler { }
#endif
