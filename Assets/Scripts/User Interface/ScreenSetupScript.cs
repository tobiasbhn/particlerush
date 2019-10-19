using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSetupScript : MonoBehaviour {

    //INSTANCES
    [HideInInspector] public static ScreenSetupScript instance;
    [HideInInspector] public bool thisScriptLoaded = false;

    //OBJECT-LINKS
    public Canvas canvasGameObject;
    public RectTransform safeAreaGameObject;
    public RectTransform largeScreenSpaceGameObject; // = Canvas-Child which goes up to maximum Point within the Safe Area
    public RectTransform smallScreenSpaceGameObject; // = Canvas-Child which goes only up to the lowest Point of the Player

    void Awake() {
        instance = this;
    }

    void Start() {
        ApplySaveArea();
        ApplyLargeScreenSpace();
        ApplySmallScreenSpace();
        thisScriptLoaded = true;
    }

    void ApplySaveArea() {
        var safeArea = Screen.safeArea;

        Vector2 min = safeArea.position;
        Vector2 max = safeArea.position + safeArea.size;

        min.x /= Screen.width;
        min.y /= Screen.height;
        max.x /= Screen.width;
        max.y /= Screen.height;

        safeAreaGameObject.anchorMin = min;
        safeAreaGameObject.anchorMax = max;
    }

    void ApplyLargeScreenSpace() {
        Vector2 min = new Vector2(ConstantManager.UI_CONTAINERS_MARGIN_LEFT, ConstantManager.UI_CONTAINERS_MARGIN_BOTTOM);
        Vector2 max = new Vector2(
            Screen.width - ConstantManager.UI_CONTAINERS_MARGIN_RIGHT,
            Screen.height - ConstantManager.UI_CONTAINERS_MARGIN_TOP
        );

        min.x /= Screen.width;
        min.y /= Screen.height;
        max.x /= Screen.width;
        max.y /= Screen.height;

        largeScreenSpaceGameObject.anchorMin = min;
        largeScreenSpaceGameObject.anchorMax = max;
    }

    void ApplySmallScreenSpace() {
        float marginBottom = Screen.safeArea.position.y;
        float marginLeft = Screen.safeArea.position.x;
        float marginRight = Screen.width - (Screen.safeArea.position.x + Screen.safeArea.size.x);

        Vector3 playerPos = Camera.main.WorldToScreenPoint(ConstantManager.PLAYER_DEFAULT_POSITION_IN_WORLD);
        float playerRadius = (float)Screen.width / (float)ConstantManager.PLAYER_MAX_MESH_GENERATION_SIZE * ((float)ConstantManager.PLAYER_MENU_START_MASS / 2f);
        float playerMinPoint = playerPos.y - playerRadius;

        float maxY = playerMinPoint - marginBottom;
        float maxX = Screen.width - marginLeft - marginRight - ConstantManager.UI_CONTAINERS_MARGIN_RIGHT;

        Vector2 min = new Vector2(ConstantManager.UI_CONTAINERS_MARGIN_LEFT, ConstantManager.UI_CONTAINERS_MARGIN_BOTTOM);
        Vector2 max = new Vector2(maxX, maxY);

        min.x /= Screen.width;
        min.y /= Screen.height;
        max.x /= Screen.width;
        max.y /= Screen.height;

        smallScreenSpaceGameObject.anchorMin = min;
        smallScreenSpaceGameObject.anchorMax = max;
    }
}
