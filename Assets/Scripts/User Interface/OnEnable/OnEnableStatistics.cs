using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableStatistics : MonoBehaviour {

    void OnEnable() {
        if (SaveDataManager.getValue.settingsLanguage == SettingsLanguages.English)
            UiObjectReferrer.instance.statisticsTextMain.text = GetTextEN();
        else
            UiObjectReferrer.instance.statisticsTextMain.text = GetTextDE();
    }

    private string GetTextDE() {
        string _ret = "";
        _ret += "<b>Allgemein:</b>";
        _ret += "\n<i>Anzahl Spiele: " + SaveDataManager.getValue.statsTotalGamesPlayed + "</i>";
        _ret += "\n<i>Anzahl Wiederbeleben:" + SaveDataManager.getValue.statsTotleCountRevive + "</i>";
        _ret += "\n<i>Gesamte Zeit Ingame: " + PrittyFormateTime(SaveDataManager.getValue.totalTimeIngame) + "</i>";
        _ret += "\n<i>Summe aller Scores: " + SaveDataManager.getValue.scoreTotal + "</i>";
        _ret += "\n<i>Durchschnittlicher Score: " + (SaveDataManager.getValue.scoreTotal / SaveDataManager.getValue.statsTotalGamesPlayed) + "</i>";
        _ret += "\n<i>Highscore: " + SaveDataManager.getValue.highscore + "</i>";
        
        _ret += "\n\n<i>Gesamte zugelegte Masse: " + SaveDataManager.getValue.statsTotalGainedMass + "</i>";
        _ret += "\n<i>Gesamte verlorene Masse: " + SaveDataManager.getValue.statsTotalLossMass + "</i>";
        _ret += "\n<i>Gesamtes eingesammeltes Gold: " + SaveDataManager.getValue.statsTotalGainedGold + "</i>";
        

        _ret += "\n\n\n\n<b>Partikel:</b>";
        _ret += "\n<i>Gesamt gespawnt: " + SaveDataManager.getValue.statsTotalParticles + "</i>";
        
        _ret += "\n\n<b>Normale Partikel:</b>";
        _ret += "\n<i>Normale Partikel gespawnt: " + SaveDataManager.getValue.statsTotalNormalParticlesSpawned + "</i>";
        _ret += "\n<i>Normale Partikel zerstört: " + SaveDataManager.getValue.statsTotalNormalParticlesDestroyed + "</i>";
        _ret += "\n<i>Normale Partikel eingesammelt: " + SaveDataManager.getValue.statsTotalNormalParticlesCollected + "</i>";
        
        _ret += "\n\n<b>Schrumpf Partikel:</b>";
        _ret += "\n<i>Schrumpf Partikel gespawnt: " + SaveDataManager.getValue.statsTotalShrinkParticlesSpawned + "</i>";
        _ret += "\n<i>Schrumpf Partikel zerstört: " + SaveDataManager.getValue.statsTotalShrinkParticlesDestroyed + "</i>";
        _ret += "\n<i>Schrumpf Partikel eingesammelt: " + SaveDataManager.getValue.statsTotalShrinkParticlesCollected + "</i>";
        
        _ret += "\n\n<b>Gold Partikel:</b>";
        _ret += "\n<i>Gold Partikel gespawnt: " + SaveDataManager.getValue.statsTotalGoldParticlesSpawned + "</i>";
        _ret += "\n<i>Gold Partikel zerstört: " + SaveDataManager.getValue.statsTotalGoldParticlesDestroyed + "</i>";
        _ret += "\n<i>Gold Partikel eingesammelt: " + SaveDataManager.getValue.statsTotalGoldParticlesCollected + "</i>";
        

        _ret += "\n\n\n\n<b>Projektile:</b>";
        _ret += "\n<i>Gesamt geschossen: " + SaveDataManager.getValue.statsTotalProjectilesFired + "</i>";
        _ret += "\n<i>Gesamt getroffen: " + SaveDataManager.getValue.statsTotalProjectilesHit + "</i>";


        _ret += "\n\n<b>Items:</b>";
        _ret += "\n<i>Gesamt gespawnt: " + SaveDataManager.getValue.statsTotalItemsSpawned + "</i>";
        _ret += "\n<i>Gesamt genutzt: " + SaveDataManager.getValue.statsTotalItemsUsed + "</i>";
        

        _ret += "\n\n<b>Steuerung:</b>";
        _ret += "\n<i>Anzahl Swipes: " + SaveDataManager.getValue.statsTotalInputSwipe + "</i>";
        _ret += "\n<i>Anzahl Tabs: " + SaveDataManager.getValue.statsTotalInputTab + "</i>";

        
        _ret += "\n\n<b>Shop:</b>";
        _ret += "\n<i>Gold ausgegeben: " + SaveDataManager.getValue.statsTotalGoldSpend + "</i>";
        _ret += "\n<i>Verbesserungen gekauft: " + GetTotalLVL() + "</i>";
        return _ret;
    }
    private string GetTextEN() {
        string _ret = "";
        _ret += "<b>General:</b>";
        _ret += "\n<i>Games played: " + SaveDataManager.getValue.statsTotalGamesPlayed + "</i>";
        _ret += "\n<i>Revive Count: " + SaveDataManager.getValue.statsTotleCountRevive + "</i>";
        _ret += "\n<i>Total Time Ingame: " + PrittyFormateTime(SaveDataManager.getValue.totalTimeIngame) + "</i>";
        _ret += "\n<i>Sum of all Scores: " + SaveDataManager.getValue.scoreTotal + "</i>";
        _ret += "\n<i>Average Score: " + (SaveDataManager.getValue.scoreTotal / SaveDataManager.getValue.statsTotalGamesPlayed) + "</i>";
        _ret += "\n<i>Highscore: " + SaveDataManager.getValue.highscore + "</i>";
        
        _ret += "\n\n<i>Total gained mass: " + SaveDataManager.getValue.statsTotalGainedMass + "</i>";
        _ret += "\n<i>Total loss mass: " + SaveDataManager.getValue.statsTotalLossMass + "</i>";
        _ret += "\n<i>Total collected gold: " + SaveDataManager.getValue.statsTotalGainedGold + "</i>";
        

        _ret += "\n\n\n\n<b>Particle:</b>";
        _ret += "\n<i>Spawned total: " + SaveDataManager.getValue.statsTotalParticles + "</i>";
        
        _ret += "\n\n<b>Normal Particles:</b>";
        _ret += "\n<i>Normal Particles spawned: " + SaveDataManager.getValue.statsTotalNormalParticlesSpawned + "</i>";
        _ret += "\n<i>Normal Particles destroyed: " + SaveDataManager.getValue.statsTotalNormalParticlesDestroyed + "</i>";
        _ret += "\n<i>Normal Particles collected: " + SaveDataManager.getValue.statsTotalNormalParticlesCollected + "</i>";
        
        _ret += "\n\n<b>Shrink Particels:</b>";
        _ret += "\n<i>Shrink Partikels spawned: " + SaveDataManager.getValue.statsTotalShrinkParticlesSpawned + "</i>";
        _ret += "\n<i>Shrink Partikels destroyed: " + SaveDataManager.getValue.statsTotalShrinkParticlesDestroyed + "</i>";
        _ret += "\n<i>Shrink Partikels collected: " + SaveDataManager.getValue.statsTotalShrinkParticlesCollected + "</i>";
        
        _ret += "\n\n<b>Gold Particles:</b>";
        _ret += "\n<i>Gold Particles spawned: " + SaveDataManager.getValue.statsTotalGoldParticlesSpawned + "</i>";
        _ret += "\n<i>Gold Particles destroyed: " + SaveDataManager.getValue.statsTotalGoldParticlesDestroyed + "</i>";
        _ret += "\n<i>Gold Particles collected: " + SaveDataManager.getValue.statsTotalGoldParticlesCollected + "</i>";
        

        _ret += "\n\n\n\n<b>Projectiles:</b>";
        _ret += "\n<i>Total fired: " + SaveDataManager.getValue.statsTotalProjectilesFired + "</i>";
        _ret += "\n<i>Total hit: " + SaveDataManager.getValue.statsTotalProjectilesHit + "</i>";


        _ret += "\n\n<b>Items:</b>";
        _ret += "\n<i>Items spawned: " + SaveDataManager.getValue.statsTotalItemsSpawned + "</i>";
        _ret += "\n<i>Items used: " + SaveDataManager.getValue.statsTotalItemsUsed + "</i>";
        

        _ret += "\n\n<b>Controls:</b>";
        _ret += "\n<i>Swipes Count: " + SaveDataManager.getValue.statsTotalInputSwipe + "</i>";
        _ret += "\n<i>Tab Count: " + SaveDataManager.getValue.statsTotalInputTab + "</i>";

        
        _ret += "\n\n<b>Shop:</b>";
        _ret += "\n<i>Gold spend: " + SaveDataManager.getValue.statsTotalGoldSpend + "</i>";
        _ret += "\n<i>Upgrades purchased: " + GetTotalLVL() + "</i>";
        return _ret;
    }

    private string PrittyFormateTime(float time) {
        var hh = Mathf.FloorToInt(time / 3600).ToString("00");
        var mm = Mathf.FloorToInt((time % 3600) / 60).ToString("00");
        var ss = Mathf.FloorToInt((time % 3600) % 60).ToString("00");
        var ms = Mathf.FloorToInt((time - Mathf.FloorToInt(time)) * 1000);

        var timeString = hh+":"+mm+":"+ss+"."+ms;
        return timeString;
    }
    private string GetTotalLVL() {
        var sum = SaveDataManager.getValue.forceItemLVL + 
                SaveDataManager.getValue.clearItmLVL +
                SaveDataManager.getValue.shrinkItemLVL +
                SaveDataManager.getValue.shieldItemLVL +
                SaveDataManager.getValue.slowItemLVL +
                SaveDataManager.getValue.slideItemLVL +
                SaveDataManager.getValue.goldrushItemLVL;   
        return sum.ToString();           
    }
}
