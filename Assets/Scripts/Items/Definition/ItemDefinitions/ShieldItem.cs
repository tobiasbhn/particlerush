using UnityEngine;

[CreateAssetMenu(menuName = "Items/Shield Item")]
public class ShieldItem : ItemDefinition {
    public override int getCurrendLVL() {
        return SaveDataManager.getValue.shieldItemLVL;
    }
}
