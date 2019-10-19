using UnityEngine;
using System.Collections;

public class FPS : MonoBehaviour
{
    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 120;
        style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 0.7f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string frames = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        string text = " Particle Rush DEMO | " + frames;
        text += "\n (c) Tobias Bohn(Insta @tobias.bhn) - Restricted use only. ";
        text += "\n GameStatus: " + SaveDataManager.getValue.gameStatus;
        //text += "\n\nGoogle ID: " + Social.localUser.id + "\nGoogle Name: ";
        //text += Social.localUser.userName;
        GUI.Label(rect, text, style);
    }
}