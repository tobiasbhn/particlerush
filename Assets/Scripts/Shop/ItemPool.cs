using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour {

    //INSTANCE
    public static ItemPool instance;
    void Awake() {
        instance = this;
    }

    //ITEM POOLS AND SINGLETONS
    public ShrinkItem shrinkItem;
    public ShieldItem shieldItem;
    public ForceItem forceItem;
    public ClearItem clearItem;
    public SlowItem slowItem;
    public SlideItem slideItem;
    public GoldrushItem goldrushItem;
}