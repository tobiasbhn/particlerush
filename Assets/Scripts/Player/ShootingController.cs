using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour {

    //INSTANCE
    [HideInInspector] public static ShootingController instance;
    [HideInInspector] public bool thisScriptLoaded = false;

    //OBJECT_LINKS
    public GameObject projectilePrefab;
    public GameObject projectileParent;

    //PROJECTILES
    private List<GameObject> instantiatedProjectileList;

    void Awake() {
        instance = this;
        instantiatedProjectileList = new List<GameObject>();
        thisScriptLoaded = true;
    }

    public void NewInput(Vector3 _pos) {
        RuntimeDataManager.setValue.projectilesFiredTotal++;

        var currentPlayerRadius = PlayerScript.instance.currentMass * ConstantManager.PLAYER_AMOUNT_TO_GROW_PER_MASS_IN_WORLD_SPACE / 2;
        var targetPos = Camera.main.ScreenToWorldPoint(new Vector3(_pos.x, _pos.y, ConstantManager.CAMERA_DISTANCE_PLAYER));
        var originPos = Vector3.MoveTowards(PlayerScript.instance.playerHolder.transform.position, targetPos, currentPlayerRadius);

        //Get rotation
        float AngleRad = Mathf.Atan2(targetPos.y - originPos.y, targetPos.x - originPos.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        //Get Speed
        var direction = (targetPos - originPos).normalized;
        var addForce = direction * 800;

        //Get Damage
        var oneHundretOfMass = (100f / ((float)ConstantManager.PLAYER_MAX_MESH_GENERATION_SIZE - (float)ConstantManager.PLAYER_MIN_MESH_GENERATION_SIZE));
        var massInPercent = oneHundretOfMass * (PlayerScript.instance.currentMass - ConstantManager.PLAYER_MIN_MESH_GENERATION_SIZE);
        massInPercent = massInPercent <= ConstantManager.PROJECTILE_MIN_DAMAGE_REDUCION_PER_PLAYER_MASS ? ConstantManager.PROJECTILE_MIN_DAMAGE_REDUCION_PER_PLAYER_MASS : massInPercent;
        
        var instantiatedProjectile = Instantiate(projectilePrefab, originPos, Quaternion.Euler(0, 0, AngleDeg));
        instantiatedProjectile.transform.parent = projectileParent.transform;
        instantiatedProjectile.GetComponent<ProjectileScript>().damageToDeal = massInPercent;
        instantiatedProjectile.GetComponent<Rigidbody>().AddForce(addForce);
        instantiatedProjectileList.Add(instantiatedProjectile);
    }

    public void DestroyAllProjectiles() {
        foreach (GameObject projectile in instantiatedProjectileList) 
            GameObject.Destroy(projectile.gameObject);
        instantiatedProjectileList.Clear();
    }
}
