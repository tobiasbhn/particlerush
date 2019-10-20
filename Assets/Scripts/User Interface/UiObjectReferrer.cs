using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiObjectReferrer : MonoBehaviour {
    //INSTANCE
    [HideInInspector] public static UiObjectReferrer instance;
    [HideInInspector] public bool thisScriptLoaded = false;

    //LANGUAGES
    [HideInInspector] public List<GameObject> objectsEN;
    [HideInInspector] public List<GameObject> objectsDE;

    //OBJECT-LINKS
    [Header("INGAME")]
    public GameObject ingameMain;
    public GameObject ingameScoreText;
    public GameObject ingameGoldText;
    public GameObject ingamePauseButton;

    [Header("MAIN MENU")]
    [Space(30)]
    public GameObject menuMain;
    [Header("MENU EN")]
    public GameObject menuMainEN;
    [Space(5)]
    public GameObject menuPlayButtonEN;
    public GameObject menuShopButtonEN;
    public GameObject menuLeaderboardButtonEN;
    public GameObject menuChallengeButtonEN;
    public GameObject menuSettingsButtonEN;
    [Header("MENU DE")]
    public GameObject menuMainDE;
    [Space(5)]
    public GameObject menuPlayButtonDE;
    public GameObject menuShopButtonDE;
    public GameObject menuLeaderboardButtonDE;
    public GameObject menuChallengeButtonDE;
    public GameObject menuSettingsButtonDE;




    [Header("ENDGAME")]
    [Space(30)]
    public GameObject endgameMain;
    public GameObject endgameReviveMain;

    [Header("ENDGAME EN")]
    public GameObject endgameMainEN;
    public GameObject endgameReviveMainEN;

    [Space(5)]
    public GameObject endgameGameOverTextEN;
    public GameObject endgameHighscoreTextEN;
    public GameObject endgameScoreTextEN;

    [Header("ENDGAME DE")]
    public GameObject endgameMainDE;
    public GameObject endgameReviveMainDE;

    [Space(5)]
    public GameObject endgameGameOverTextDE;
    public GameObject endgameHighscoreTextDE;
    public GameObject endgameScoreTextDE;




    [Header("PAUSE MENU")]
    [Space(30)]
    public GameObject pauseMain;
    [Header("PAUSE EN")]
    public GameObject pauseMainEN;
    [Space(5)]
    public GameObject pauseResumeButtonEN;
    public GameObject pauseMenuButtonEN;
    public GameObject pauseShopButtonEN;
    public GameObject pauseSettingsButtonEN;
    public GameObject pauseRestartButtonEN;
    [Header("PAUSE DE")]
    public GameObject pauseMainDE;
    [Space(5)]
    public GameObject pauseResumeButtonDE;
    public GameObject pauseMenuButtonDE;
    public GameObject pauseShopButtonDE;
    public GameObject pauseSettingsButtonDE;
    public GameObject pauseRestartButtonDE;

    [Header("SETTINGS MENU")]
    [Space(30)]
    public GameObject settingsMain;
    [Header("SETTINGS EN")]
    public GameObject settingsInterfaceEN;
    public GameObject settingsMainEN;
    [Space(5)]
    public GameObject settingsSoundTextEN;
    public GameObject settingsVibrationTextEN;
    public GameObject settingsItemPosTextEN;
    public GameObject settingsGooglePlayTextEN;

    [Header("SETTINGS DE")]
    public GameObject settingsInterfaceDE;
    public GameObject settingsMainDE;
    [Space(5)]
    public GameObject settingsSoundTextDE;
    public GameObject settingsVibrationTextDE;
    public GameObject settingsItemPosTextDE;
    public GameObject settingsGooglePlayTextDE;

    [Header("ADD NOTIFICATION")]
    [Space(30)]
    public GameObject notificationAdsMain;
    public GameObject notificationAdsDE;
    public GameObject notificationAdsEN;

    void Awake() {
        instance = this;
    }

    void Start() {
        DefineLanguagesLists();
        thisScriptLoaded = true;
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
            endgameReviveMainEN
        });
        objectsDE.AddRange(new List<GameObject>() {
            //MENU
            menuMainDE,
            pauseMainDE,
            settingsInterfaceDE,
            settingsMainDE,
            notificationAdsDE,
            endgameMainDE,
            endgameReviveMainDE
        });
    }
}
