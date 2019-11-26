using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OEShopMenu : MonoBehaviour {

    //INSTANCES
    public static OEShopMenu instance;

    //VARS
    [HideInInspector] public ItemDefinition currentItem;

    void Awake() {
        instance = this;
    }

    //ENABLE & DISABLE FUNCTION
    public void OnEnable() {
        UiObjectReferrer.instance.shopScrollRect.horizontalNormalizedPosition = 0f;
        DisableAllItems();
    }

    public void DisableAllItems() {
        DisableAllItems(true);
    }
    public void DisableAllItems(bool updateInfos) {
        foreach (ItemShopPrefab item in ItemPool.instance.itemShopPrefabs) {
            item.HideItemInfos();
        }
        currentItem = null;
        SceneManager.instance.callSceneShopReset();
        if (updateInfos)
            UpdateGeneralInfos();
    }

    //BUY ITEM & UPDATE INFOS
    public void BuyItem() {
        if (currentItem != null && GoldScript.instance.SpendGold(currentItem.getCurrentPrice())) {
            currentItem.UpdateLevel();
            UpdateGeneralInfos();
            foreach (ItemShopPrefab item in ItemPool.instance.itemShopPrefabs) {
                item.UpdateLvlBasedItemInfos();
            }
        }
    }
    public void UpdateGeneralInfos() {
        if (currentItem == null) {
            var description = SaveDataManager.getValue.settingsLanguage == SettingsLanguages.English ? "Click on an Upgrade to get more Informations." : "Klicke auf ein Upgrade für weitere Informationen.";
            UiObjectReferrer.instance.shopDescription.text = description;
            UiObjectReferrer.instance.shopLevelInfo.text = " ";
            DisableButton();
        } else {
            UiObjectReferrer.instance.shopDescription.text = currentItem.getDescription();
            UiObjectReferrer.instance.shopLevelInfo.text = currentItem.getInfo();
            var price = currentItem.getCurrentPrice();
            if (price == -1 || !GoldScript.instance.CheckGold(price))
                DisableButton();
            else
                EnableButton();
        }
    }

    //BUTTON FUNCTIONS
    private void EnableButton() {
        UiObjectReferrer.instance.shopBuyButtonDE.interactable = true;
        UiObjectReferrer.instance.shopBuyTextDE.color = new Color32(255, 255, 255, 255);
        UiObjectReferrer.instance.shopBuyButtonEN.interactable = true;
        UiObjectReferrer.instance.shopBuyTextEN.color = new Color32(255, 255, 255, 255);
    }
    private void DisableButton() {
        UiObjectReferrer.instance.shopBuyButtonDE.interactable = false;
        UiObjectReferrer.instance.shopBuyTextDE.color = new Color32(255, 255, 255, 90);
        UiObjectReferrer.instance.shopBuyButtonEN.interactable = false;
        UiObjectReferrer.instance.shopBuyTextEN.color = new Color32(255, 255, 255, 90);
    }
}
