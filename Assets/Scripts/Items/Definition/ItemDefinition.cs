﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class ItemDefinition : ScriptableObject {

    [SerializeField] private ItemLanguageSet english;
    [SerializeField] private ItemLanguageSet german;
    [SerializeField] private Sprite itemIconAsSprite;
    [SerializeField] private Texture itemIconAsTexture;
    [SerializeField] private ScenariosDefault shopScenarioOnActive;
    [SerializeField] private List<float> itemLevels = new List<float>();
    [SerializeField] private List<int> itemPrices = new List<int>();


    public Texture getIconAsTexture() {
        return itemIconAsTexture;
    }
    public Sprite getIconAsSprite() {
        return itemIconAsSprite;
    }
    public string getInfoEnglish() {
        var info = english.itemName.ToString() + ":\n\n" + english.itemDescription.ToString();
        return info;
    }
    public string getInfoGerman() {
        var info = german.itemName.ToString() + ":\n\n" + german.itemDescription.ToString();
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



    [System.Serializable]
    private class ItemLanguageSet{
        public string itemName = "";
        public string itemDescription = "";
    }

}
