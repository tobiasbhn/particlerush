using UnityEngine;

[CreateAssetMenu(menuName = "Items/Shield Item")]
public class ShieldItem : ItemDefinition {
    public override int getCurrentLVL() {
        return SaveDataManager.getValue.shieldItemLVL;
    }
    public override void UpdateLevel() {
        if (SaveDataManager.getValue.shieldItemLVL < 3) {
            SaveDataManager.getValue.shieldItemLVL++;
            SaveDataManager.Save();
        }
    }
    public override void showShopDemo() {
        
    }
}
