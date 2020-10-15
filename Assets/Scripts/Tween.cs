using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween {
    public Transform Target { get; private set; }
    public Vector3 StartPos { get; private set; }
    public Vector3 EndPos { get; private set; }
    public float T { get; set; }
    public float Rate { get; private set; }

    public Tween(Transform Target, Vector3 StartPos, Vector3 EndPos, float Rate) {
        this.Target = Target;
        this.StartPos = StartPos;
        this.EndPos = EndPos;
        this.T = 0f;
        this.Rate = Rate;
    }
}
