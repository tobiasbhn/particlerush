using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawnScript : MonoBehaviour {
    //INSTANCE
    [HideInInspector] public static ParticleSpawnScript instance;
    [HideInInspector] public bool thisScriptLoaded = false;

    //LINKS
    public GameObject particlePrefab;
    public GameObject particleDirectory;

    //BEHAVIOUR
    [HideInInspector] public bool allowSpeedIncrease = false;

    //SPAWN
    private float lastParticleSpawnTime;
    [HideInInspector] public List<GameObject> instantiatedParticles = new List<GameObject>();
    public SpawnModi spawnModi;
    [HideInInspector] public float spawnBaseDelay = 0;
    [HideInInspector] public float spawnBaseSpeed = 0;

    void Awake() {
        instance = this;
        lastParticleSpawnTime = Time.time;
        spawnModi = SpawnModi.none;
        thisScriptLoaded = true;
    }


    //SPAWN BEHAVIOUR
    void Update() {
        if (spawnModi != SpawnModi.none) {
            var difficultFactor = 0;
            var spawnDelay = allowSpeedIncrease ? spawnBaseDelay - difficultFactor : spawnBaseDelay;

            if (lastParticleSpawnTime + spawnDelay < Time.time) {
                lastParticleSpawnTime = Time.time;
                SpawnParticle();
            }
        }
    }

    private void SpawnParticle() {
        Vector3 randomParticleSpawnPosition = new Vector3(
            Random.Range(ConstantManager.CAMERA_LOWER_LEFT_CORNER_IN_WORD_SPACE.x, ConstantManager.CAMERA_UPPER_RIGHT_CORNER_IN_WORLD_SPACE.x),
            ConstantManager.CAMERA_LOWER_LEFT_CORNER_IN_WORD_SPACE.y - (ConstantManager.CAMERA_SCREEN_HEIGHT_IN_WORLD_SPACE / 5),
            ConstantManager.CAMERA_LOWER_LEFT_CORNER_IN_WORD_SPACE.z
        );

        var spawnedParticle = Instantiate(particlePrefab, randomParticleSpawnPosition, Quaternion.identity);
        spawnedParticle.transform.SetParent(particleDirectory.transform);
        var particleScript = spawnedParticle.GetComponent<ParticleScript>();
        particleScript.particleType = DefineType();
        particleScript.particleSpeed = DefineSpeed();
        particleScript.allowSpeedIncrease = allowSpeedIncrease;
        instantiatedParticles.Add(spawnedParticle);
        RuntimeDataManager.value.particlesSpawned++;
    }

    //DEFINE TYPE
    private ParticleType DefineType() {
        switch (spawnModi) {
            case SpawnModi.all:
                return DefineNormalType();
            case SpawnModi.onlyNorm:
                RuntimeDataManager.value.normalParticlesSpawned++;
                return ParticleType.grow;
            case SpawnModi.onlyShrink:
                RuntimeDataManager.value.shrinkParticlesSpawned++;
                return ParticleType.shrink;
            case SpawnModi.onlyGold:
                RuntimeDataManager.value.goldParticlesSpawned++;
                return ParticleType.gold;
            case SpawnModi.onlyMassRelative:
                if (Random.Range(0, 100) < ConstantManager.PARTICLE_SHRINK_SPAWN_CHANCE) {
                    RuntimeDataManager.value.shrinkParticlesSpawned++;
                    return ParticleType.shrink;
                }
                else {
                    RuntimeDataManager.value.normalParticlesSpawned++;
                    return ParticleType.grow;
                }
            default:
                spawnModi = SpawnModi.all;
                return DefineNormalType();
        }
    }
    private ParticleType DefineNormalType() {
        var random = Random.Range(0, 100);
        if (random < ConstantManager.PARTICLE_GOLD_SPAWN_CHANCE) {
            RuntimeDataManager.value.goldParticlesSpawned++;
            return ParticleType.gold;
        } else if (random < ConstantManager.PARTICLE_GOLD_SPAWN_CHANCE + ConstantManager.PARTICLE_SHRINK_SPAWN_CHANCE) {
            RuntimeDataManager.value.shrinkParticlesSpawned++;
            return ParticleType.shrink;
        } else {
            RuntimeDataManager.value.normalParticlesSpawned++;
            return ParticleType.grow;
        }
    }

    //DEFINE SPEED
    private float DefineSpeed() {
        //random factor, so not all particles fly next to each other
        var randomFactor = Random.Range(
            -spawnBaseSpeed / ConstantManager.PARTICLE_SPEED_RANDOM_FACTOR,
            spawnBaseSpeed / ConstantManager.PARTICLE_SPEED_RANDOM_FACTOR
        );
        //difficult factor, so that the Particles gets faster according to level
        var difficultFactor = 0f;

        //add everything together
        var returnSpeed = spawnBaseSpeed + randomFactor;
        returnSpeed = allowSpeedIncrease ? returnSpeed + difficultFactor : returnSpeed;

        return returnSpeed;
    }

    //DESTROY BEHAVIOUR
    public void DestroyAllParticles() {
        foreach (GameObject particle in instantiatedParticles) {
            particle.GetComponent<ParticleScript>().Destroy(true, false, false);
        }
        instantiatedParticles.Clear();
    }
}