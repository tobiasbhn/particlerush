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
        ParticleSceneSetup.instance.SetupIngame();
        ParticleSpawnScript.instance.spawnModi = ParticleSpawnModi.onlyGold;
        PlayerScript.instance.coinMagnetEffectFactor = itemEffects[2];
        PlayerScript.instance.sphereCollider.radius = 1f + ((float)itemEffects[2] / 100);
    }
}
