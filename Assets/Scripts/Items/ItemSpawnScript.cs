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
    public float currendTime = 0f;
    public ItemSpawnSceneModi spawnModi;
    public List<GameObject> instantiatedItems;

    void Awake() {
        instance = this;
    }
    void Start() {
        spawnModi = ItemSpawnSceneModi.refuse;
        nextItemAt = Time.time;
        instantiatedItems = new List<GameObject>();
    }

    void Update() {
        currendTime = Time.time;
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
            var itemScript = item.GetComponent<ItemScript>();
            itemScript.speed = ParticleSpawnScript.instance.DefineSpeed();
            itemScript.itemType = DefineItemType();
            instantiatedItems.Add(item);
            RuntimeDataManager.value.itemsSpawned++;
        }
    }

    public void SetupEnabled() {
        nextItemAt = CalculateNextItemTime();
        spawnModi = ItemSpawnSceneModi.allow;
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
        foreach (GameObject item in instantiatedItems) {
            item.GetComponent<ItemScript>().Destroy(false);
        }
        instantiatedItems.Clear();
    }
}
