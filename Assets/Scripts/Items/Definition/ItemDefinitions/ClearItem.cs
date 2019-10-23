using UnityEngine;

[CreateAssetMenu(menuName = "Items/Clear Item")]
public class ClearItem : ItemDefinition {

    public int GetCurrendLVL() {
        return SaveDataManager.getValue.clearItmLVL;
    }

    public void Setup() {
        var currentLVL = SaveDataManager.getValue.clearItmLVL;
    }

    public void BuyItem() {
        // var maxLVL = base.getMaxLVL;
        // var currentLVL = SaveDataManager.getValue.clearItmLVL;
        // if (currentLVL < maxLVL) {
        //     SaveDataManager.getValue.clearItmLVL++;
        //     SaveDataManager.Save();
        // }
    }
}
