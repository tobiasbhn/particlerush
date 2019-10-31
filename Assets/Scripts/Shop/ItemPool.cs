using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour {

    //INSTANCE
    public static ItemPool instance;
    void Awake() {
        instance = this;
    }

    // ITEM ARRAY
    public ItemDefinition[] items;
    // ITEM POOLS AND SINGLETONS
    public ShrinkItem shrinkItem;
    public ShieldItem shieldItem;
    public ForceItem forceItem;
    public ClearItem clearItem;
    public SlowItem slowItem;
    public SlideItem slideItem;
    public GoldrushItem goldrushItem;

    void Start() {
        items = new ItemDefinition[7];
        items[0] = shrinkItem;
        items[1] = shieldItem;
        items[2] = forceItem;
        items[3] = clearItem;
        items[4] = slowItem;
        items[5] = slideItem;
        items[6] = goldrushItem;
    }
}