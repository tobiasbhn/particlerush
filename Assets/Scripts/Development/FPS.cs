using UnityEngine;
using System.Collections;

public class FPS : MonoBehaviour {

    // INSTANCE
    public static FPS instance;

    // VARS
    float deltaTime = 0.0f;
    private bool show = false;

    void Awake() {
        instance = this;
    }

    void Update() {
        if (show)
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI() {
        if (show) {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, h / 10, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 120;
            style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 0.7f);
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string frames = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            string text = " Particle Rush DEMO - " + Application.version.ToString();
            text += "\n (c) Tobias Bohn | Instagram: @tobias.bhn";

            text += "\n\n Stats:";
            text += "\n Frames: " + frames;
            text += "\n Current Mass: " + PlayerScript.instance.currentMass.ToString("0.000");
            text += "\n Difficulty: " + ScoreScript.instance.difficultFactor.ToString("0.000");
            text += "\n Last AD: " + (Time.realtimeSinceStartup - AdsManager.instance.lastAdShown).ToString("0.000");
            if (SaveDataManager.getValue != null)
                text += "\n Game Status: " + SaveDataManager.getValue.gameStatus;
            text += "\n Already Revived: " + ReviveScript.instance.alreadyRevived.ToString();

            text += "\n\n Google Play Services:";
            text += "\n Google Login: " + GoogleLoginScript.instance.isAuthenticated();
            text += "\n Google Login Status: " + GoogleLoginScript.instance.loginStatus;
            if (GoogleLoginScript.instance.isAuthenticated()) {
                text += "\n Google Name: " + GoogleLoginScript.instance.getUsername();
                text += "\n Google ID: " + GoogleLoginScript.instance.getUserID();
            }

            text += "\n\n Device:";
            text += "\n " + SystemInfo.operatingSystem;
            text += "\n " + SystemInfo.deviceModel;
            text += "\n Screen: " + Screen.height.ToString() + "/" + Screen.width.ToString() + "px";
            //text += Social.localUser.userName;
            GUI.Label(rect, text, style);
        }
    }

    public void UpdateShow() {
        var debugSetting = SaveDataManager.getValue.settingsDebug;
        var statusSetting = SaveDataManager.getValue.gameStatus;
        if ((debugSetting == SettingsDebug.ingame && statusSetting == GameStatus.ingame) || debugSetting == SettingsDebug.everywhere)
            show = true;
        else
            show = false;
    }
}