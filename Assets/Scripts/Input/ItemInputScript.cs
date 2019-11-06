using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInputScript : MonoBehaviour {

    // INSTANCE
    [HideInInspector] public static ItemInputScript instance;

    // BEHAVIOUR
    private ItemInputSceneModis modi;

    // VARS
    private ItemIngamePrefab item = null;
    private float touchStart = 0f;

    void Awake() {
        instance = this;
    }
    void Start() {
        modi = ItemInputSceneModis.refuse;
    }
    void Update() {
        if (modi == ItemInputSceneModis.allow && Input.touchCount > 0) {
            var touch = Input.touches[0];
            
            // If no other Item is currently draged
            if (item == null) {
                var tempItem = TouchIsOnItem(touch.position);
                if (touch.phase == TouchPhase.Began && tempItem != null && tempItem.dragable) {
                    // Touch Began on Item
                    touchStart = Time.realtimeSinceStartup;
                } else if (touch.phase == TouchPhase.Ended) {
                    // Touch Ended before Min-Drag-Time passed - maybe accidently tapped
                    touchStart = Time.realtimeSinceStartup;
                } else if (tempItem != null && tempItem.dragable && Time.realtimeSinceStartup - touchStart > ConstantManager.ITEM_INPUT_DELAY_TO_DRAG) {
                    // Any other Touch on Item bevor Item deklaration, that is a real Drag-Wish and not only accidently tapped
                    item = tempItem;
                    Time.timeScale = ConstantManager.ITEM_TIME_SCALE_ON_DRAG_DROP;
                    item.activeDragDrop = true;
                    item.DragItem(touch.position);
                }
            // If currently dragging an Item
            } else if (item != null) {
                // Drag Item
                if (touch.phase == TouchPhase.Moved && item.activeDragDrop) {
                    item.DragItem(touch.position);
                // Drop Item
                } else if (touch.phase == TouchPhase.Ended && item.activeDragDrop) {
                    Time.timeScale = 1f;
                    item.DropItem();
                    item.activeDragDrop = false;
                    item = null;
                }
            }
        }
    }

    private ItemIngamePrefab TouchIsOnItem(Vector2 pos) {
        RaycastHit[] hits;
        var ray = Camera.main.ScreenPointToRay(new Vector3(pos.x, pos.y, 0));
        hits = Physics.RaycastAll(ray.origin, ray.direction);

        foreach (RaycastHit hit in hits)
            if (hit.collider != null && hit.collider.gameObject.tag == "Item" && hit.collider.GetType() == typeof(SphereCollider))
                return hit.collider.gameObject.GetComponent<ItemIngamePrefab>();
        return null;
    }


    public void SetupEnable() {
        modi = ItemInputSceneModis.allow;
    }
    public void SetupDisable() {
        modi = ItemInputSceneModis.refuse;
    }
}
