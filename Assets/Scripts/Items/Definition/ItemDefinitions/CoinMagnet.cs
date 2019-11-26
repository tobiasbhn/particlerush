using UnityEngine;

[CreateAssetMenu(menuName = "Items/Coin Magnet")]
public class CoinMagnet : ItemDefinition {
    public override int getCurrentLVL() {
        return SaveDataManager.getValue.coinItemLVL;
    }
    public override void UpdateLevel() {
        if (SaveDataManager.getValue.coinItemLVL < 3) {
            SaveDataManager.getValue.coinItemLVL++;
            SaveDataManager.Save();
        }
    }
    public override void showShopDemo() {
        
    }
}
