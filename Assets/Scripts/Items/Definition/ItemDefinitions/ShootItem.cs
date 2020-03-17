using UnityEngine;

[CreateAssetMenu(menuName = "Items/Shoot Item")]
public class ShootItem : ItemDefinition {
    public override int getCurrentLVL() {
        return SaveDataManager.getValue.shootItemLVL;
    }
    public override void UpdateLevel() {
        if (SaveDataManager.getValue.shootItemLVL < 3) {
            SaveDataManager.getValue.shootItemLVL++;
            SaveDataManager.Save();
        }
    }
    public override void showShopDemo() {
        // ShootingController.instance.DemoShooting();
        ParticleSceneSetup.instance.SetupIngame();
        ParticleSpawnScript.instance.spawnModi = ParticleSpawnModi.onlyNorm;
        PlayerMovementScript.instance.allowTab = true;
        PlayerMovementScript.instance.shootItemEffect = 1f / (float)getCurrendEffect(+1);
    }
}
