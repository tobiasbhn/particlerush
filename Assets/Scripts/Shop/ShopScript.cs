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
        UiObjectReferrer.instance.shopWelcomeTextDE.SetActive(false);
        UiObjectReferrer.instance.shopWelcomeTextEN.SetActive(false);
    }

    public void SetButtonTextBuy(bool interactable) {
        UiObjectReferrer.instance.shopBuyButtonDE.SetActive(true);
        UiObjectReferrer.instance.shopBuyButtonEN.SetActive(true);
        UiObjectReferrer.instance.shopUnlockButtonDE.SetActive(false);
        UiObjectReferrer.instance.shopUnlockButtonEN.SetActive(false);
        UiObjectReferrer.instance.shopBuyButtonDE.GetComponent<Button>().interactable = interactable;
        UiObjectReferrer.instance.shopBuyButtonEN.GetComponent<Button>().interactable = interactable;
    }
    public void SetButtonTextUnlock(bool interactable) {
        UiObjectReferrer.instance.shopUnlockButtonDE.SetActive(true);
        UiObjectReferrer.instance.shopUnlockButtonEN.SetActive(true);
        UiObjectReferrer.instance.shopBuyButtonDE.SetActive(false);
        UiObjectReferrer.instance.shopBuyButtonEN.SetActive(false);
        UiObjectReferrer.instance.shopBuyButtonDE.GetComponent<Button>().interactable = interactable;
        UiObjectReferrer.instance.shopBuyButtonEN.GetComponent<Button>().interactable = interactable;
    }

    public void OnEnable() {
        UiObjectReferrer.instance.shopWelcomeTextDE.SetActive(true);
        UiObjectReferrer.instance.shopWelcomeTextEN.SetActive(true);
        ShopScript.instance.SetButtonTextBuy(false);
    }
}
