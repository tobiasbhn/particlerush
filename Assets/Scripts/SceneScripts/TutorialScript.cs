using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour {

    // INSTANCE
    public static TutorialScript instance;

    // VARS
    public int currentTutorialProgress = 0;

    void Awake() {
        instance = this;
    }

    public void StartTutorial() {
        currentTutorialProgress = 0;
        DisplayCurrentTutorialProgress();
    }
    public void NextTutorialStep() {
        currentTutorialProgress++;
        DisplayCurrentTutorialProgress();
    }
    private void DisplayCurrentTutorialProgress() {
        switch (currentTutorialProgress) {
            case 0: // Welcome to PArticle Rush
                DisableAllStats();
                UiObjectReferrer.instance.tutorialStep1DE.gameObject.SetActive(true);
                UiObjectReferrer.instance.tutorialStep1EN.gameObject.SetActive(true);
                break;
            case 1: // You are this beautiful waterdrop, that glides through endless space.
                DisableAllStats();
                UiObjectReferrer.instance.tutorialStep2DE.gameObject.SetActive(true);
                UiObjectReferrer.instance.tutorialStep2EN.gameObject.SetActive(true);
                PlayerSceneSetup.instance.SetupMenu();
                break;
            case 2: // But you have to be careful. There are other particles flying around that could hit you.
                DisableAllStats();
                UiObjectReferrer.instance.tutorialStep3DE.gameObject.SetActive(true);
                UiObjectReferrer.instance.tutorialStep3EN.gameObject.SetActive(true);
                ParticleSpawnScript.instance.spawnModi = ParticleSpawnModi.onlyNorm;
                ParticleSceneSetup.instance.SetupMenu();
                break;
            case 3: // When you collide with a particle, you unite. If you get too big, you lose your inner stability and burst.
                DisableAllStats();
                UiObjectReferrer.instance.tutorialStep4DE.gameObject.SetActive(true);
                UiObjectReferrer.instance.tutorialStep4EN.gameObject.SetActive(true);
                PlayerScript.instance.playerAllowGrow = true;
                PlayerScript.instance.playerAllowShrink = true;
                break;
            case 4: // So you should try to protect yourself from the particles. Try to destroy the particles.
                DisableAllStats();
                UiObjectReferrer.instance.tutorialStep5DE.gameObject.SetActive(true);
                UiObjectReferrer.instance.tutorialStep5EN.gameObject.SetActive(true);
                PlayerScript.instance.SetTargetMass(ConstantManager.PLAYER_INGAME_START_MASS, true);
                PlayerMovementScript.instance.allowTab = true;
                break;
            case 5: // The bigger you get, the more damage you do to the particles. If it gets tight, you can swip to the left or right.
                DisableAllStats();
                UiObjectReferrer.instance.tutorialStep6DE.gameObject.SetActive(true);
                UiObjectReferrer.instance.tutorialStep6EN.gameObject.SetActive(true);
                EnableOnlySwipeBar();
                PlayerScript.instance.SetTargetMass(ConstantManager.PLAYER_INGAME_START_MASS, true);
                PlayerMovementScript.instance.allowSwipe = true;
                PlayerMovementScript.instance.forceCenterPosition = false;
                break;
            case 6: // Among the particles there are also special ones that will help you. You better not destroy them.
                DisableAllStats();
                UiObjectReferrer.instance.tutorialStep7DE.gameObject.SetActive(true);
                UiObjectReferrer.instance.tutorialStep7EN.gameObject.SetActive(true);
                ParticleSpawnScript.instance.spawnModi = ParticleSpawnModi.onlySpecial;
                break;
            case 8:
                DontShowAgainAtStartup();
                SceneManager.instance.callSceneMenu();
                break;
        }
    }

    public void DontShowAgainAtStartup() {
        SaveDataManager.getValue.tutorialFinished = true;
        SaveDataManager.Save();
    }


    private void DisableAllStats() {
        UiObjectReferrer.instance.tutorialStep1DE.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep2DE.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep3DE.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep4DE.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep5DE.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep6DE.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep7DE.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep1EN.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep2EN.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep3EN.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep4EN.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep5EN.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep6EN.gameObject.SetActive(false);
        UiObjectReferrer.instance.tutorialStep7EN.gameObject.SetActive(false);
    }
    private void EnableOnlySwipeBar() {
        UiObjectReferrer.instance.ingameMain.SetActive(true);
        UiObjectReferrer.instance.ingameGoldText.gameObject.SetActive(false);
        UiObjectReferrer.instance.ingameScoreText.gameObject.SetActive(false);
        UiObjectReferrer.instance.ingameItemSlotsLeft.SetActive(false);
        UiObjectReferrer.instance.ingameItemSlotsRight.SetActive(false);
        UiObjectReferrer.instance.ingamePauseButton.SetActive(false);
    }
}
