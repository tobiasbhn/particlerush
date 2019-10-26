using UnityEngine;

[CreateAssetMenu(menuName = "Items/Force Item")]
public class ForceItem : ItemDefinition {
    public override int getCurrendLVL() {
        return SaveDataManager.getValue.forceItemLVL;
    }
}
