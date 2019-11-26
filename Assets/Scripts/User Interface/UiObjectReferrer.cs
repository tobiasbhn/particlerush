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
    public Canvas uiCanvas;

    [Header("INGAME")]
    public GameObject ingameMain;
    public Text ingameScoreText;
    public Text ingameGoldText;
    public RectTransform ingameSlideContainer;
    public RectTransform ingameSlideContent;
    public GameObject ingamePauseButton;

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
    public Text endgameTextReviveGoldEN;
    public Button endgameButtonReviveAdsEN;
    public Button endgameButtonReviveSkipEN;

    [Header("ENDGAME LEVEL INFO")]
    [Space(5)]
    public Text endgameLevelCurrentTextEN;
    public Text endgameLevelNextTextEN;
    public Text endgameLevelInfoTextEN;
    public Text endgameLevelParticleTextEN;
    public Text endgameLEvelDistanceTextEN;
    public RectTransform endgameLevelBarContainerEN;
    public RectTransform endgameLEvelBarContentEN;

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
    public Text endgameTextReviveGoldDE;
    public Button endgameButtonReviveAdsDE;
    public Button endgameButtonReviveSkipDE;

    [Header("ENDGAME LEVEL INFO")]
    [Space(10)]
    public Text endgameLevelCurrentTextDE;
    public Text endgameLevelNextTextDE;
    public Text endgameLevelInfoTextDE;
    public Text endgameLevelParticleTextDE;
    public Text endgameLEvelDistanceTextDE;
    public RectTransform endgameLevelBarContainerDE;
    public RectTransform endgameLEvelBarContentDE;

    [Header("Revive Bars")]
    public RectTransform endgameReviveBar1;
    public RectTransform endgameReviveBar2;
    public RectTransform endgameReviveBar3;
    public RectTransform endgameReviveBar4;




    [Header("ACCOUNT INFO")]
    [Space(30)]
    public GameObject accountInfoMain;
    public Text accountInfoGold;
    public Text accountInfoHighscore;

    [Header("LEVEL INFO")]
    [Space(10)]
    public GameObject levelInfoMain;
    public RectTransform levelInfoSliderContainer;
    public RectTransform levelInfoSliderContent;
    public Text levelCurrentText;
    public Text levelNextText;
    public Text levelInfoText;



    [Header("HIGHSCORE")]
    [Space(30)]
    public GameObject highscoreMain;
    
    [Header("HIGHSCORE EN")]
    public GameObject highscoreMainEN;
    public Text highscorePersonalScoreEN;
    public Text highscorePersonalGrowEN;
    public Text highscorePersonalShrinkEN;
    public Text highscorePersonalGoldEN;
    public Text highscorePersonalTimeEN;
    
    [Header("HIGHSCORE DE")]
    public GameObject highscoreMainDE;
    public Text highscorePersonalScoreDE;
    public Text highscorePersonalGrowDE;
    public Text highscorePersonalShrinkDE;
    public Text highscorePersonalGoldDE;
    public Text highscorePersonalTimeDE;




    [Header("SHOP")]
    [Space(30)]
    public GameObject shopLowerMain;
    public GameObject shopUpperMain;
    public ScrollRect shopScrollRect;
    public Text shopDescription;
    public Text shopLevelInfo;
    [Header("SHOP EN")]
    public GameObject shopInterfaceEN;
    public Button shopBuyButtonEN;
    public Text shopBuyTextEN;
    [Header("SHOP DE")]
    public GameObject shopInterfaceDE;
    public Button shopBuyButtonDE;
    public Text shopBuyTextDE;





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
    public Text settingsGooglePlayHeadingEN;
    public Text settingsGooglePlayTextEN;
    public Text settingsDebugTextEN;

    [Header("SETTINGS DE")]
    public GameObject settingsInterfaceDE;
    public GameObject settingsMainDE;
    [Space(5)]
    public Text settingsSoundTextDE;
    public Text settingsVibrationTextDE;
    public Text settingsGooglePlayHeadingDE;
    public Text settingsGooglePlayTextDE;
    public Text settingsDebugTextDE;

    [Header("ADD NOTIFICATION")]
    [Space(30)]
    public GameObject notificationAdsMain;
    public GameObject notificationAdsDE;
    public GameObject notificationAdsEN;

    [Header("LOGIN NOTIFICATION")]
    [Space(30)]
    public GameObject notificationLoginMain;
    public GameObject notificationLoginDE;
    public GameObject notificationLoginEN;
    public Button notificationLoginContinueBDE;
    public Button notificationLoginBackBDE;
    public Button notificationLoginContinueBEN;
    public Button notificationLoginBackBEN;
    public Text notificationLoginContinueTDE;
    public Text notificationLoginBackTDE;
    public Text notificationLoginContinueTEN;
    public Text notificationLoginBackTEN;

    [Header("LOGOUT NOTIFICATION")]
    [Space(30)]
    public GameObject notificationLogoutMain;
    public GameObject notificationLogoutDE;
    public GameObject notificationLogoutEN;
    public Button notificationLogoutContinueBDE;
    public Button notificationLogoutBackBDE;
    public Button notificationLogoutContinueBEN;
    public Button notificationLogoutBackBEN;
    public Text notificationLogoutContinueTDE;
    public Text notificationLogoutBackTDE;
    public Text notificationLogoutContinueTEN;
    public Text notificationLogoutBackTEN;

    [Header("GOOGLE RESULT NOTIFICATION")]
    [Space(30)]
    public GameObject notificationGoogleResultMain;
    public GameObject notificationGoogleResultDE;
    public GameObject notificationGoogleResultEN;
    public Text notificationGoogleResultTextDE;
    public Text notificationGoogleResultTextEN;

    [Header("STATISTICS")]
    [Space(30)]
    public GameObject statisticsMain;
    public ScrollRect statisticsScrollRect;
    public Text statisticsTextMain;
    public GameObject staticsInterfaceDE;
    public GameObject staticsInterfaceEN;

    [Header("TUTORIAL")]
    [Space(30)]
    public GameObject tutorialMainTop;
    public GameObject tutorialMainBottom;
    [Header("TUTORIAL DE")]
    public GameObject tutorialMainTopDE;
    public GameObject tutorialMainBottomDE;
    public Text tutorialHeadingDE;
    public Text tutorialStep1DE;
    public Text tutorialStep2DE;
    public Text tutorialStep3DE;
    public Text tutorialStep4DE;
    public Text tutorialStep5DE;
    public Text tutorialStep6DE;
    public Text tutorialStep7DE;
    public Button tutorialSkipDE;
    public Button tutorialNextDE;
    public Button tutorialTutorialDE;

    [Header("TUTORIAL EN")]
    public GameObject tutorialMainTopEN;
    public GameObject tutorialMainBottomEN;
    public Text tutorialHeadingEN;
    public Text tutorialStep1EN;
    public Text tutorialStep2EN;
    public Text tutorialStep3EN;
    public Text tutorialStep4EN;
    public Text tutorialStep5EN;
    public Text tutorialStep6EN;
    public Text tutorialStep7EN;
    public Button tutorialSkipEN;
    public Button tutorialNextEN;
    public Button tutorialTutorialEN;



    [Header("OWN AD")]
    [Space(30)]
    public GameObject ownAdMain;
    public GameObject ownAdDE;
    public GameObject ownAdEN;

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
            staticsInterfaceEN,
            tutorialMainTopEN,
            tutorialMainBottomEN,
            notificationLoginEN,
            notificationLogoutEN,
            notificationGoogleResultEN,
            highscoreMainEN,
            ownAdEN
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
            staticsInterfaceDE,
            tutorialMainTopDE,
            tutorialMainBottomDE,
            notificationLoginDE,
            notificationLogoutDE,
            notificationGoogleResultDE,
            highscoreMainDE,
            ownAdDE
        });
    }
}
