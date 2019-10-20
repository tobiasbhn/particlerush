using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSDScript : MonoBehaviour {
    
    public bool rotate = false;
    private float targetOpacity = 200f;

    void Update() {
        if (rotate)
            transform.Rotate(new Vector3(0,0,.1f));
    }

    public void Enable() {
        rotate = true;
        StartCoroutine(fadeIn());
    }

    public void Disable() {
        rotate = false;
    }

    private IEnumerator fadeIn() {
        float opacity = 0f;
        while (opacity < targetOpacity) {
            opacity += Mathf.FloorToInt(targetOpacity * Time.unscaledDeltaTime * 2);
            opacity = Mathf.Min(opacity, targetOpacity);
            GetComponent<RawImage>().color = new Color(255,255,255,opacity / 255);
            yield return null;
        }
    }
}
