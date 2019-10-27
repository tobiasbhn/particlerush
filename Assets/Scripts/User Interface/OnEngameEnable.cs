﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnEngameEnable : MonoBehaviour {

    void OnEnable() {
        var score = "Score: " + ((int)RuntimeDataManager.preRevive.score + (int)RuntimeDataManager.postRevive.score).ToString("000000");
        UiObjectReferrer.instance.endgameScoreTextDE.GetComponent<Text>().text = score;
        UiObjectReferrer.instance.endgameScoreTextEN.GetComponent<Text>().text = score;
        var highscore = "Highscore: " + ((int)RuntimeDataManager.value.highscore).ToString("000000");
        UiObjectReferrer.instance.endgameHighscoreTextDE.GetComponent<Text>().text = highscore;
        UiObjectReferrer.instance.endgameHighscoreTextEN.GetComponent<Text>().text = highscore;
        if (score == highscore) {
            UiObjectReferrer.instance.endgameGameOverTextEN.GetComponent<Text>().text = "NEW HIGHSCORE!";
            UiObjectReferrer.instance.endgameGameOverTextDE.GetComponent<Text>().text = "NEUER HIGHSCORE!";
        } else {
            UiObjectReferrer.instance.endgameGameOverTextEN.GetComponent<Text>().text = "GAME OVER";
            UiObjectReferrer.instance.endgameGameOverTextDE.GetComponent<Text>().text = "SPIELENDE";
        }

        if (ReviveScript.instance.alreadyRevived == false) {
            ReviveScript.instance.SetupRevive();
        } else {
            ReviveScript.instance.SetupLevel();
        }
    }
}