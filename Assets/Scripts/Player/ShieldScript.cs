using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class ShieldScript : MonoBehaviour {
    private Mesh mesh;
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshFilter meshFilter;
    private int collisionCount = 0;

    public void Setup() {
        mesh = PlayerScript.instance.playerMeshGenerator.unityMesh;
        meshCollider.sharedMesh = mesh;
        meshFilter.mesh = mesh;
        meshCollider.enabled = false;
        meshRenderer.enabled = false;
    }
    private void Update() {
        transform.Rotate(new Vector3(.3f, .3f, .3f), Space.Self);
    }

    public void ActivateShield(int count) {
        if (count > 0 && (!ReviveScript.instance.alreadyRevived || SaveDataManager.getValue.gameStatus == GameStatus.shop)) {
            collisionCount = count;
            meshRenderer.enabled = true;
            meshCollider.enabled = true;
        } else {
            DeactivateShield();
        }
    }
    public void DeactivateShield() {
        collisionCount = 0;
        meshRenderer.enabled = false;
        meshCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
        var tag = other.gameObject.tag;
        if (tag == "Particle" && other.GetType() == typeof(MeshCollider)) {
            var particleScript = other.gameObject.GetComponent<ParticleScript>();
            if (particleScript.particleType == ParticleType.grow) {
                particleScript.Destroy(true);
                VibrationManager.Vibrate();
                collisionCount--;
                if (collisionCount < 1) {
                    DeactivateShield();
                }
            }
        }
    }
}
