

//GAME STATUS
public enum GameStatus {loading, tutorial, menu, ingame, endgame, shop, leaderboard, settings, paused, notification, advert, itemDemoShrink, itemDemoShield, itemDemoForce, itemDemoClear, itemDemoSlow, itemDemoSlide, itemDemoGoldrush};

//PARTICLES / ITEMS & SPAWN
public enum ParticleType {grow, shrink, gold};
public enum ParticleSpawnModi {none, all, onlyNorm, onlyShrink, onlyGold, onlyMassRelative, onlySpecial};
public enum ItemActionOnDrop {slot1, slot2, slot3, inactive, activate, blocked};
public enum ItemAnimationType {none, circle, line};

//SETTINGS
public enum SettingsLanguages {English, German};
public enum SettingsSounds {Off, All, Sound};
public enum SettingsVibration {Off, Short, Medium, Long};
public enum SettingsItemPosition {Left, Right};
public enum SettingsDebug {off, ingame, everywhere};

//ADS
public enum AdResult {Finished, Skipped, Failed, Private};
public enum AdType {Normal, Rewarded};

//SCENARIOS
public enum PlayerSceneModis {keepCurrent, disabled, ingame, menu, pause, resume };
public enum ParticleBehaviourSceneModis {keepCurrent, disabled, ingame, menu };
public enum ItemSpawnSceneModi {refuse, allow };
public enum PlayerInputSceneModis {refuse, allow, onlyShoot, onlyMove };
public enum ItemInputSceneModis {refuse, allow };
public enum TimeSceneModis {keepCurrent, normal, stop };
public enum UiSceneModis {keepCurrent, disabled, ingame, menu, pause, shop, settings, adNotification, endgame, openSource, statistics, tutorial };
public enum UiShowAccountInfo {disabled, show};
public enum UiShowLevelInfo {disabled, show};
public enum ShakeSceneModis {keepCurrent, disabled, ingame, menu };
public enum ScoreSceneModis {keepCurrent, disabled, activeReset, active };
public enum RuntimeDataSceneModi {keepCurrent, ingame, endgame };
public enum AdsSceneModis {refuse, allow };
public enum ReviveSceneModis {keepCurrent, reset };
public enum AudioLowPassModi {lowPass, normal };
public enum TutorialActionModie {disabled, start};