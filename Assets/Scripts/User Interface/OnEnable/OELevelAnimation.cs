using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OELevelAnimation : MonoBehaviour {
    private Coroutine routine;
    private int destroyed = 0;
    private int distance = 0;
    private int distanceTarget = 0;
    private int destroyedTarget = 0;
    private int totalLevelPoints = 0;


    void OnEnable() {
        if (routine != null)
            StopCoroutine(routine);
        routine = StartCoroutine(LevelAnimation());
    }

    private IEnumerator LevelAnimation() {
        destroyed = 0;
        distance = 0;
        destroyedTarget = RuntimeDataManager.instance.preRevive.levelPointsForDestroy + RuntimeDataManager.instance.postRevive.levelPointsForDestroy;
        distanceTarget = RuntimeDataManager.instance.preRevive.levelPointsForDistance + RuntimeDataManager.instance.postRevive.levelPointsForDistance;
        totalLevelPoints = SaveDataManager.getValue.currentLevelPoints;
        while (destroyed < destroyedTarget) {
            destroyed += Mathf.CeilToInt((float)destroyedTarget / 30f);
            UpdateUI();
            yield return new WaitForSecondsRealtime(.05f);
        }
        destroyed = destroyedTarget;
        while (distance < distanceTarget) {
            distance += Mathf.CeilToInt((float)distanceTarget / 30f);
            UpdateUI();
            yield return new WaitForSecondsRealtime(.05f);
        }
        distance = distanceTarget;
        UpdateUI();
        routine = null;
    }

    private void UpdateUI() {
        var pointsBeforRound = totalLevelPoints - destroyedTarget - distanceTarget;
        var poitsToWorkWith = pointsBeforRound + destroyed + distance;
        var info = GetCurrentLevel(poitsToWorkWith);
        var percent = 100;

        if (info[0] != ConstantManager.LEVEL_MAX_LEVEL) {
            var target = ConstantManager.LEVEL_POINTS[info[0]];
            var nextText = info[0] == 9 ? "MAX" : (info[0] + 1).ToString();

            percent = 100 * info[1] / target;
            percent = Mathf.Min(percent, 100);
            percent = Mathf.Max(percent, 0);

            UiObjectReferrer.instance.endgameLevelCurrentTextDE.text = info[0].ToString();
            UiObjectReferrer.instance.endgameLevelNextTextDE.text = nextText;
            UiObjectReferrer.instance.endgameLevelInfoTextDE.text = info[1].ToString() + "/" + target.ToString();
            UiObjectReferrer.instance.endgameLevelCurrentTextEN.text = info[0].ToString();
            UiObjectReferrer.instance.endgameLevelNextTextEN.text = nextText;
            UiObjectReferrer.instance.endgameLevelInfoTextEN.text = info[1].ToString() + "/" + target.ToString();
        } else {
            UiObjectReferrer.instance.endgameLevelCurrentTextDE.text = "";
            UiObjectReferrer.instance.endgameLevelNextTextDE.text = "";
            UiObjectReferrer.instance.endgameLevelInfoTextDE.text = "MAX";
            UiObjectReferrer.instance.endgameLevelCurrentTextEN.text = "";
            UiObjectReferrer.instance.endgameLevelNextTextEN.text = "";
            UiObjectReferrer.instance.endgameLevelInfoTextEN.text = "MAX";
        }
        var fullWidth = UiObjectReferrer.instance.endgameLevelBarContainerDE.rect.width;
        var contentRectTransformDE = UiObjectReferrer.instance.endgameLEvelBarContentDE;
        var contentRectTransformEN = UiObjectReferrer.instance.endgameLEvelBarContentEN;
        contentRectTransformDE.sizeDelta = new Vector2((fullWidth / 100 * percent), contentRectTransformDE.sizeDelta.y);
        contentRectTransformEN.sizeDelta = new Vector2((fullWidth / 100 * percent), contentRectTransformEN.sizeDelta.y);

        UiObjectReferrer.instance.endgameLevelParticleTextDE.text = "Partikel: +" + destroyed.ToString();
        UiObjectReferrer.instance.endgameLevelParticleTextEN.text = "Particle: +" + destroyed.ToString();
        UiObjectReferrer.instance.endgameLEvelDistanceTextDE.text = "Distanz: +" + distance.ToString();
        UiObjectReferrer.instance.endgameLEvelDistanceTextEN.text = "Distance: +" + distance.ToString();
    }
    private int[] GetCurrentLevel(int x) {
        var totalPoints = x;
        int level = 0;
        int remainingPoints = totalPoints;
        foreach (int i in ConstantManager.LEVEL_POINTS) {
            totalPoints -= i;
            if (totalPoints >= 0) {
                level++;
                remainingPoints = totalPoints;
            } else { break; }
        }
        return new int[] { level, remainingPoints };
    }
}
