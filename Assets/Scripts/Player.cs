using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator animatorController = null;
    [SerializeField] AudioSource moveSound = null;

    // Start is called before the first frame update
    void Start() {
        transform.position = new Vector3(0.32f, -0.32f, 0);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
