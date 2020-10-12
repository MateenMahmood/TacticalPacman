using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // All Serialize Fields should be set to as null to stop warning
    [SerializeField] private AudioSource startMusic = null;
    [SerializeField] private AudioSource mainMusic = null;
    int sceneIndex;

    void Start() {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update() {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex == 0) {

            if (!startMusic.loop) {
                startMusic.loop = true;
            }

            if (!startMusic.isPlaying) {
                startMusic.Play();
            }
        }

        if (sceneIndex == 1) {
            
            if (startMusic.loop) {
                startMusic.loop = false;
            }

            if (!startMusic.isPlaying && !mainMusic.isPlaying) {
                mainMusic.Play();
            }
        }
    }
}
