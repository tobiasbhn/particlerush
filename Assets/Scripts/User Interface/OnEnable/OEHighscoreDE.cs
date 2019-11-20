using UnityEngine;

public class OEHighscoreDE : MonoBehaviour {
    void OnEnable() {
        UiObjectReferrer.instance.highscorePersonalScoreDE.text = "HIGHSCORE: " + ((int)SaveDataManager.getValue.highscore).ToString();
        UiObjectReferrer.instance.highscorePersonalGrowDE.text = "Zerstörte negative Partikel: " + SaveDataManager.getValue.highscoreRoundDataGrow.ToString();
        UiObjectReferrer.instance.highscorePersonalShrinkDE.text = "Gesammelte positive Partikel: " + SaveDataManager.getValue.highscoreRoundDataShrink.ToString();
        UiObjectReferrer.instance.highscorePersonalGoldDE.text = "Gesammeltes Gold: " + SaveDataManager.getValue.highscoreRoundDataGold.ToString();
        UiObjectReferrer.instance.highscorePersonalTimeDE.text = "Zeit: " + LogTime.Time(SaveDataManager.getValue.highscoreRoundDataTime);

        // UiObjectReferrer.instance.highscoreScrollRect.verticalNormalizedPosition = 1f;
        // if (GoogleLoginScript.instance.isAuthenticated()) {
        //     UiObjectReferrer.instance.highscoreGlobalNames.alignment = TextAnchor.UpperLeft;
        //     UiObjectReferrer.instance.highscoreGlobalScores.alignment = TextAnchor.UpperRight;
        //     UiObjectReferrer.instance.highscoreGlobalNames.text = "1. Tobi";
        //     UiObjectReferrer.instance.highscoreGlobalScores.text = "...einfach der Beste";
        // } else {
        //     UiObjectReferrer.instance.highscoreGlobalNames.alignment = TextAnchor.UpperCenter;
        //     UiObjectReferrer.instance.highscoreGlobalNames.text = "Bitte melde dich bei\nGoogle Play Services an.\n";
        //     UiObjectReferrer.instance.highscoreGlobalScores.text = "";
        // }
    }
}
