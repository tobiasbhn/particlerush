using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHelper : MonoBehaviour {

    //INSTANCE
    [HideInInspector] public static GameHelper instance;
    [HideInInspector] public bool thisScriptLoaded = false;

    void Awake() {
        instance = this;
        thisScriptLoaded = true;
    }

    public void StartCoroutineFromNonMonoBehaviour(System.Collections.IEnumerator routine) {
        StartCoroutine(routine);
    }

}
