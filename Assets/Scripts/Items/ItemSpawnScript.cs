using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnScript : MonoBehaviour {

    // INSTANCE
    [HideInInspector] public static ItemSpawnScript instance;

    // OPJECT-LINKS
    public GameObject itemPrefab;
    public GameObject itemDirectory;

    // BEHAVIOUR
    public float nextItemAt = 0f;
    public ItemSpawnSceneModi spawnModi;
    public List<ItemIngamePrefab> instantiatedItems;
    public ItemIngamePrefab[] itemSlots = new ItemIngamePrefab[3];
    [HideInInspector] public Vector3[] itemSlotPositions = new Vector3[3];

    void Awake() {
        instance = this;
    }
    void Start() {
        spawnModi = ItemSpawnSceneModi.refuse;
        nextItemAt = Time.time;
        instantiatedItems = new List<ItemIngamePrefab>();
    }

    void Update() {
        if (spawnModi == ItemSpawnSceneModi.allow && nextItemAt <= Time.time) {
            // spawn Item
            nextItemAt = CalculateNextItemTime();
            Vector3 randomItemSpawnPosition = new Vector3(
                    Random.Range(ConstantManager.CAMERA_LOWER_LEFT_CORNER_IN_WORD_SPACE.x, ConstantManager.CAMERA_UPPER_RIGHT_CORNER_IN_WORLD_SPACE.x),
                    ConstantManager.CAMERA_LOWER_LEFT_CORNER_IN_WORD_SPACE.y - (ConstantManager.CAMERA_SCREEN_HEIGHT_IN_WORLD_SPACE / 5),
                    ConstantManager.CAMERA_LOWER_LEFT_CORNER_IN_WORD_SPACE.z
            );
            var item = Instantiate(itemPrefab, randomItemSpawnPosition, Quaternion.identity);
            item.transform.parent = itemDirectory.transform;
            var itemScript = item.GetComponent<ItemIngamePrefab>();
            itemScript.speed = ParticleSpawnScript.instance.DefineSpeed();
            itemScript.itemType = DefineItemType();
            instantiatedItems.Add(itemScript);
            RuntimeDataManager.value.itemsSpawned++;
        }
    }

    public void SetupEnabled() {
        nextItemAt = CalculateNextItemTime();
        spawnModi = ItemSpawnSceneModi.allow;
        if (SaveDataManager.getValue.settingsItemPosition == SettingsItemPosition.Left) {
            var left1 = UiObjectReferrer.instance.ingameItemSlotsLeft1.transform.position;
            var left2 = UiObjectReferrer.instance.ingameItemSlotsLeft2.transform.position;
            var left3 = UiObjectReferrer.instance.ingameItemSlotsLeft3.transform.position;
            itemSlotPositions[0] = Camera.main.ScreenToWorldPoint(new Vector3(left1.x, left1.y, ConstantManager.CAMERA_DISTANCE_PLAYER));
            itemSlotPositions[1] = Camera.main.ScreenToWorldPoint(new Vector3(left2.x, left2.y, ConstantManager.CAMERA_DISTANCE_PLAYER));
            itemSlotPositions[2] = Camera.main.ScreenToWorldPoint(new Vector3(left3.x, left3.y, ConstantManager.CAMERA_DISTANCE_PLAYER));
        } else if (SaveDataManager.getValue.settingsItemPosition == SettingsItemPosition.Right) {
            var right1 = UiObjectReferrer.instance.ingameItemSlotsRight1.transform.position;
            var right2 = UiObjectReferrer.instance.ingameItemSlotsRight2.transform.position;
            var right3 = UiObjectReferrer.instance.ingameItemSlotsRight3.transform.position;
            itemSlotPositions[0] = Camera.main.ScreenToWorldPoint(new Vector3(right1.x, right1.y, ConstantManager.CAMERA_DISTANCE_PLAYER));
            itemSlotPositions[1] = Camera.main.ScreenToWorldPoint(new Vector3(right2.x, right2.y, ConstantManager.CAMERA_DISTANCE_PLAYER));
            itemSlotPositions[2] = Camera.main.ScreenToWorldPoint(new Vector3(right3.x, right3.y, ConstantManager.CAMERA_DISTANCE_PLAYER));
        }
    }
    public void SetupDisabled() {
        DestroyAllItems();
        spawnModi = ItemSpawnSceneModi.refuse;
    }

    private float CalculateNextItemTime() {
        var time = Time.time;
        var ranom = ConstantManager.ITEM_SPAWN_RANDOM_FACTOR;
        var add = ConstantManager.ITEM_SPAWN_DELAY + Random.Range(-ranom, ranom);
        time += add;
        return time;
    }
    private ItemDefinition DefineItemType() {
        ItemDefinition item = null;
        while (item == null) {
            var random = Random.Range(0, ItemPool.instance.items.Length);
            var tempItem = ItemPool.instance.items[random];
            if (tempItem.getCurrendLVL() > 0)
                item = tempItem;
        }
        return item;
    }

    public void DestroyAllItems() {
        foreach (ItemIngamePrefab item in instantiatedItems) {
            item.Destroy(false);
        }
        instantiatedItems.Clear();
    }
}
