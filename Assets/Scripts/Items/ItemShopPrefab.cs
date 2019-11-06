using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopPrefab : MonoBehaviour {

    public ItemDefinition itemDefinition;
    public Text itemPriceHolder;
    public Text itemNameHolder;
    public RawImage itemIconHolder;
    public Text itemInfoHolder;
    public GameObject lockedScreen;
    public GameObject activeScreen;

    void OnEnable() {
        HideItemInfos();
        UpdateStaticItemInfos();
        UpdateLvlBasedItemInfos();
    }
    
    // on select item in Shop-Menu (Button Press on Item)
    public void ShowItemInfos() {
        ShopScript.instance.DisableAllItems();
        itemInfoHolder.gameObject.SetActive(true);
        activeScreen.SetActive(true);
        // itemDefinition.GetScenario().callScenario();

        if (itemDefinition.getCurrendLVL() == 0)
            ShopScript.instance.SetButtonTextUnlock(true);
        else
            ShopScript.instance.SetButtonTextBuy(true);
    }
    // on other Item gets Selected or No Item should be selected (e.g. coming to shop from menu)
    public void HideItemInfos() {
        itemInfoHolder.gameObject.SetActive(false);
        activeScreen.SetActive(false);
    }


    // Setup everything that is static, like Item Name and Item Description (never Change, no matter which LVL)
    void UpdateStaticItemInfos() {
        itemIconHolder.texture = itemDefinition.getIconAsTexture();
        itemInfoHolder.text = itemDefinition.getDescription();
        itemNameHolder.text = itemDefinition.getName();
    }

    // Setup everything that depends on the LVL, like Price, Locked-State etc.
    void UpdateLvlBasedItemInfos() {
        itemPriceHolder.text = itemDefinition.getCurrendPrice();

        var LVL = itemDefinition.getCurrendLVL();
        if (LVL == 0)
            lockedScreen.SetActive(true);
        else
            lockedScreen.SetActive(false);
    }
}
