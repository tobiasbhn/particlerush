using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour {

    //INSTANCE
    [HideInInspector] public static ShootingController instance;

    //OBJECT_LINKS
    public GameObject projectilePrefab;
    public GameObject projectileParent;

    //PROJECTILES
    private List<GameObject> instantiatedProjectileList;

    void Awake() {
        instance = this;
    }
    void Start() {
        instantiatedProjectileList = new List<GameObject>();
    }

    public void NewInput(Vector3 _pos) {
        RuntimeDataManager.value.projectilesFiredTotal++;

        var currentPlayerRadius = PlayerScript.instance.currentMass * ConstantManager.PLAYER_AMOUNT_TO_GROW_PER_MASS_IN_WORLD_SPACE / 2;
        var targetPos = Camera.main.ScreenToWorldPoint(new Vector3(_pos.x, _pos.y, ConstantManager.CAMERA_DISTANCE_PLAYER));
        var originPos = Vector3.MoveTowards(PlayerMovementScript.instance.playerHolder.transform.position, targetPos, currentPlayerRadius);

        //Get rotation
        float AngleRad = Mathf.Atan2(targetPos.y - originPos.y, targetPos.x - originPos.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        //Get Speed
        var direction = (targetPos - originPos).normalized;
        var addForce = direction * 800;

        //Get Damage
        var oneHundretOfMass = (100f / ((float)ConstantManager.PLAYER_MAX_MESH_GENERATION_SIZE - (float)ConstantManager.PLAYER_MIN_MESH_GENERATION_SIZE)); // 100 Percent dividet to the Mass that is possible to gain
        var massInPercent = oneHundretOfMass * (PlayerScript.instance.currentMass - ConstantManager.PLAYER_MIN_MESH_GENERATION_SIZE);
        massInPercent = massInPercent <= ConstantManager.PROJECTILE_MIN_DAMAGE_REDUCION_PER_PLAYER_MASS ? ConstantManager.PROJECTILE_MIN_DAMAGE_REDUCION_PER_PLAYER_MASS : massInPercent;

        var instantiatedProjectile = Instantiate(projectilePrefab, originPos, Quaternion.Euler(0, 0, AngleDeg));
        instantiatedProjectile.transform.parent = projectileParent.transform;
        var instantiatedScript = instantiatedProjectile.GetComponent<ProjectileScript>();
        instantiatedScript.damageToDeal = massInPercent;
        instantiatedScript.rb.AddForce(addForce);
        instantiatedProjectileList.Add(instantiatedProjectile);
    }

    public void DestroyAllProjectiles() {
        if (instantiatedProjectileList != null) {
            foreach (GameObject projectile in instantiatedProjectileList)
                GameObject.Destroy(projectile.gameObject);
            instantiatedProjectileList.Clear();
        }
    }

    public void DemoShooting() {
        StartCoroutine(DemoShootingHelper());
    }
    private IEnumerator DemoShootingHelper() {
        var delay = 1 / ItemPool.instance.shootItemDefinition.getCurrendEffect(+1);
        var lastShoot = Time.realtimeSinceStartup;
        while (OEShopMenu.instance.currentItem == ItemPool.instance.shootItemDefinition) {
            if (Time.realtimeSinceStartup - lastShoot > delay) {
                NewInput(new Vector3(Screen.width / 2, Screen.height / 2));
                lastShoot = Time.realtimeSinceStartup;
            }
            yield return null;
        }
    }
}
