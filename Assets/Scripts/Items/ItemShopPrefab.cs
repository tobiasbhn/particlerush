using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopPrefab : MonoBehaviour {

    public ItemDefinition itemDefinition;
    public Text itemPriceHolder;
    public Text itemNameHolder;
    public RawImage itemIconHolder;
    public GameObject activeScreen;
    public Image progressLVL1;
    public Image progressLVL2;
    public Image progressLVL3;

    void OnEnable() {
        UpdateStaticItemInfos();
        UpdateLvlBasedItemInfos();
    }

    // on other Item gets Selected or No Item should be selected (e.g. coming to shop from menu)
    public void HideItemInfos() {
        activeScreen.SetActive(false);
    }

    // on select item in Shop-Menu (Button Press on Item)
    public void ShowItemInfos() {
        if (OEShopMenu.instance.currentItem != itemDefinition) {
            OEShopMenu.instance.DisableAllItems(false);
            OEShopMenu.instance.currentItem = itemDefinition;
            OEShopMenu.instance.UpdateGeneralInfos();
            activeScreen.SetActive(true);
            itemDefinition.showShopDemo();
        }
    }


    // Setup everything that is static, like Item Name and Item Description (never Change, no matter which LVL)
    void UpdateStaticItemInfos() {
        itemIconHolder.texture = itemDefinition.getIconAsTexture();
        itemNameHolder.text = itemDefinition.getName();
    }
    // Setup everything that depends on the LVL, like Price, Locked-State etc.
    public void UpdateLvlBasedItemInfos() {
        var LVL = itemDefinition.getCurrentLVL();
        var price = itemDefinition.getCurrentPrice();
        itemPriceHolder.text = price < 0 ? "MAX" : price.ToString();
        switch (LVL) {
            case 0:
                progressLVL1.color = new Color32(255, 255, 255, 90);
                progressLVL2.color = new Color32(255, 255, 255, 90);
                progressLVL3.color = new Color32(255, 255, 255, 90);
                break;
            case 1:
                progressLVL1.color = new Color32(255, 255, 255, 255);
                progressLVL2.color = new Color32(255, 255, 255, 90);
                progressLVL3.color = new Color32(255, 255, 255, 90);
                break;
            case 2:
                progressLVL1.color = new Color32(255, 255, 255, 255);
                progressLVL2.color = new Color32(255, 255, 255, 255);
                progressLVL3.color = new Color32(255, 255, 255, 90);
                break;
            case 3:
                progressLVL1.color = new Color32(255, 255, 255, 255);
                progressLVL2.color = new Color32(255, 255, 255, 255);
                progressLVL3.color = new Color32(255, 255, 255, 255);
                break;
        }
    }
}
