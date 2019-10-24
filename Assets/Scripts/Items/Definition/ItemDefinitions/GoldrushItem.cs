using UnityEngine;

[CreateAssetMenu(menuName = "Items/Goldrush Item")]
public class GoldrushItem : ItemDefinition {
    public override int getCurrendLVL() {
        return SaveDataManager.getValue.goldrushItemLVL;
    }
}
