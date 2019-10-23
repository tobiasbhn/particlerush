using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class ItemDefinition : ScriptableObject {

    [SerializeField] private ItemLanguageSet english;
    [SerializeField] private ItemLanguageSet german;
    [SerializeField] private Texture itemTexture;
    [SerializeField] private List<float> itemLevels = new List<float>();
    [SerializeField] private List<int> itemPrices = new List<int>();

    public Texture getIcon() {
        return itemTexture;
    }
    public string getInfoEnglish() {
        var info = english.itemName.ToString() + "\n" + english.itemDescription.ToString();
        return info;
    }
    public string getInfoGerman() {
        var info = german.itemName.ToString() + "\n" + german.itemDescription.ToString();
        return info;
    }
    public int getCurrendPrice(int lvl) {
        var price = itemPrices[lvl];
        return price;
    }

    [System.Serializable]
    private class ItemLanguageSet{
        public string itemName = "";
        public string itemDescription = "";
    }

}
