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
        PlayerMovementScript.instance.swipeItemEffect = getCurrendEffect(+1);
        PlayerMovementScript.instance.allowSwipe = true;
        UiSceneScript.instance.EnableOnlySwipeBar();
    }
}
