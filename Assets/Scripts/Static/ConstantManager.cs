using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstantManager {

    //TODO ON PUBLISHING:
        // - ConstantManager.useLocalFile = true;
        // - ConstantManager.localSaveFileName = Unique name with Verion included
        // - PlayerSettings => Set right Version
        // - PlayerSettings => Check right Game Spelling
        // - PlayerSettings => Game Icons set?
        // - PlayerSettings => Bundle Number correct?
        // - PlayerSettings => App signed?
        // - UnityServices => Ads disable "Development-Mode"
        // - AdsManager => ID's for the Advertisments right?
        // - SaveDataManager => All default Values set correctly? (e.g. Download-Language)


    //DEBUG
    public static readonly bool useLocalSaveFile = false;
    public static readonly string localSaveFileName = "particleRush_45trtefefgv003";

    //PLAYER && MESH GENERATION
    //MASS
    public const int PLAYER_MAX_MESH_GENERATION_SIZE = 70;
    public const int PLAYER_MIN_MESH_GENERATION_SIZE = 12;
    public const int PLAYER_INGAME_START_MASS = 20;
    public const int PLAYER_MENU_START_MASS = 40;
    public const float PLAYER_MAX_SIZE_DIFFERENCE_TO_END_ANIMATION = 0.3f; //if (sizeDifference >= X) {grow || shrink}
    public const float PLAYER_SIZE_ANIMATION_DURATION = 30; //currentMass += sizeDifference / X;
    //ROTATION
    public const int PLAYER_INGAME_ROTATION_SPEED = 20;
    public const int PLAYER_MENU_ROTATION_SPEED = 15;
    //WAVES
    public const int PLAYER_WAVES_MIN_WAVE_MOTION = 50;
    public const float PLAYER_WAVES_REDUCION_PER_FRAME = 0.07f;
    public const int PLAYER_WAVES_SPEED = 4;
    public const int PLAYER_WAVES_HEIGHT = 10;
    public const float PLAYER_WAVES_OFFSET = 0.8f;
    public const int PLAYER_WAVES_DECREASE_WAVES_OUTSIDE = 4;
    //BEHAVIUR
    //Ingame
    public const bool PLAYER_INGAME_ALLOW_GROW = true;
    public const bool PLAYER_INGAME_ALLOW_SHRINK = true;
    public const bool PLAYER_INGAME_ALLOW_ROTATION = true;
    public const bool PLAYER_INGAME_ALLOW_WAVES = true;
    //Menu
    public const bool PLAYER_MENU_ALLOW_GROW = false;
    public const bool PLAYER_MENU_ALLOW_SHRINK = false;
    public const bool PLAYER_MENU_ALLOW_ROTATION = true;
    public const bool PLAYER_MENU_ALLOW_WAVES = true;
    

    //CAMERA, SCREEN AND WORLD SIZES
    public static readonly float CAMERA_DISTANCE_PLAYER = Vector3.Distance(Camera.main.transform.position, PLAYER_DEFAULT_POSITION_IN_WORLD);
    public static readonly Vector3 CAMERA_LOWER_LEFT_CORNER_IN_WORD_SPACE = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, CAMERA_DISTANCE_PLAYER));
    public static readonly Vector3 CAMERA_UPPER_RIGHT_CORNER_IN_WORLD_SPACE = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, CAMERA_DISTANCE_PLAYER));
    public static readonly float CAMERA_SCREEN_HEIGHT_IN_WORLD_SPACE = 2.0f * CAMERA_DISTANCE_PLAYER * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
    public static readonly float CAMERA_SCREEN_WIDTH_IN_WORD_SPACE = CAMERA_SCREEN_HEIGHT_IN_WORLD_SPACE * Camera.main.aspect;
    public static readonly float PLAYER_AMOUNT_TO_GROW_PER_MASS_IN_WORLD_SPACE = CAMERA_SCREEN_WIDTH_IN_WORD_SPACE / PLAYER_MAX_MESH_GENERATION_SIZE;
    public static readonly Vector3 PLAYER_DEFAULT_POSITION_IN_WORLD = Camera.main.ScreenToWorldPoint(new Vector3((Screen.width / 2), Screen.height - (Screen.width / 2), Vector3.Distance(Camera.main.gameObject.transform.position, Vector3.zero)));
    //CAMERA SHAKE
    public const float CAMERA_SHAKE_DURATION = 0.5f; //How long should Shake take
    public const float CAMERA_SHAKE_AMOUNT = 0.5f; //How strong should it shake
    public const float CAMERA_SHAKE_DECREASE = 1.5f; //How fast should it decrease
    public const float PP_CHROMATIC_ABBERATION_ESKALATION = 5f;
    public const bool CAMERA_SHAKE_ALLOW_INGAME = true;
    public const bool CAMERA_SHAKE_ALLOW_MENU = true;
    //UI
    public static readonly float UI_CONTAINERS_MARGIN_TOP = Screen.height / 100f * 1f; // in Percent
    public static readonly float UI_CONTAINERS_MARGIN_RIGHT = Screen.width / 100f * 2f; // in Percent
    public static readonly float UI_CONTAINERS_MARGIN_BOTTOM = Screen.height / 100f * 3f; // in Percent
    public static readonly float UI_CONTAINERS_MARGIN_LEFT = Screen.width / 100f * 2f; // in Percent


    //PLAYER MOVEMENT
    public const float PLAYER_MOVEMENT_SPEED = 5f;
    public const float PLAYER_MOVEMENT_SLIDE_TIME_COOLDOWN = 3f;


    //PARTICLE SETTINGS
    public static readonly float PARTICLE_SPAWN_BASE_DELAY_INGAME = 60f / 130; // 60 Seconds / Particle Spawn Count per Minute
    public static readonly float PARTICLE_SPAWN_BASE_DELAY_MENU = 60f / 20; // 60 Seconds / Particle Spawn Count per Minute
    public const float PARTICLE_SPAWN_BASE_ROTATION = 5f; // AddTorque(Random.Range(-x, x))
    public static readonly float PARTICLE_SPAWN_BASE_SPEED_INGAME = CAMERA_SCREEN_HEIGHT_IN_WORLD_SPACE * 7f;
    public static readonly float PARTICLE_SPAWN_BASE_SPEED_MENU = CAMERA_SCREEN_HEIGHT_IN_WORLD_SPACE * 4f;
    public const int PARTICLE_SPEED_RANDOM_FACTOR = 20; // speed = PARTICLE_BASE_SPEED / X
    public const int PARTICLE_MASS_TO_ADD_ON_DESTROY_WRONG_PARTICLE = 7;
    //CHANCES
    public const int PARTICLE_SHRINK_SPAWN_CHANCE = 15; // in Percent
    public const int PARTICLE_GOLD_SPAWN_CHANCE = 17; // in Percent
    //BEHAVIOUR
    public const SpawnModi PARTICLE_SPAWNMODI_INGAME = SpawnModi.all;
    public const SpawnModi PARTICLE_SPAWNMODI_MENU = SpawnModi.onlyNorm;
    public const bool PARTICLE_ALLOW_SPEED_INCREASE_INGAME = true;
    public const bool PARTICLE_ALLOW_SPEED_INCREASE_MENU = false;

    //PROJECTILE
    public const int PROJECTILE_MIN_DAMAGE_REDUCION_PER_PLAYER_MASS = 20;
    public const float PROJECTILE_MAX_LIFE_TIME = 1f; // Time in Seconds
    public const float PROJECTILE_MAX_WIDTH = 0.3f;
    public const float PROJECTILE_MIN_WIDTH = 0.03f;



    //INPUTS
    public const bool INPUT_ALLOW_SWIPE_INGAME = true;
    public const bool INPUT_ALLOW_SWIPE_MENU = false;
    public const bool INPUT_ALLOW_TAP_INGAME = true;
    public const bool INPUT_ALLOW_TAP_MENU = false;


    //ADVERTS
    public const float AD_TIME_TO_PASS_TO_SHOW_AD = 30f;


    //SCORE
    public const int SCORE_PER_SECOND = 100;
    public const int SCORE_PER_PARTICLE = 500;
}

