using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    [HideInInspector] public float damageToDeal;
    private float spawnTime;

    void Awake() {
        spawnTime = Time.time;
    }

    void Update() {
        // Destroy if Projectile lived for too long
        if (spawnTime + ConstantManager.PROJECTILE_MAX_LIFE_TIME < Time.time) {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible() {
        GameObject.Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        var tag = other.gameObject.tag;
        if (tag == "Particle" && other.GetType() == typeof(SphereCollider)) {
            //Deal 100% Damage if Players Mass is Huge, deal less Damage if Players Mass is not that big
            other.gameObject.GetComponent<ParticleScript>().ShrinkParticle(1f * damageToDeal / 100);
            GameObject.Destroy(this.transform.gameObject);
        }
    }
}
