using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    //INSTANCE
    [HideInInspector] public static ScoreScript instance;

    //BEHAVIOUR
    private bool countScore = false;
    private float startTime = 0f;
    [HideInInspector] public float currentScore = 0;
    [HideInInspector] public float difficultFactor = 0;

    // VARS
    [HideInInspector] public int gold;
    private float increaseDifficultiAfterRevive = 0;
    private float previousDifficultiFactor = 0;

    void Awake() {
        instance = this;
    }

    void Update() {
        if (countScore) {
            // Calculate Score
            currentScore += ConstantManager.SCORE_PER_SECOND * Time.deltaTime;
            RuntimeDataManager.value.score = currentScore;
            // Calculate Highscore
            var currentHighscore = Mathf.Max(SaveDataManager.getValue.highscore, RuntimeDataManager.instance.preRevive.score + RuntimeDataManager.instance.postRevive.score);
            RuntimeDataManager.value.highscore = currentHighscore;
            // Update UI
            var goldStr = gold.ToString();
            var score = ((int)RuntimeDataManager.instance.postRevive.score + (int)RuntimeDataManager.instance.preRevive.score).ToString("000000");
            UiObjectReferrer.instance.ingameScoreText.text = score;
            UiObjectReferrer.instance.ingameGoldText.text = goldStr;

            // Calculate Difficult Factor
            CalculateDifficultFactor();
        }
    }
    public void IncreaseScoreParticle() {
        if (countScore) {
            currentScore += ConstantManager.SCORE_PER_PARTICLE;
        }
    }


    private void CalculateDifficultFactor() {
        // Some Function From Excel
        // (100 * ((((MAX_LVL + LVL_BUFFER) - CURR_LVL) / DES_TIME * CURR_TIME) + CURR_LVL) / (MAX_LVL + LVL_BUFFER)) / 100
        int absMax = ConstantManager.SCORE_DIFFICULTY_MAX_LVL + ConstantManager.SCORE_DIFFICULTY_LVL_BUFFER;
        int lvlTillAbsMax = absMax - SaveDataManager.getValue.currentLevel;
        float dependOnTime = (float)lvlTillAbsMax / (float)ConstantManager.SCORE_DIFFICULTY_DESIRED_ROUND_TIME * (Time.time - startTime);
        float shiftWithLVL = dependOnTime + SaveDataManager.getValue.currentLevel;
        float inPercent = 100f * shiftWithLVL / (float)absMax;

        RuntimeDataManager.value.difficultyFactor = inPercent / 100f;

        if (previousDifficultiFactor != 0)
            increaseDifficultiAfterRevive += previousDifficultiFactor / (ConstantManager.SCORE_DIFFICULTY_TIME_TO_SOFT_INTRODUCE_AFTER_REVIVE / Time.deltaTime);
        else
            increaseDifficultiAfterRevive = RuntimeDataManager.instance.preRevive.difficultyFactor;
        difficultFactor = increaseDifficultiAfterRevive + RuntimeDataManager.instance.postRevive.difficultyFactor;
        difficultFactor = Mathf.Min(1f, difficultFactor);
        difficultFactor = Mathf.Max(0f, difficultFactor);
    }

    public float GetParticleSpawnFactor() {
        if (SaveDataManager.getValue.gameStatus == GameStatus.ingame) {
            float spawnManipulation = ConstantManager.PARTICLE_SPAWN_DELAY_INGAME_MAX - ConstantManager.PARTICLE_SPAWN_DELAY_INGAME_MIN;
            spawnManipulation *= difficultFactor;
            return spawnManipulation;
        } else {
            return 0f;
        }
    }
    public float GetParticleSpeedFactor() {
        if (SaveDataManager.getValue.gameStatus == GameStatus.ingame) {
            float speedManipulation = ConstantManager.PARTICLE_SPAWN_SPEED_INGAME_MAX - ConstantManager.PARTICLE_SPAWN_SPEED_INGAME_MIN;
            speedManipulation *= difficultFactor;
            return speedManipulation;
        } else {
            return 0f;
        }
    }


    public void SetupIngame() {
        countScore = true;
        startTime = Time.time;
        currentScore = 0;
        increaseDifficultiAfterRevive = 0;
        previousDifficultiFactor = RuntimeDataManager.instance.preRevive.difficultyFactor;
        gold = RuntimeDataManager.instance.preRevive.goldMassCollected;
    }

    public void SetupDisabled() {
        countScore = false;
    }

    public void SetupActive() {
        countScore = true;
    }
}
