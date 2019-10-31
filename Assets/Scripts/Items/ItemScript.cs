using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class ItemScript : MonoBehaviour {

    // OBJECT-LINKS
    private Rigidbody rb;
    public GameObject border;
    public GameObject icon;

    // BEHAVIOUR
    [HideInInspector] public float speed = 0;
    [HideInInspector] public bool dragable = true;
    [HideInInspector] public bool activateOnDrop = false; // wether the Item should be activatet on Drop or not
    [HideInInspector] public ItemDefinition itemType;

    // VARS
    [HideInInspector] public bool activeDragDrop = false;
    private bool currendAnimationActivated = false;
    private Coroutine calledAnimation;


    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(0, speed, 0);
        icon.GetComponent<SpriteRenderer>().sprite = itemType.getIconAsSprite();
        DefineSize();

    }

    // Update is called once per frame
    void Update() {
        if (activateOnDrop && !currendAnimationActivated)
            calledAnimation = StartCoroutine(AnimateItemActive());
        else if (!activateOnDrop && currendAnimationActivated)
            calledAnimation = StartCoroutine(AnimateItemInactive());
    }

    private void DefineSize() {
        var size = 20f * ConstantManager.PLAYER_AMOUNT_TO_GROW_PER_MASS_IN_WORLD_SPACE;
        transform.localScale = new Vector3(size, size, size);
    }

    public void DragItem(Vector2 pos) {
        PlayerMovementScript.instance.DenyInput(true);
        rb.velocity = Vector3.zero;
        var touchInWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, ConstantManager.CAMERA_DISTANCE_PLAYER));
        this.transform.position = touchInWorldSpace;
        activateOnDrop = !TouchIsOnItemSlot(pos);
    }
    public void DropItem(Vector2 pos) {
        if (TouchIsOnItemSlot(pos)) {
            ItemSnapToItemSlots();
        } else {
            ItemActivate();
        }
        PlayerMovementScript.instance.DenyInput(false);
    }

    private bool TouchIsOnItemSlot(Vector2 pos) {
        PointerEventData cursor = new PointerEventData(EventSystem.current);
        cursor.position = pos;
        List<RaycastResult> hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(cursor, hits);
        var hitItem = false;

        foreach (RaycastResult hit in hits)
            if (hit.gameObject.transform.tag == "Item Slots")
                hitItem = true;

        return hitItem;
    }



    private void ItemSnapToItemSlots() {
        Debug.Log("Item Snap to Item-Slot");
    }
    private void ItemActivate() {
        Destroy(true);
        RuntimeDataManager.value.itemsUsed++;
        Debug.Log("Item Activate");
    }



    private IEnumerator AnimateItemActive() {
        if (calledAnimation != null)
            StopCoroutine(calledAnimation);
        currendAnimationActivated = true;
        var scale = border.transform.localScale;
        var factor = Time.deltaTime * ConstantManager.ITEM_ACTIVE_ANIMATION_SPEED;
        while (scale.x < ConstantManager.ITEM_ACTIVE_ANIMATION_CIRCE_SIZE) {
            scale = new Vector3(scale.x + factor, scale.y + factor, scale.z + factor);
            border.transform.localScale = scale;
            yield return null;
        }
        border.transform.localScale = new Vector3(ConstantManager.ITEM_ACTIVE_ANIMATION_CIRCE_SIZE, ConstantManager.ITEM_ACTIVE_ANIMATION_CIRCE_SIZE, ConstantManager.ITEM_ACTIVE_ANIMATION_CIRCE_SIZE);
    }
    private IEnumerator AnimateItemInactive() {
        if (calledAnimation != null)
            StopCoroutine(calledAnimation);
        currendAnimationActivated = false;
        var scale = border.transform.localScale;
        var factor = Time.deltaTime * ConstantManager.ITEM_ACTIVE_ANIMATION_SPEED;
        while (scale.x > 0f) {
            scale = new Vector3(scale.x - factor, scale.y - factor, scale.z - factor);
            border.transform.localScale = scale;
            yield return null;
        }
        border.transform.localScale = new Vector3(0f, 0f, 0f);
    }




    private void OnBecameInvisible() {
        Destroy(true);
    }
    public void Destroy(bool deleteFromList) {
        GameObject.Destroy(this.transform.gameObject);
        if (deleteFromList)
            ItemSpawnScript.instance.instantiatedItems.Remove(this.gameObject);
    }
}
