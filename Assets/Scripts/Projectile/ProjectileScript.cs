﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    //OBJECT LINKS
    public Rigidbody rb;

    // VARS
    [HideInInspector] public float damageToDeal;
    private float spawnTime;

    void Start() {
        spawnTime = Time.time;
        SoundScript.Shoot();
        DefineWidth();
    }

    private void DefineWidth() {
        var maxWidth = ConstantManager.PROJECTILE_MAX_WIDTH - ConstantManager.PROJECTILE_MIN_WIDTH;
        var useWidth = maxWidth / 100f * damageToDeal;
        useWidth += ConstantManager.PROJECTILE_MIN_WIDTH;
        transform.localScale = new Vector3(transform.localScale.x, useWidth, transform.localScale.z);
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
            RuntimeDataManager.value.projectilesHitTotal++;
            //Deal 100% Damage if Players Mass is Huge, deal less Damage if Players Mass is not that big
            other.gameObject.GetComponent<ParticleScript>().ShrinkParticle(ConstantManager.PROJECTILE_DAMAGE_FACTOR * damageToDeal / 100);
            GameObject.Destroy(this.transform.gameObject);
        }
    }
}
