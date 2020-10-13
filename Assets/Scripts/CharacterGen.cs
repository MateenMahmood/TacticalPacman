using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGen : MonoBehaviour
{
    [SerializeField] Camera mainCamera = null;
    [SerializeField] GameObject Pacman = null;
    [SerializeField] GameObject Inky = null;
    [SerializeField] GameObject Blinky = null;
    [SerializeField] GameObject Pinky = null;
    [SerializeField] GameObject Clyde = null;
    // Start is called before the first frame update
    void Start() {
        GameObject pacman = Instantiate(Pacman, new Vector3(0.48f, -0.48f, 0), Quaternion.identity);
        GameObject inky = Instantiate(Inky, new Vector3(3.84f, -4.48f, 0), Quaternion.identity);
        GameObject blinky = Instantiate(Blinky, new Vector3(4.16f, -4.48f, 0), Quaternion.identity);
        GameObject pinky = Instantiate(Pinky, new Vector3(4.48f, -4.48f, 0), Quaternion.identity);
        GameObject clyde = Instantiate(Clyde, new Vector3(4.8f, -4.48f, 0), Quaternion.identity);
        inky.GetComponentInChildren<Canvas>().worldCamera = mainCamera;
        blinky.GetComponentInChildren<Canvas>().worldCamera = mainCamera;
        pinky.GetComponentInChildren<Canvas>().worldCamera = mainCamera;
        clyde.GetComponentInChildren<Canvas>().worldCamera = mainCamera;
    }
}
