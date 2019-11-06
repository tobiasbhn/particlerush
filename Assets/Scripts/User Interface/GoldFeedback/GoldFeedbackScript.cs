using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFeedbackScript : MonoBehaviour {
    private Vector3 target;
    private float distance;

    void Awake() {
        target = UiObjectReferrer.instance.ingameGoldText.transform.position;
        distance = Vector2.Distance(this.transform.position, target); 
    }

    void Update() {
        var newPos = Vector2.MoveTowards(transform.position, target, distance * Time.deltaTime * 3);
        this.transform.position = newPos;
        var newDist = Vector2.Distance(this.transform.position, target);
        if (newDist <= distance * Time.deltaTime * 3) {
            GameObject.Destroy(this.gameObject);
            ScoreScript.instance.gold ++;
        }
    }
}
