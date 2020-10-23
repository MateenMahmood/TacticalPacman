using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // All Serialize Fields should be set to as null to stop warning
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] List<AudioClip> soundClips = null;
    int sceneIndex;
    public bool isDead;
    public bool isScared;
    bool scaredTrigger;
    bool deadTrigger;

    void Start() {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        isDead = false;
        isScared = false;
        scaredTrigger = true;
        deadTrigger = true;
    }

    void Update() {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex == 0) {

            audioSource.clip = soundClips[0];

            if (!audioSource.loop) {
                audioSource.loop = true;
            }

            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
        }

        if (sceneIndex == 1) {

            if (!isScared && ! isDead) {
                audioSource.clip = soundClips[1];
            }

            if (isScared && !isDead) {
                if (scaredTrigger) {
                    if (audioSource.isPlaying) {
                        audioSource.Stop();
                        audioSource.clip = soundClips[2];
                        scaredTrigger = false;       
                    }
                }
            } else {
                scaredTrigger = true;
            }

            if (isDead) {
                if (deadTrigger) {
                    if (audioSource.isPlaying) {
                        audioSource.Stop();
                        audioSource.clip = soundClips[3];
                        deadTrigger = false;       
                    }
                }
            } else {
                deadTrigger = true;
            }

            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
        }
    }
}
