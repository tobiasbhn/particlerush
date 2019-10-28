using System.Collections;
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
    private PlayerMeshGenerator playerMeshGenerator;
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;
    private MeshRenderer meshRenderer;

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



    //GENERAL FUNCTIONS
    void Awake() {
        instance = this;
    }
    void Start() {
        playerMeshGenerator = GetComponent<PlayerMeshGenerator>();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        playerMeshGenerator.unityMesh = new UnityEngine.Mesh();
        mesh = playerMeshGenerator.unityMesh;
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
        meshRenderer.enabled = false;
        this.transform.Rotate(90, 0, 0);
    }
    void Update() {
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
            if (currentMass >= ConstantManager.PLAYER_MAX_MESH_GENERATION_SIZE && SaveDataManager.getValue.gameStatus == GameStatus.ingame && sizeTrend >= 0) {
                SceneManager.instance.callSceneEndgame();
            }
        }
    }
    private void ResizeActualPlayerSizeInWorld() {
        var size = (currentMass * ConstantManager.PLAYER_AMOUNT_TO_GROW_PER_MASS_IN_WORLD_SPACE) / 2f;
        transform.localScale = new Vector3(size, size, size);
    }

    //ON COLLISSION WITH FOOD
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
            } else if (particleType == ParticleType.shrink) {
                SetTargetMass(targetMass - particleMass);
                RuntimeDataManager.value.shrinkParticlesCollected++;
                RuntimeDataManager.value.lossMass += particleMass;
            } else if (particleType == ParticleType.gold) {
                RuntimeDataManager.value.goldParticlesCollected++;
                RuntimeDataManager.value.goldMassCollected += (int)particleMass;
            }

            particleScript.Destroy(true, true, false);
            playerMeshGenerator.NewCollision(pos);
        }
    }
}
