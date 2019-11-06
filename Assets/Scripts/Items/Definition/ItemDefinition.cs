using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class ItemDefinition : ScriptableObject {

    [SerializeField] private ItemLanguageSet english;
    [SerializeField] private ItemLanguageSet german;
    [SerializeField] private Sprite itemIconAsSprite;
    [SerializeField] private Texture itemIconAsTexture;
    [SerializeField] private ScenariosDefault shopScenarioOnActive;
    [SerializeField] private ItemAnimationType itemAnimation;
    [SerializeField] private List<float> itemLevels = new List<float>();
    [SerializeField] private List<int> itemPrices = new List<int>();


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

    public string getCurrendPrice() {
        var LVL = getCurrendLVL();
        var price = itemPrices[LVL].ToString();
        if (LVL == 0 && SaveDataManager.getValue.settingsLanguage == SettingsLanguages.English)
            price = "locked";
        else if (LVL == 0 && SaveDataManager.getValue.settingsLanguage == SettingsLanguages.German)
            price = "verschlossen";
        return price;
    }
    public float getCurrendEffect() {
        var LVL = getCurrendLVL();
        return itemLevels[LVL];
    }
    public virtual int getCurrendLVL() {
        return 0;
    }
    public ScenariosDefault GetScenario() {
        return shopScenarioOnActive;
    }
    public ItemAnimationType GetAnimationType() {
        return itemAnimation;
    }



    [System.Serializable]
    private class ItemLanguageSet{
        public string itemName = "";
        public string itemDescription = "";
    }

}
