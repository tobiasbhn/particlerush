using UnityEngine;

[CreateAssetMenu(menuName = "Items/Second Chance")]
public class SecondChance : ItemDefinition {
    public override int getCurrentLVL() {
        return SaveDataManager.getValue.secondChanceItemLVL;
    }
    public override void UpdateLevel() {
        if (SaveDataManager.getValue.secondChanceItemLVL < 3) {
            SaveDataManager.getValue.secondChanceItemLVL++;
            SaveDataManager.Save();
        }
    }
    public override void showShopDemo() {
        
    }
}
