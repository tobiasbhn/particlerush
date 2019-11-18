using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleLoginScript : MonoBehaviour {

    // INSTANCE
    public static GoogleLoginScript instance;

    // VARS
    [HideInInspector] public bool currentlyLoggedIn = false;
    [HideInInspector] public string userName = "";

    void Awake() {
        instance = this;
    }

    public void Login() {
        currentlyLoggedIn = true;
        userName = "Testname";
    }
    public void Logout() {
        currentlyLoggedIn = false;
        userName = "";
    }
}
