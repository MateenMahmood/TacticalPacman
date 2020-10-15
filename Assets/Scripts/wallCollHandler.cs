using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallCollHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem particle = null;
    float timer = 0;
    void Start() {
        if (!particle.isPlaying) {
            particle.Play();
        }
    }

    void Update() {
        timer += Time.deltaTime;
        if (timer > 0.8) {
            Destroy(gameObject);
        }
    }
}
