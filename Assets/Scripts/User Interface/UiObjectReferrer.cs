using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiObjectReferrer : MonoBehaviour {
    //INSTANCE
    [HideInInspector] public static UiObjectReferrer instance;

    //LANGUAGES
    [HideInInspector] public List<GameObject> objectsEN;
    [HideInInspector] public List<GameObject> objectsDE;

    //OBJECT-LINKS
    [Header("INGAME")]
    public GameObject ingameMain;
    public Text ingameScoreText;
    public Text ingameGoldText;
    public RectTransform ingameSlideContainer;
    public RectTransform ingameSlideContent;
    public GameObject ingameItemSlotsLeft;
    public GameObject ingameItemSlotsLeft1;
    public GameObject ingameItemSlotsLeft2;
    public GameObject ingameItemSlotsLeft3;
    public GameObject ingameItemSlotsRight;
    public GameObject ingameItemSlotsRight1;
    public GameObject ingameItemSlotsRight2;
    public GameObject ingameItemSlotsRight3;

    [Header("MAIN MENU")]
    [Space(30)]
    public GameObject menuMain;
    public GameObject menuMainEN;
    public GameObject menuMainDE;




    [Header("ENDGAME")]
    [Space(30)]
    public GameObject endgameMain;

    [Header("ENDGAME EN")]
    public GameObject endgameMainEN;

    [Space(5)]
    public Text endgameGameOverTextEN;
    public Text endgameHighscoreTextEN;
    public Text endgameScoreTextEN;
    public GameObject endgameReviveEN;
    public GameObject endgameLevelEN;
    [Space(5)]
    public Button endgameButtonRestartEN;
    public Button endgameButtonShopEN;
    public Button endgameButtonMenuEN;
    public Button endgameButtonSettingsEN;
    public Button endgameButtonReviveGoldEN;
    public Button endgameButtonReviveAdsEN;
    public Button endgameButtonReviveSkipEN;

    [Header("ENDGAME DE")]
    public GameObject endgameMainDE;

    [Space(5)]
    public Text endgameGameOverTextDE;
    public Text endgameHighscoreTextDE;
    public Text endgameScoreTextDE;
    public GameObject endgameReviveDE;
    public GameObject endgameLevelDE;
    [Space(5)]
    public Button endgameButtonRestartDE;
    public Button endgameButtonShopDE;
    public Button endgameButtonMenuDE;
    public Button endgameButtonSettingsde;
    public Button endgameButtonReviveGoldDE;
    public Button endgameButtonReviveAdsDE;
    public Button endgameButtonReviveSkipDE;



    [Header("SHOP")]
    [Space(30)]
    public GameObject shopLowerMain;
    public GameObject shopUpperMain;
    [Header("SHOP EN")]
    public GameObject shopInterfaceEN;
    public Text shopWelcomeTextEN;
    public Button shopBuyButtonEN;
    public Button shopUnlockButtonEN;
    [Header("SHOP DE")]
    public GameObject shopInterfaceDE;
    public Text shopWelcomeTextDE;
    public Button shopBuyButtonDE;
    public Button shopUnlockButtonDE;
    [Header("SHOP ITEMS")]
    public List<ItemShopPrefab> shopItems;





    [Header("PAUSE MENU")]
    [Space(30)]
    public GameObject pauseMain;
    public GameObject pauseMainEN;
    public GameObject pauseMainDE;





    [Header("SETTINGS MENU")]
    [Space(30)]
    public GameObject settingsMain;
    public GameObject openSourceMain;
    public ScrollRect settingsScrollcontainer;
    [Header("SETTINGS EN")]
    public GameObject settingsInterfaceEN;
    public GameObject settingsMainEN;
    [Space(5)]
    public Text settingsSoundTextEN;
    public Text settingsVibrationTextEN;
    public Text settingsItemPosTextEN;
    public Text settingsGooglePlayTextEN;
    public Text settingsDebugTextEN;

    [Header("SETTINGS DE")]
    public GameObject settingsInterfaceDE;
    public GameObject settingsMainDE;
    [Space(5)]
    public Text settingsSoundTextDE;
    public Text settingsVibrationTextDE;
    public Text settingsItemPosTextDE;
    public Text settingsGooglePlayTextDE;
    public Text settingsDebugTextDE;

    [Header("ADD NOTIFICATION")]
    [Space(30)]
    public GameObject notificationAdsMain;
    public GameObject notificationAdsDE;
    public GameObject notificationAdsEN;

    [Header("STATISTICS")]
    [Space(30)]
    public GameObject statisticsMain;
    public Text statisticsTextMain;
    public GameObject staticsInterfaceDE;
    public GameObject staticsInterfaceEN;

    void Awake() {
        instance = this;
    }
    void Start() {
        DefineLanguagesLists();
    }

    private void DefineLanguagesLists() {
        objectsEN.AddRange(new List<GameObject>() {
            //MENU
            menuMainEN,
            pauseMainEN,
            settingsInterfaceEN,
            settingsMainEN,
            notificationAdsEN,
            endgameMainEN,
            shopInterfaceEN,
            staticsInterfaceEN
        });
        objectsDE.AddRange(new List<GameObject>() {
            //MENU
            menuMainDE,
            pauseMainDE,
            settingsInterfaceDE,
            settingsMainDE,
            notificationAdsDE,
            endgameMainDE,
            shopInterfaceDE,
            staticsInterfaceDE
        });
    }
}
