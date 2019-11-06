using UnityEngine;

public static class VibrationManager {

    // SETUP VIBRATOR
#if UNITY_ANDROID && !UNITY_EDITOR
    private static readonly AndroidJavaObject Vibrator =
    new AndroidJavaClass("com.unity3d.player.UnityPlayer")// Get the Unity Player.
    .GetStatic<AndroidJavaObject>("currentActivity")// Get the Current Activity from the Unity Player.
    .Call<AndroidJavaObject>("getSystemService", "vibrator");// Then get the Vibration Service from the Current Activity.
#else
    private static readonly AndroidJavaObject Vibrator = null;
#endif

    // VARS
    private static long duration = 150;

    public static void Setup() {
        switch (SaveDataManager.getValue.settingsVibration) {
            case SettingsVibration.Off:
                duration = 0;
                break;
            case SettingsVibration.Short:
                duration = 75;
                break;
            case SettingsVibration.Medium:
                duration = 150;
                break;
            case SettingsVibration.Long:
                duration = 250;
                break;
        }
    }

    public static void Vibrate() {
        if (Vibrator != null && duration != 0)
            Vibrator.Call("vibrate", duration);
        else if (duration != 0)
            DebugVibration();

    }

    public static void DebugVibration() {
        if (duration == 1)
            Handheld.Vibrate();
    }
}