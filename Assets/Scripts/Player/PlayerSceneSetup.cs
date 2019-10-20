using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneSetup : MonoBehaviour {

    [HideInInspector] public static PlayerSceneSetup instance;
    [HideInInspector] public bool thisScriptLoaded = false;
    void Awake() {
        instance = this;
        thisScriptLoaded = true;
    }

    public void SetupIngame() {
        PlayerMovementScript.instance.allowSwipe = ConstantManager.INPUT_ALLOW_SWIPE_INGAME;
        PlayerMovementScript.instance.allowTab = ConstantManager.INPUT_ALLOW_TAP_INGAME;
        PlayerMovementScript.instance.lastSwipeTime = Time.time;

        ShootingController.instance.DestroyAllParticles();

        PlayerScript.instance.playerAllowGrow = ConstantManager.PLAYER_INGAME_ALLOW_GROW;
        PlayerScript.instance.playerAllowShrink = ConstantManager.PLAYER_INGAME_ALLOW_SHRINK;
        PlayerScript.instance.playerAllowRotate = ConstantManager.PLAYER_INGAME_ALLOW_ROTATION;
        PlayerScript.instance.playerAllowWaves = ConstantManager.PLAYER_INGAME_ALLOW_WAVES;
        PlayerScript.instance.playerRotationSpeed = ConstantManager.PLAYER_INGAME_ROTATION_SPEED;
        PlayerScript.instance.SetTargetMass(ConstantManager.PLAYER_INGAME_START_MASS, true);
        PlayerScript.instance.ShowPlayer();
    }

    public void SetupMenu() {
        PlayerMovementScript.instance.allowSwipe = ConstantManager.INPUT_ALLOW_SWIPE_MENU;
        PlayerMovementScript.instance.allowTab = ConstantManager.INPUT_ALLOW_TAP_MENU;
        
        ShootingController.instance.DestroyAllParticles();

        PlayerScript.instance.playerAllowGrow = ConstantManager.PLAYER_MENU_ALLOW_GROW;
        PlayerScript.instance.playerAllowShrink = ConstantManager.PLAYER_MENU_ALLOW_SHRINK;
        PlayerScript.instance.playerAllowRotate = ConstantManager.PLAYER_MENU_ALLOW_ROTATION;
        PlayerScript.instance.playerAllowWaves = ConstantManager.PLAYER_MENU_ALLOW_WAVES;
        PlayerScript.instance.playerRotationSpeed = ConstantManager.PLAYER_MENU_ROTATION_SPEED;
        PlayerScript.instance.SetTargetMass(ConstantManager.PLAYER_MENU_START_MASS, true);
        PlayerScript.instance.ShowPlayer();
    }

    public void SetupDisabled() {
        PlayerMovementScript.instance.allowSwipe = false;
        PlayerMovementScript.instance.allowTab = false;
        
        ShootingController.instance.DestroyAllParticles();
        
        PlayerScript.instance.playerAllowGrow = false;
        PlayerScript.instance.playerAllowShrink = false;
        PlayerScript.instance.playerAllowRotate = false;
        PlayerScript.instance.playerAllowWaves = false;
        PlayerScript.instance.playerRotationSpeed = 0;
        PlayerScript.instance.SetTargetMass(0, true);
        PlayerScript.instance.HidePlayer();
    }

    public void SetupPause() {
        PlayerMovementScript.instance.allowSwipe = false;
        PlayerMovementScript.instance.allowTab = false;
    }

    public void SetupResume() {
        PlayerMovementScript.instance.allowSwipe = true;
        PlayerMovementScript.instance.allowTab = true;
    }
}
