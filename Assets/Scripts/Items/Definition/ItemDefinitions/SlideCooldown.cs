using UnityEngine;

[CreateAssetMenu(menuName = "Items/Slide Cooldown")]
public class SlideCooldown : ItemDefinition {
    public override int getCurrentLVL() {
        return SaveDataManager.getValue.slideItemLVL;
    }
    public override void UpdateLevel() {
        if (SaveDataManager.getValue.slideItemLVL < 3) {
            SaveDataManager.getValue.slideItemLVL++;
            SaveDataManager.Save();
        }
    }
    public override void showShopDemo() {
        
    }
}
