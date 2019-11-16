using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawnScript : MonoBehaviour {
    //INSTANCE
    [HideInInspector] public static ParticleSpawnScript instance;

    //LINKS
    public GameObject particlePrefab;
    public GameObject particleDirectory;

    //SPAWN
    private float lastParticleSpawnTime;
    [HideInInspector] public List<ParticleScript> instantiatedParticles = new List<ParticleScript>();
    [HideInInspector] public ParticleSpawnModi spawnModi;
    [HideInInspector] public float spawnDelay = 0;
    [HideInInspector] public float spawnSpeed = 0;

    void Awake() {
        instance = this;
        lastParticleSpawnTime = Time.time;
        spawnModi = ParticleSpawnModi.none;
    }


    //SPAWN BEHAVIOUR
    void Update() {
        if (spawnModi != ParticleSpawnModi.none) {
            var delay = spawnDelay + ScoreScript.instance.GetParticleSpawnFactor();

            if (lastParticleSpawnTime + delay < Time.time) {
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
        particleScript.speed = DefineSpeed();
        instantiatedParticles.Add(particleScript);
        RuntimeDataManager.value.particlesSpawned++;
    }

    //DEFINE TYPE
    private ParticleType DefineType() {
        switch (spawnModi) {
            case ParticleSpawnModi.all:
                return DefineNormalType();
            case ParticleSpawnModi.onlyNorm:
                RuntimeDataManager.value.normalParticlesSpawned++;
                return ParticleType.grow;
            case ParticleSpawnModi.onlyShrink:
                RuntimeDataManager.value.shrinkParticlesSpawned++;
                return ParticleType.shrink;
            case ParticleSpawnModi.onlyGold:
                RuntimeDataManager.value.goldParticlesSpawned++;
                return ParticleType.gold;
            case ParticleSpawnModi.onlyMassRelative:
                if (Random.Range(0, 100) < ConstantManager.PARTICLE_SHRINK_SPAWN_CHANCE) {
                    RuntimeDataManager.value.shrinkParticlesSpawned++;
                    return ParticleType.shrink;
                } else {
                    RuntimeDataManager.value.normalParticlesSpawned++;
                    return ParticleType.grow;
                }
            case ParticleSpawnModi.onlySpecial:
                var totalPercent = ConstantManager.PARTICLE_GOLD_SPAWN_CHANCE + ConstantManager.PARTICLE_SHRINK_SPAWN_CHANCE;
                if (Random.Range(0, totalPercent) < ConstantManager.PARTICLE_GOLD_SPAWN_CHANCE) {
                    RuntimeDataManager.value.goldParticlesSpawned++;
                    return ParticleType.gold;
                } else {
                    RuntimeDataManager.value.shrinkParticlesSpawned++;
                    return ParticleType.shrink;
                }
            default:
                spawnModi = ParticleSpawnModi.all;
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
    public float DefineSpeed() {
        var speed = spawnSpeed + ScoreScript.instance.GetParticleSpeedFactor();
        speed += Random.Range(-speed / ConstantManager.PARTICLE_SPEED_RANDOM_FACTOR, speed / ConstantManager.PARTICLE_SPEED_RANDOM_FACTOR); 
        return speed;
    }

    //DESTROY BEHAVIOUR
    public void DestroyAllParticles() {
        foreach (ParticleScript particle in instantiatedParticles) {
            particle.Destroy(true, false, false, true);
        }
        instantiatedParticles.Clear();
    }
}