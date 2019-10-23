using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopPrefab : MonoBehaviour {

    public ItemDefinition itemDefinition;
    public Text itemPriceHolder;
    public RawImage itemIconHolder;
    public Text itemInfoHolder;

    void Awake() {
        
    }
    void Start() {
        
    }

    void OnEnable() {
        UpdateHoleShopPresence();
    }

    void UpdateHoleShopPresence() {
        UpdatePrice();
        itemIconHolder.texture = itemDefinition.getIcon();
        if (SaveDataManager.getValue.settingsLanguage == SettingsLanguages.English) {
            itemInfoHolder.text = itemDefinition.getInfoEnglish();
        } else {
            itemInfoHolder.text = itemDefinition.getInfoGerman();
        }
    }
    void UpdatePrice() {
        itemPriceHolder.text = itemDefinition.getCurrendPrice(0).ToString();
    }
}
