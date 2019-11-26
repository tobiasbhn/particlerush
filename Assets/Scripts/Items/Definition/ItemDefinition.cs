using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class ItemDefinition : ScriptableObject {

    [SerializeField] private ItemLanguageSet english;
    [SerializeField] private ItemLanguageSet german;
    [SerializeField] private Texture itemIconAsTexture;
    [SerializeField] protected float[] itemEffects = new float[4];
    [SerializeField] private int[] itemPrices = new int[3];


    public Texture getIconAsTexture() {
        return itemIconAsTexture;
    }
    public string getDescription() {
        var info = "";
        if (SaveDataManager.getValue.settingsLanguage == SettingsLanguages.English)
            info = english.itemDescription;
        else if (SaveDataManager.getValue.settingsLanguage == SettingsLanguages.German)
            info = german.itemDescription;
        return info;
    }
    public string getName() {
        var info = "";
        if (SaveDataManager.getValue.settingsLanguage == SettingsLanguages.English)
            info = english.itemName;
        else if (SaveDataManager.getValue.settingsLanguage == SettingsLanguages.German)
            info = german.itemName;
        return info;
    }
    public string getInfo() {
        var LVL = getCurrentLVL();
        var current = SaveDataManager.getValue.settingsLanguage == SettingsLanguages.German ? "Aktuell: " + german.itemInfos[LVL] : "Current: " + english.itemInfos[LVL];
        if (LVL >= 3)
            return "Max Level\n" + current;
        var next = SaveDataManager.getValue.settingsLanguage == SettingsLanguages.German ? "Nächstes: " + german.itemInfos[LVL + 1] : "Next: " + english.itemInfos[LVL + 1];
        return current + "\n" + next;
    }

    public int getCurrentPrice() {
        var LVL = getCurrentLVL();
        if (LVL >= 3)
            return -1;
        return itemPrices[LVL];
    }
    public float getCurrendEffect() {
        var LVL = getCurrentLVL();
        return itemEffects[LVL];
    }
    public virtual int getCurrentLVL() {
        return 0;
    }
    public virtual void UpdateLevel() {

    }
    public virtual void showShopDemo() {

    }



    [System.Serializable]
    private class ItemLanguageSet{
        public string itemName = "";
        public string itemDescription = "";
        public string[] itemInfos = new string[4];
    }

}
