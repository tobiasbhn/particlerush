using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class ItemIngamePrefab : MonoBehaviour {

    // OBJECT-LINKS
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject border;
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private LineRenderer line;

    // BEHAVIOUR
    [HideInInspector] public float speed = 0;
    [HideInInspector] public bool dragable = true;
    // private ItemActionOnDrop previousAction = ItemActionOnDrop.inactive;
    [HideInInspector] public ItemDefinition itemType;

    // VARS
    [HideInInspector] public bool activeDragDrop = false;
    private bool currendAnimationActivated = false;
    private Coroutine calledAnimation;
    private float creationTime;

    // ITEM SLOT VARS
    private ItemActionOnDrop actionOnDrop = ItemActionOnDrop.inactive;
    private string currentHit = ""; // The Current UI Hit from Raycast - either Slot 1 - 3 or "Active" 
    private Vector2 positionOnNotActivte;


    void Start() {
        rb.AddForce(0, speed, 0);
        icon.sprite = itemType.getIconAsSprite();
        creationTime = Time.realtimeSinceStartup;
        DefineSize();
    }

    // ON TRIGGER
    private void OnTriggerEnter(Collider other) {
        var tag = other.gameObject.transform.tag;
        if (tag == "Player" && other.GetType() == typeof(MeshCollider) && actionOnDrop == ItemActionOnDrop.inactive) {
            SetToNextFreeSlot(new int[3] { 0, 1, 2 }, true);
            rb.velocity = Vector3.zero;
            this.transform.position = positionOnNotActivte;
        }
    }

    // Update is called once per frame
    void Update() {
        if (actionOnDrop == ItemActionOnDrop.activate && !currendAnimationActivated) {
            // Activate Animation if not done jet and action is active
            calledAnimation = StartCoroutine(AnimateItemActive());
        } else if (actionOnDrop != ItemActionOnDrop.activate && currendAnimationActivated) {
            // Disable Animation if not done jet and action is not active
            calledAnimation = StartCoroutine(AnimateItemInactive());
        }
        if (actionOnDrop == ItemActionOnDrop.inactive) {
            positionOnNotActivte = transform.position;
        }

        if (itemType.GetAnimationType() == ItemAnimationType.line && actionOnDrop == ItemActionOnDrop.activate && line.positionCount == 2) {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, PlayerScript.instance.gameObject.transform.position);
        }
    }

    public void DragItem(Vector2 pos) {
        PlayerMovementScript.instance.DenyInput(true);
        rb.velocity = Vector3.zero;
        CheckPosition(pos);

        if (actionOnDrop == ItemActionOnDrop.activate || actionOnDrop == ItemActionOnDrop.blocked) {
            var touchInWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, ConstantManager.CAMERA_DISTANCE_PLAYER));
            this.transform.position = touchInWorldSpace;
        } else {
            this.transform.position = positionOnNotActivte;
        }
    }

    private void CheckPosition(Vector2 touchPos) {
        PointerEventData cursor = new PointerEventData(EventSystem.current);
        cursor.position = touchPos;
        List<RaycastResult> hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(cursor, hits);
        bool hitSlot = false;

        foreach (RaycastResult hit in hits) {
            var tag = hit.gameObject.transform.tag;
            if (tag == "Slot 1" || tag == "Slot 2" || tag == "Slot 3") {
                hitSlot = true;
                if (tag != currentHit) {
                    // THE TOUCH INPUT HAS MOVED TO ANOTHER ITEM SLOT - CALLED ONCE EVERY SWITCH
                    currentHit = tag;
                    if (tag == "Slot 1") {
                        SetToNextFreeSlot(new int[3] { 0, 1, 2 }, false);
                    } else if (tag == "Slot 2") {
                        SetToNextFreeSlot(new int[3] { 1, 0, 2 }, false);
                    } else if (tag == "Slot 3") {
                        SetToNextFreeSlot(new int[3] { 2, 1, 0 }, false);
                    }
                }
            }
        }
        if (hitSlot == false && currentHit != "Active") {
            // THE TOUCH INPUT HAS LEFT THE ITEMS SLOTS ON IS BACK ON FREE SPACE
            currentHit = "Active";
            ClearItemSlotsFromThis();
            actionOnDrop = ItemActionOnDrop.activate;
        }
    }
    private void ClearItemSlotsFromThis() {
        if (ItemSpawnScript.instance.itemSlots[0] == this)
            ItemSpawnScript.instance.itemSlots[0] = null;
        else if (ItemSpawnScript.instance.itemSlots[1] == this)
            ItemSpawnScript.instance.itemSlots[1] = null;
        else if (ItemSpawnScript.instance.itemSlots[2] == this)
            ItemSpawnScript.instance.itemSlots[2] = null;
    }

    private void SetToNextFreeSlot(int[] order, bool destroyIfNoSlotFree) {
        ClearItemSlotsFromThis();
        var helper = new ItemActionOnDrop[3] { ItemActionOnDrop.slot1, ItemActionOnDrop.slot2, ItemActionOnDrop.slot3 };

        if (ItemSpawnScript.instance.itemSlots[order[0]] == null) {
            ItemSpawnScript.instance.itemSlots[order[0]] = this;
            positionOnNotActivte = ItemSpawnScript.instance.itemSlotPositions[order[0]];
            actionOnDrop = helper[order[0]];
        } else if (ItemSpawnScript.instance.itemSlots[order[1]] == null) {
            ItemSpawnScript.instance.itemSlots[order[1]] = this;
            positionOnNotActivte = ItemSpawnScript.instance.itemSlotPositions[order[1]];
            actionOnDrop = helper[order[1]];
        } else if (ItemSpawnScript.instance.itemSlots[order[2]] == null) {
            ItemSpawnScript.instance.itemSlots[order[2]] = this;
            positionOnNotActivte = ItemSpawnScript.instance.itemSlotPositions[order[2]];
            actionOnDrop = helper[order[2]];
        } else {
            if (destroyIfNoSlotFree)
                Destroy(true);
            else
                actionOnDrop = ItemActionOnDrop.blocked;
        }
    }


    public void DropItem() {
        if (actionOnDrop == ItemActionOnDrop.activate) {
            ItemActivate();
        } else if (actionOnDrop == ItemActionOnDrop.blocked) {
            transform.position = positionOnNotActivte;
            actionOnDrop = ItemActionOnDrop.inactive;
            rb.AddForce(0, speed, 0);
        }
        PlayerMovementScript.instance.DenyInput(false);
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

        if (itemType.GetAnimationType() == ItemAnimationType.circle) {
            var factor = Time.deltaTime * ConstantManager.ITEM_ACTIVE_ANIMATION_SPEED;
            var scale = border.transform.localScale;
            while (scale.x < ConstantManager.ITEM_ACTIVE_ANIMATION_CIRCE_SIZE) {
                scale = new Vector3(scale.x + factor, scale.y + factor, scale.z + factor);
                border.transform.localScale = scale;
                yield return null;
            }
            border.transform.localScale = new Vector3(ConstantManager.ITEM_ACTIVE_ANIMATION_CIRCE_SIZE, ConstantManager.ITEM_ACTIVE_ANIMATION_CIRCE_SIZE, ConstantManager.ITEM_ACTIVE_ANIMATION_CIRCE_SIZE);
        } else if (itemType.GetAnimationType() == ItemAnimationType.line) {
            line.startWidth = ConstantManager.PLAYER_AMOUNT_TO_GROW_PER_MASS_IN_WORLD_SPACE / 2;
            line.endWidth = ConstantManager.PLAYER_AMOUNT_TO_GROW_PER_MASS_IN_WORLD_SPACE / 2;
            line.positionCount = 2;
        }
        yield return null;
    }
    private IEnumerator AnimateItemInactive() {
        if (calledAnimation != null)
            StopCoroutine(calledAnimation);
        currendAnimationActivated = false;

        if (itemType.GetAnimationType() == ItemAnimationType.circle) {
            var factor = Time.deltaTime * ConstantManager.ITEM_ACTIVE_ANIMATION_SPEED;
            var scale = border.transform.localScale;
            while (scale.x > 0f) {
                scale = new Vector3(scale.x - factor, scale.y - factor, scale.z - factor);
                border.transform.localScale = scale;
                yield return null;
            }
            border.transform.localScale = new Vector3(0f, 0f, 0f);
        } else if (itemType.GetAnimationType() == ItemAnimationType.line) {
            line.positionCount = 0;
        }
        yield return null;
    }

    private void DefineSize() {
        var size = 20f * ConstantManager.PLAYER_AMOUNT_TO_GROW_PER_MASS_IN_WORLD_SPACE;
        transform.localScale = new Vector3(size, size, size);
    }




    private void OnBecameInvisible() {
        if (Time.realtimeSinceStartup - creationTime > 5f)
            Destroy(true);
    }
    public void Destroy(bool deleteFromList) {
        GameObject.Destroy(this.transform.gameObject);
        if (deleteFromList)
            ItemSpawnScript.instance.instantiatedItems.Remove(this);
    }
}
