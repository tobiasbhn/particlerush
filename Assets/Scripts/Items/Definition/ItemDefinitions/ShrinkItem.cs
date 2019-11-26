using UnityEngine;

[CreateAssetMenu(menuName = "Items/Shrink Item")]
public class ShrinkItem : ItemDefinition {
    public override int getCurrentLVL() {
        return SaveDataManager.getValue.shrinkItemLVL;
    }
    public override void UpdateLevel() {
        if (SaveDataManager.getValue.shrinkItemLVL < 3) {
            SaveDataManager.getValue.shrinkItemLVL++;
            SaveDataManager.Save();
        }
    }
    public override void showShopDemo() {
        ParticleSceneSetup.instance.SetupIngame();
        ParticleSpawnScript.instance.spawnModi = ParticleSpawnModi.onlyNorm;
        PlayerScript.instance.playerAllowGrow = true;
        PlayerScript.instance.playerAllowShrink = true;
        PlayerScript.instance.shrinkEffectFactor = itemEffects[3];
    }
}
