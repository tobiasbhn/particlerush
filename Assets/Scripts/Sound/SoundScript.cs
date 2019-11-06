using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour {

    // INSTANCE
    [HideInInspector] public static SoundScript instance;

    // OBJECT-LINKS
    public AudioSource shoot;

    void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
