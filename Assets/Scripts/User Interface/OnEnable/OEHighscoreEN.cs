using UnityEngine;

public class OEHighscoreEN : MonoBehaviour {
    void OnEnable() {
        UiObjectReferrer.instance.highscorePersonalScoreEN.text = "HIGHSCORE: " + ((int)SaveDataManager.getValue.highscore).ToString();
        UiObjectReferrer.instance.highscorePersonalGrowEN.text = "Destroyed negative Particles: " + SaveDataManager.getValue.highscoreRoundDataGrow.ToString();
        UiObjectReferrer.instance.highscorePersonalShrinkEN.text = "Collected positive Particles: " + SaveDataManager.getValue.highscoreRoundDataShrink.ToString();
        UiObjectReferrer.instance.highscorePersonalGoldEN.text = "Collected Gold: " + SaveDataManager.getValue.highscoreRoundDataGold.ToString();
        UiObjectReferrer.instance.highscorePersonalTimeEN.text = "Time: " + LogTime.Time(SaveDataManager.getValue.highscoreRoundDataTime);

        // UiObjectReferrer.instance.highscoreScrollRect.verticalNormalizedPosition = 1f;
        // if (GoogleLoginScript.instance.isAuthenticated()) {
        //     UiObjectReferrer.instance.highscoreGlobalNames.alignment = TextAnchor.UpperLeft;
        //     UiObjectReferrer.instance.highscoreGlobalScores.alignment = TextAnchor.UpperRight;
        //     UiObjectReferrer.instance.highscoreGlobalNames.text = "1. Tobi";
        //     UiObjectReferrer.instance.highscoreGlobalScores.text = "...simply the Best.";
        // } else {
        //     UiObjectReferrer.instance.highscoreGlobalNames.alignment = TextAnchor.UpperCenter;
        //     UiObjectReferrer.instance.highscoreGlobalNames.text = "Please sign in to\nGoogle Play Services.\n";
        //     UiObjectReferrer.instance.highscoreGlobalScores.text = "";
        // }
    }
}
