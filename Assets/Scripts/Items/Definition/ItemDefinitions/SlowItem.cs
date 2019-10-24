using UnityEngine;

[CreateAssetMenu(menuName = "Items/Slow Item")]
public class SlowItem : ItemDefinition {
    public override int getCurrendLVL() {
        return SaveDataManager.getValue.slowItemLVL;
    }
}