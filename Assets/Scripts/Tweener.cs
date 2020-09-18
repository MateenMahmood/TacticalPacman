using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    //private Tween activeTween;
    private List<Tween> activeTweens;


    // Start is called before the first frame update
    void Start() {
        activeTweens = new List<Tween>();
    }

    // Update is called once per frame
    void Update() { 
        if (activeTweens != null) {
            for (int i = 0; i < activeTweens.Count; i++) {
                if (Vector3.Distance(activeTweens[i].Target.position, activeTweens[i].EndPos) > 0.0005f) {
                    float timeFraction = (Time.time - activeTweens[i].StartTime) / activeTweens[i].Duration;
                    float cubicTimeFraction = timeFraction * timeFraction * timeFraction;
                    activeTweens[i].Target.position = Vector3.Lerp(activeTweens[i].StartPos, activeTweens[i].EndPos, timeFraction);
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

    public bool AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration) {

        if (!(TweenExists(targetObject))) {
            activeTweens.Add(new Tween(targetObject, startPos, endPos, Time.time, duration));
            return true;
        }
        return false;
    }
}
