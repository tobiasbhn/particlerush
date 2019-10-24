using UnityEngine;

[CreateAssetMenu(menuName = "Items/Clear Item")]
public class ClearItem : ItemDefinition {
    public override int getCurrendLVL() {
        return SaveDataManager.getValue.clearItmLVL;
    }
}
