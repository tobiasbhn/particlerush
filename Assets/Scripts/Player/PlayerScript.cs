﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(PlayerMeshGenerator))]
public class PlayerScript : MonoBehaviour {

    //INSTANCE
    [HideInInspector] public static PlayerScript instance;

    //OBJECT-LINKS
    public PlayerMeshGenerator playerMeshGenerator;
    public ShieldScript playerShield;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshCollider meshCollider;
    public SphereCollider sphereCollider;
    [SerializeField] private MeshRenderer meshRenderer;


    //MESH-RELATED
    private UnityEngine.Mesh mesh;

    //BEHAVIUR
    [HideInInspector] public bool playerAllowGrow = false;
    [HideInInspector] public bool playerAllowShrink = false;
    [HideInInspector] public bool playerAllowRotate = false;
    [HideInInspector] public bool playerAllowWaves = false;
    [HideInInspector] public int playerRotationSpeed = 0;

    //MASS RELATIVE
    [HideInInspector] public float targetMass = 0f;
    [HideInInspector] public float currentMass = 0f;
    [HideInInspector] public float shrinkEffectFactor = 0f;
    [HideInInspector] public float coinMagnetEffectFactor = 0f;



    //GENERAL FUNCTIONS
    void Awake() {
        instance = this;
    }
    void Start() {
        playerMeshGenerator.unityMesh = new UnityEngine.Mesh();
        playerShield.Setup();
        mesh = playerMeshGenerator.unityMesh;
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
        meshRenderer.enabled = false;
        this.transform.Rotate(90, 0, 0);
    }
    void Update() {
        //SHRINK ITEM
        if (playerAllowShrink) {
            SetTargetMass(targetMass - shrinkEffectFactor * Time.deltaTime);
        }
        //ROTATION
        if (mesh != null && playerAllowRotate) {
            this.transform.Rotate(new Vector3(0, playerRotationSpeed * Time.deltaTime, 0));
        }
        //SIZE
        if (mesh != null) {
            MassRoutine();
        }
        //WAVES
        if (mesh != null && playerAllowWaves) {
            playerMeshGenerator.UpdateWaves();
        }
    }

    //HIDE AND SHOW PLAYER
    public void HidePlayer() {
        meshRenderer.enabled = false;
        meshCollider.enabled = false;
    }
    public void ShowPlayer() {
        meshRenderer.enabled = true;
        meshCollider.enabled = true;
    }

    //EVERYTHING ABOUT MASS - GROW AND SHRINK
    public void SetTargetMass(float _targetMass) {
        SetTargetMass(_targetMass, false);
    }
    public void SetTargetMass(float _targetMass, bool _force) {
        if ((!_force && playerAllowShrink && (_targetMass < targetMass)) || //Would shrink and shrink is allowed, not forced
            (!_force && playerAllowGrow && (_targetMass > targetMass)) || //Would grow and grow is allowed, not forced
            _force) { //forced
            targetMass = _targetMass < ConstantManager.PLAYER_MIN_MESH_GENERATION_SIZE ? (float)ConstantManager.PLAYER_MIN_MESH_GENERATION_SIZE : _targetMass;
        }
    }

    private void MassRoutine() {
        var sizeDifference = targetMass - currentMass; // positive when to small, negative when to big
        if (sizeDifference >= ConstantManager.PLAYER_MAX_SIZE_DIFFERENCE_TO_END_ANIMATION
                || sizeDifference <= -ConstantManager.PLAYER_MAX_SIZE_DIFFERENCE_TO_END_ANIMATION) {
            currentMass += sizeDifference / (float)ConstantManager.PLAYER_SIZE_ANIMATION_DURATION * Time.deltaTime * 60;

            playerMeshGenerator.CreateShape(currentMass);
            ResizeActualPlayerSizeInWorld();

            var sizeTrend = targetMass - currentMass;
            if (currentMass >= ConstantManager.PLAYER_MAX_MESH_GENERATION_SIZE && sizeTrend >= 0) {
                // WOULD LOOSE
                if (SaveDataManager.getValue.gameStatus == GameStatus.ingame) {
                    var effekt = ItemPool.instance.secondChanceItemDefinition.getCurrendEffect();
                    if (effekt != 0 && !RuntimeDataManager.instance.preRevive.secondChanceUsed && !RuntimeDataManager.instance.postRevive.secondChanceUsed) {
                        var newMass = (float)ConstantManager.PLAYER_MAX_MESH_GENERATION_SIZE / 100f * effekt;
                        Debug.Log("2nd Chance: " + newMass.ToString());
                        SetTargetMass(newMass, true);
                        RuntimeDataManager.value.secondChanceUsed = true;
                    } else {
                        SceneManager.instance.callSceneEndgame();
                    }
                } else if (SaveDataManager.getValue.gameStatus == GameStatus.shop) {
                    SetTargetMass(ConstantManager.PLAYER_MENU_START_MASS, true);
                }
            }
        }
    }
    private void ResizeActualPlayerSizeInWorld() {
        var size = (currentMass * ConstantManager.PLAYER_AMOUNT_TO_GROW_PER_MASS_IN_WORLD_SPACE) / 2f;
        transform.localScale = new Vector3(size, size, size);
    }

    //ON COLLISSION WITH PARTICLE
    private void OnCollisionEnter(Collision other) {
        var pos = other.transform.position;
        var tag = other.gameObject.tag;
        if (tag == "Particle" && other.collider.GetType() == typeof(MeshCollider)) {
            var particleScript = other.gameObject.GetComponent<ParticleScript>();
            var particleType = particleScript.particleType;
            var particleMass = particleScript.particleMass;

            if (particleType == ParticleType.grow) {
                SetTargetMass(targetMass + particleMass);
                RuntimeDataManager.value.normalParticlesCollected++;
                RuntimeDataManager.value.gainedMass += particleMass;
                SoundScript.ExplosionLarge();
                particleScript.Destroy(true, true, false, false);
            } else if (particleType == ParticleType.shrink) {
                SetTargetMass(targetMass - particleMass);
                RuntimeDataManager.value.shrinkParticlesCollected++;
                RuntimeDataManager.value.lossMass += particleMass;
                SoundScript.Shrink();
                particleScript.Destroy(true, true, false, true);
            } else if (particleType == ParticleType.gold) {
                RuntimeDataManager.value.goldParticlesCollected++;
                RuntimeDataManager.value.goldMassCollected += Mathf.CeilToInt(particleMass);
                GoldFeedbackSpawn.instance.NewGoldFeedback(pos, Mathf.CeilToInt(particleMass));
                SoundScript.Gold();
                particleScript.Destroy(true, true, false, true);
            }

            playerMeshGenerator.NewCollision(pos);
        }
    }

    //ON TRIGGER (=COIN MAGNET)
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Particle" &&
                    other.GetType() == typeof(SphereCollider) &&
                    other.GetComponent<ParticleScript>().particleType == ParticleType.gold &&
                    coinMagnetEffectFactor != 0)
            StartCoroutine(ParticleInOrbit(other));
    }
    private IEnumerator ParticleInOrbit(Collider other) {
        var rb = other.gameObject.GetComponent<Rigidbody>();
        while (other != null) {
            var dist = Vector3.Distance(transform.position, other.gameObject.transform.position);
            var direction = transform.position - other.gameObject.transform.position;
            var value = ConstantManager.ITEM_COIN_MAGNET_FACTOR;
            rb.AddForce(direction * value * rb.mass);
            yield return new WaitForSeconds(5 * Time.deltaTime);
        }
    }
}
