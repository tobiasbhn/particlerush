using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    //INSTANCE
    [HideInInspector] public static ScoreScript instance;

    //BEHAVIOUR
    private bool countScore = false;
    [HideInInspector] public float currentScore = 0;

    void Awake() {
        instance = this;
    }

    void Update() {
        if (countScore) {
            currentScore += ConstantManager.SCORE_PER_SECOND * Time.deltaTime;
            RuntimeDataManager.value.score = currentScore;
            var currentHighscore = Mathf.Max(SaveDataManager.getValue.highscore, RuntimeDataManager.preRevive.score + RuntimeDataManager.postRevive.score);
            RuntimeDataManager.value.highscore = currentHighscore;
            var gold = (RuntimeDataManager.postRevive.goldMassCollected + RuntimeDataManager.preRevive.goldMassCollected).ToString();
            var score = ((int)RuntimeDataManager.postRevive.score + (int)RuntimeDataManager.preRevive.score).ToString("000000");
            UiObjectReferrer.instance.ingameScoreText.GetComponent<Text>().text = score;
            UiObjectReferrer.instance.ingameGoldText.GetComponent<Text>().text = gold;
        }
    }
    public void IncreaseScoreParticle() {
        if (countScore) {
            currentScore += ConstantManager.SCORE_PER_PARTICLE;
        }
    }

    public void SetupIngame() {
        countScore = true;
        currentScore = 0;
    }

    public void SetupDisabled() {
        countScore = false;
    }

    public void SetupActive() {
        countScore = true;
    }
}
