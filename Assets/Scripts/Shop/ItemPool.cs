using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour {
    
    //INSTANCE
    public static ItemPool instance;

    //ITEM DEFINITIONS
    public ItemDefinition shootItemDefinition;
    public ItemDefinition swipeItemDefinition;
    public ItemDefinition shieldItemDefinition;
    public ItemDefinition coinMagnetItemDefinition;
    public ItemDefinition secondChanceItemDefinition;
    public ItemDefinition shrinkItemDefinition;

    // ITME SHOP PREFAB
    public ItemShopPrefab[] itemShopPrefabs;

    void Awake() {
        instance = this;
    }
}
