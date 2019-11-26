using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldScript : MonoBehaviour {

    //INSTANCE
    public static GoldScript instance;

    void Awake() {
        instance = this;
    }

    //SPENDING
    public bool SpendGold(int amount) {
        if (SaveDataManager.getValue.currentGold >= amount) {
            SaveDataManager.getValue.currentGold -= amount;
            SaveDataManager.Save();
            return true;
        } else {
            return false;
        }
    }
    public bool CheckGold(int amount) {
        return SaveDataManager.getValue.currentGold >= amount;
    }

    //EARNING
    public void EarnGold(int amount) {
        SaveDataManager.getValue.currentGold += amount;
        SaveDataManager.Save();
    }
}
