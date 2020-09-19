using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGen : MonoBehaviour
{
    [SerializeField] GameObject Pacman = null;
    [SerializeField] GameObject Inky = null;
    [SerializeField] GameObject Blinky = null;
    [SerializeField] GameObject Pinky = null;
    [SerializeField] GameObject Clyde = null;
    // Start is called before the first frame update
    void Start() {
        Instantiate(Pacman, new Vector3(0.32f, -0.32f, 0), Quaternion.identity);
        Instantiate(Inky, new Vector3(3.84f, -4.48f, 0), Quaternion.identity);
        Instantiate(Blinky, new Vector3(4.16f, -4.48f, 0), Quaternion.identity);
        Instantiate(Pinky, new Vector3(4.48f, -4.48f, 0), Quaternion.identity);
        Instantiate(Clyde, new Vector3(4.8f, -4.48f, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
