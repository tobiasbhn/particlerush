using UnityEngine;

[CreateAssetMenu(menuName = "Items/Slide Item")]
public class SlideItem : ItemDefinition {
    public override int getCurrendLVL() {
        return SaveDataManager.getValue.slideItemLVL;
    }
}
