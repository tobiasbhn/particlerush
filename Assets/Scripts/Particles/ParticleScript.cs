using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class ParticleScript : MonoBehaviour {
    
    //OBJECT_LINKS
    [SerializeField] private Rigidbody rb;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private ParticleSystem particleSys;

    //BEHAVIOUR
    [HideInInspector] public float speed = 0;
    private bool alreadyDestroyed = false;

    //PARTICLE OPTIONS
    [HideInInspector] public float particleMass;
    [HideInInspector] public Mesh particleMesh;
    public Color growParticleColor;
    public Color shrinkParticleColor;
    public Color goldParticleColor;
    [HideInInspector] public ParticleType particleType;

    //START
    void Start() {
        DefineMass();
        DefineMesh();
        DefineSize();
        DefineColor();

        rb.AddForce(0, speed, 0);
        rb.AddTorque(
            Random.Range(-ConstantManager.PARTICLE_ROTATION_SPEED, ConstantManager.PARTICLE_ROTATION_SPEED),
            Random.Range(-ConstantManager.PARTICLE_ROTATION_SPEED, ConstantManager.PARTICLE_ROTATION_SPEED),
            Random.Range(-ConstantManager.PARTICLE_ROTATION_SPEED, ConstantManager.PARTICLE_ROTATION_SPEED)
        );
    }

    //START DEKLARATIONS
    private void DefineMass() {
        particleMass = 5f;
        particleMass -= (float)Random.Range(0, 5);
    }

    private void DefineMesh() {
        if (particleMesh == null) {}
            particleMesh = new Mesh();

        switch (Mathf.CeilToInt(particleMass)) {
            case 1: 
                particleMesh = ParticleMeshScript.mesh1; break;
            case 2: 
                particleMesh = ParticleMeshScript.mesh2; break;
            case 3: 
                particleMesh = ParticleMeshScript.mesh3; break;
            case 4:
                particleMesh = ParticleMeshScript.mesh4; break;
            case 5: 
                particleMesh = ParticleMeshScript.mesh5; break;
            default:
                particleMesh = ParticleMeshScript.mesh3; break;
        }
        particleMesh.RecalculateNormals();

        meshFilter.mesh = particleMesh;
        meshCollider.sharedMesh = particleMesh;
    }

    private void DefineSize() {
        var size = (3.5f + particleMass) * (ConstantManager.PLAYER_AMOUNT_TO_GROW_PER_MASS_IN_WORLD_SPACE / 2);
        transform.localScale = new Vector3(size, size, size);
    }

    private void DefineColor() {
        var particleSysMat = particleSys.gameObject.GetComponent<ParticleSystemRenderer>().material;
        if (particleType == ParticleType.grow) {
            meshRenderer.material.SetColor("_BaseColor", growParticleColor);
            particleSysMat.SetColor("_BaseColor", growParticleColor);
        } else if (particleType == ParticleType.shrink) {
            meshRenderer.material.SetColor("_BaseColor", shrinkParticleColor);
            particleSysMat.SetColor("_BaseColor", shrinkParticleColor);
        } else if (particleType == ParticleType.gold) {
            meshRenderer.material.SetColor("_BaseColor", goldParticleColor);
            particleSysMat.SetColor("_BaseColor", goldParticleColor);
        }
    }

    //MASS STUFF
    public void ShrinkParticle() {
        ShrinkParticle(1f);
    }
    public void ShrinkParticle(float amount) {
        particleMass -= amount;
        if (particleMass <= 0) {
            Destroy(true);

            if (particleType == ParticleType.grow) {
                RuntimeDataManager.value.normalParticlesDestroyed++;
                ScoreScript.instance.IncreaseScoreParticle();
            } else if (particleType == ParticleType.shrink) {
                RuntimeDataManager.value.shrinkParticlesDestroyed++;
            } else if (particleType == ParticleType.gold) {
                RuntimeDataManager.value.goldParticlesDestroyed++;
            }
                
        } else {
            DefineMesh();
            DefineSize();
        }
    }

    //DESTROY ON INVISIBLE
    private void OnBecameInvisible() {
        Destroy(false, true, false, false);
    }

    //DESTROY DEKLARATION
    public void Destroy(bool _withAnimation, bool _deleteFromList, bool _applyPlayerGrow, bool _withSound) {
        if (!alreadyDestroyed) {
            alreadyDestroyed = true;
            //Delete that one Particle from the Particles-Array - somtimes they already have been removed
            if (_deleteFromList)
                ParticleSpawnScript.instance.instantiatedParticles.Remove(this);
            //Play Destroy-Animation and shake Screen, if animations are allowed
            if (_withAnimation) {
                ShakeScript.instance.Shake();
                if (particleSys != null) {
                    particleSys.Play();
                }
                if (particleType == ParticleType.grow) {
                    StartCoroutine(delayedVibration());
                }
            }
            if (_withSound) {
                SoundScript.ExplosionSmall();
            }
            //Let Player Grow if an "shrink"-Particle gets Detroyed
            if (_applyPlayerGrow && particleType == ParticleType.shrink) {
                PlayerScript.instance.SetTargetMass(PlayerScript.instance.targetMass += ConstantManager.PARTICLE_MASS_TO_ADD_ON_DESTROY_WRONG_PARTICLE);
            }
            //Hide Particle and Colliders, enabel Particle system and Destroy after Animation played
            meshRenderer.enabled = false;
            meshCollider.enabled = false;
            sphereCollider.enabled = false;
            GameObject.Destroy(this.gameObject, 1f);
        }
    }
    public void Destroy(bool _withAnimation) {
        Destroy(_withAnimation, true, true, true);
    }

    private IEnumerator delayedVibration() {
        yield return new WaitForSecondsRealtime(.17f);
        VibrationManager.Vibrate();
    }
}