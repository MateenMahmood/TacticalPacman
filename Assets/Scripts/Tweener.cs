using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private List<Tween> activeTweens;

    float t;

    private void Start() {
        activeTweens = new List<Tween>();
        t = 0f;
    }

    private void Update() {
        if (activeTweens != null) {
            for (int i = 0; i < activeTweens.Count; i++) {

                if (Vector3.Distance(activeTweens[i].Target.position, activeTweens[i].EndPos) > 0.02f) {
                    t += activeTweens[i].Rate * Time.deltaTime;
                    Debug.Log("\nt: " + t);
                    activeTweens[i].Target.position = Vector3.Lerp(activeTweens[i].StartPos, activeTweens[i].EndPos, t);
                } else {
                    activeTweens[i].Target.position = activeTweens[i].EndPos;
                    activeTweens.Remove(activeTweens[i]);
                }
            }
        }
    }

    public bool TweenExists(Transform target) {
        foreach (Tween tween in activeTweens) {
            if (tween.Target.transform == target) {
                return true;
            }
        }
        return false;
    }

    public bool AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float rate) {
        if (!(TweenExists(targetObject))) {
            t = 0;
            activeTweens.Add(new Tween(targetObject, startPos, endPos, rate));
            return true;
        }
        return false;
    }
}
