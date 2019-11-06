using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour {

    //INSTANCES
    public static ShopScript instance;

    void Awake() {
        instance = this;
    }

    public void DisableAllItems() {
        foreach (ItemShopPrefab item in UiObjectReferrer.instance.shopItems) {
            item.HideItemInfos();
        }
        UiObjectReferrer.instance.shopWelcomeTextDE.gameObject.SetActive(false);
        UiObjectReferrer.instance.shopWelcomeTextEN.gameObject.SetActive(false);
    }

    public void SetButtonTextBuy(bool interactable) {
        UiObjectReferrer.instance.shopBuyButtonDE.gameObject.SetActive(true);
        UiObjectReferrer.instance.shopBuyButtonEN.gameObject.SetActive(true);
        UiObjectReferrer.instance.shopUnlockButtonDE.gameObject.SetActive(false);
        UiObjectReferrer.instance.shopUnlockButtonEN.gameObject.SetActive(false);
        UiObjectReferrer.instance.shopBuyButtonDE.interactable = interactable;
        UiObjectReferrer.instance.shopBuyButtonEN.interactable = interactable;
    }
    public void SetButtonTextUnlock(bool interactable) {
        UiObjectReferrer.instance.shopUnlockButtonDE.gameObject.SetActive(true);
        UiObjectReferrer.instance.shopUnlockButtonEN.gameObject.SetActive(true);
        UiObjectReferrer.instance.shopBuyButtonDE.gameObject.SetActive(false);
        UiObjectReferrer.instance.shopBuyButtonEN.gameObject.SetActive(false);
        UiObjectReferrer.instance.shopBuyButtonDE.interactable = interactable;
        UiObjectReferrer.instance.shopBuyButtonEN.interactable = interactable;
    }

    public void OnEnable() {
        UiObjectReferrer.instance.shopWelcomeTextDE.gameObject.SetActive(true);
        UiObjectReferrer.instance.shopWelcomeTextEN.gameObject.SetActive(true);
        ShopScript.instance.SetButtonTextBuy(false);
    }
}
