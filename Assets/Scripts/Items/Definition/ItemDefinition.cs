using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class ItemDefinition : ScriptableObject {

    [SerializeField] private ItemLanguageSet english;
    [SerializeField] private ItemLanguageSet german;
    [SerializeField] private Sprite itemIconAsSprite;
    [SerializeField] private Texture itemIconAsTexture;
    [SerializeField] private ScenariosDefault shopScenarioOnActive;
    [SerializeField] private float[] itemEffects = new float[5];
    [SerializeField] private int[] itemPrices = new int[5];


    public Texture getIconAsTexture() {
        return itemIconAsTexture;
    }
    public Sprite getIconAsSprite() {
        return itemIconAsSprite;
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

    public int getCurrendPrice() {
        var LVL = getCurrendLVL();
        return itemPrices[LVL];
    }
    public float getCurrendEffect() {
        var LVL = getCurrendLVL();
        return itemEffects[LVL];
    }
    public virtual int getCurrendLVL() {
        return 0;
    }
    public ScenariosDefault GetScenario() {
        return shopScenarioOnActive;
    }



    [System.Serializable]
    private class ItemLanguageSet{
        public string itemName = "";
        public string itemDescription = "";
    }

}
