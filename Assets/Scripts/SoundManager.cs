using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // All Serialize Fields should be set to as null to stop warning
    [SerializeField] private AudioSource startJingle = null;

    // Start is called before the first frame update
    void Start() {
        startJingle.Play();
    }

    // Update is called once per frame
    void Update() {
        
    }
}
