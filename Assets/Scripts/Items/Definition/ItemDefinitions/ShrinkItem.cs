using UnityEngine;

[CreateAssetMenu(menuName = "Items/Shrink Item")]
public class ShrinkItem : ItemDefinition {
    public override int getCurrendLVL() {
        return SaveDataManager.getValue.shrinkItemLVL;
    }
}
