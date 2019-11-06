using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFeedbackSpawn : MonoBehaviour {

    // INSTANCE
    public static GoldFeedbackSpawn instance;

    // OBJECT-LINKS
    public GoldFeedbackScript feedbackPrefab;
    public GameObject feedbackDirectory;

    void Awake() {
        instance = this;
    }

    public void NewGoldFeedback(Vector3 pos, int mass) {
        StartCoroutine(GoldFeedbackHelper(pos, mass));
    }
    private IEnumerator GoldFeedbackHelper(Vector3 pos, int mass) {
        var worldToScreenPos = Camera.main.WorldToScreenPoint(pos);
        for (int i = 0; i < mass; i ++) {
            var _feedback = Instantiate(feedbackPrefab.gameObject, worldToScreenPos, Quaternion.identity);
            _feedback.transform.SetParent(feedbackDirectory.transform);
            var rect = _feedback.GetComponent<RectTransform>();
            var scale = rect.localScale;
            var newScale = scale * UiObjectReferrer.instance.uiCanvas.scaleFactor;
            rect.localScale = newScale;
            yield return new WaitForSeconds(Time.deltaTime * 5);
        }
    }
}
