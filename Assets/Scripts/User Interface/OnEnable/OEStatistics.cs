using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OEStatistics : MonoBehaviour {

    void OnEnable() {
        UiObjectReferrer.instance.statisticsScrollRect.verticalNormalizedPosition = 1f;
        if (SaveDataManager.getValue.settingsLanguage == SettingsLanguages.English)
            UiObjectReferrer.instance.statisticsTextMain.text = GetTextEN();
        else
            UiObjectReferrer.instance.statisticsTextMain.text = GetTextDE();
    }

    private string GetTextDE() {
        string _ret = "";
        _ret += "<b>Allgemein:</b>";
        _ret += "\nAnzahl Spiele: " + SaveDataManager.getValue.statsTotalGamesPlayed;
        _ret += "\nAnzahl Wiederbeleben: " + SaveDataManager.getValue.statsTotleCountRevive;
        _ret += "\nGesamte Zeit Ingame: " + PrittyFormateTime(SaveDataManager.getValue.totalTimeIngame);
        _ret += "\nSumme aller Scores: " + SaveDataManager.getValue.scoreTotal;
        var avgScr = SaveDataManager.getValue.statsTotalGamesPlayed == 0 ? 0 : SaveDataManager.getValue.scoreTotal / SaveDataManager.getValue.statsTotalGamesPlayed;
        _ret += "\nDurchschnittlicher Score: " + avgScr;
        _ret += "\nHighscore: " + SaveDataManager.getValue.highscore;
        
        _ret += "\n\nGesamte zugelegte Masse: " + SaveDataManager.getValue.statsTotalGainedMass;
        _ret += "\nGesamte verlorene Masse: " + SaveDataManager.getValue.statsTotalLossMass;
        _ret += "\nGesamtes eingesammeltes Gold: " + SaveDataManager.getValue.statsTotalGainedGold;
        

        _ret += "\n\n\n\n<b>Partikel:</b>";
        _ret += "\nGesamt gespawnt: " + SaveDataManager.getValue.statsTotalParticles;
        
        _ret += "\n\n<b>Normale Partikel:</b>";
        _ret += "\nNormale Partikel gespawnt: " + SaveDataManager.getValue.statsTotalNormalParticlesSpawned;
        _ret += "\nNormale Partikel zerstört: " + SaveDataManager.getValue.statsTotalNormalParticlesDestroyed;
        _ret += "\nNormale Partikel eingesammelt: " + SaveDataManager.getValue.statsTotalNormalParticlesCollected;
        
        _ret += "\n\n<b>Schrumpf Partikel:</b>";
        _ret += "\nSchrumpf Partikel gespawnt: " + SaveDataManager.getValue.statsTotalShrinkParticlesSpawned;
        _ret += "\nSchrumpf Partikel zerstört: " + SaveDataManager.getValue.statsTotalShrinkParticlesDestroyed;
        _ret += "\nSchrumpf Partikel eingesammelt: " + SaveDataManager.getValue.statsTotalShrinkParticlesCollected;
        
        _ret += "\n\n<b>Gold Partikel:</b>";
        _ret += "\nGold Partikel gespawnt: " + SaveDataManager.getValue.statsTotalGoldParticlesSpawned;
        _ret += "\nGold Partikel zerstört: " + SaveDataManager.getValue.statsTotalGoldParticlesDestroyed;
        _ret += "\nGold Partikel eingesammelt: " + SaveDataManager.getValue.statsTotalGoldParticlesCollected;
        

        _ret += "\n\n\n\n<b>Projektile:</b>";
        _ret += "\nGesamt geschossen: " + SaveDataManager.getValue.statsTotalProjectilesFired;
        _ret += "\nGesamt getroffen: " + SaveDataManager.getValue.statsTotalProjectilesHit;
        

        _ret += "\n\n<b>Steuerung:</b>";
        _ret += "\nAnzahl Swipes: " + SaveDataManager.getValue.statsTotalInputSwipe;
        _ret += "\nAnzahl Tabs: " + SaveDataManager.getValue.statsTotalInputTab;

        
        _ret += "\n\n<b>Shop:</b>";
        _ret += "\nGold ausgegeben: " + SaveDataManager.getValue.statsTotalGoldSpend;
        _ret += "\nVerbesserungen gekauft: " + GetTotalLVL();
        return _ret;
    }
    private string GetTextEN() {
        string _ret = "";
        _ret += "<b>General:</b>";
        _ret += "\nGames played: " + SaveDataManager.getValue.statsTotalGamesPlayed;
        _ret += "\nRevive Count: " + SaveDataManager.getValue.statsTotleCountRevive;
        _ret += "\nTotal Time Ingame: " + PrittyFormateTime(SaveDataManager.getValue.totalTimeIngame);
        _ret += "\nSum of all Scores: " + SaveDataManager.getValue.scoreTotal;
        var avgScr = SaveDataManager.getValue.statsTotalGamesPlayed == 0 ? 0 : SaveDataManager.getValue.scoreTotal / SaveDataManager.getValue.statsTotalGamesPlayed;
        _ret += "\nAverage Score: " + avgScr;
        _ret += "\nHighscore: " + SaveDataManager.getValue.highscore;
        
        _ret += "\n\nTotal gained mass: " + SaveDataManager.getValue.statsTotalGainedMass;
        _ret += "\nTotal loss mass: " + SaveDataManager.getValue.statsTotalLossMass;
        _ret += "\nTotal collected gold: " + SaveDataManager.getValue.statsTotalGainedGold;
        

        _ret += "\n\n\n\n<b>Particle:</b>";
        _ret += "\nSpawned total: " + SaveDataManager.getValue.statsTotalParticles;
        
        _ret += "\n\n<b>Normal Particles:</b>";
        _ret += "\nNormal Particles spawned: " + SaveDataManager.getValue.statsTotalNormalParticlesSpawned;
        _ret += "\nNormal Particles destroyed: " + SaveDataManager.getValue.statsTotalNormalParticlesDestroyed;
        _ret += "\nNormal Particles collected: " + SaveDataManager.getValue.statsTotalNormalParticlesCollected;
        
        _ret += "\n\n<b>Shrink Particels:</b>";
        _ret += "\nShrink Partikels spawned: " + SaveDataManager.getValue.statsTotalShrinkParticlesSpawned;
        _ret += "\nShrink Partikels destroyed: " + SaveDataManager.getValue.statsTotalShrinkParticlesDestroyed;
        _ret += "\nShrink Partikels collected: " + SaveDataManager.getValue.statsTotalShrinkParticlesCollected;
        
        _ret += "\n\n<b>Gold Particles:</b>";
        _ret += "\nGold Particles spawned: " + SaveDataManager.getValue.statsTotalGoldParticlesSpawned;
        _ret += "\nGold Particles destroyed: " + SaveDataManager.getValue.statsTotalGoldParticlesDestroyed;
        _ret += "\nGold Particles collected: " + SaveDataManager.getValue.statsTotalGoldParticlesCollected;
        

        _ret += "\n\n\n\n<b>Projectiles:</b>";
        _ret += "\nTotal fired: " + SaveDataManager.getValue.statsTotalProjectilesFired;
        _ret += "\nTotal hit: " + SaveDataManager.getValue.statsTotalProjectilesHit;
        

        _ret += "\n\n<b>Controls:</b>";
        _ret += "\nSwipes Count: " + SaveDataManager.getValue.statsTotalInputSwipe;
        _ret += "\nTab Count: " + SaveDataManager.getValue.statsTotalInputTab;

        
        _ret += "\n\n<b>Shop:</b>";
        _ret += "\nGold spend: " + SaveDataManager.getValue.statsTotalGoldSpend;
        _ret += "\nUpgrades purchased: " + GetTotalLVL();
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
