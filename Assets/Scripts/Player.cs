using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Tweener tweener = null;
    [SerializeField] AudioSource moveSound = null;
    private List<GameObject> itemList;

    // Start is called before the first frame update
    void Start() {
        gameObject.transform.position = new Vector3(0.32f, -0.32f, 0);
        itemList = new List<GameObject>();
        itemList.Add(gameObject);
        // Vector3 testEndPos = new Vector3 (1.92f, -0.32f, 0);
        // tweener.AddTween(gameObject.transform, gameObject.transform.position, testEndPos, 1.0f);
        // testEndPos = new Vector3 (2.92f, -2.32f, 0);
        // tweener.AddTween(gameObject.transform, gameObject.transform.position, testEndPos, 1.0f);
    }

    // Update is called once per frame
    void Update() {
        if (gameObject.transform.position == new Vector3(0.32f, -0.32f, 0)) {
            MovePlayer(new Vector3(1.92f, -0.32f, 0f), 1.5f);
        }
        if (gameObject.transform.position == new Vector3(1.92f, -0.32f, 0f)) {
            MovePlayer(new Vector3(1.92f, -1.6f, 0f), 1.5f);
        }
        if (gameObject.transform.position == new Vector3(1.92f, -1.6f, 0f)) {
            MovePlayer(new Vector3(0.32f, -1.6f, 0f), 1.5f);
        }
        if (gameObject.transform.position == new Vector3(0.32f, -1.6f, 0f)) {
            MovePlayer(new Vector3(0.32f, -0.32f, 0), 1.5f);
        }
    }

    void MovePlayer(Vector3 endPos, float duration) {
        foreach (GameObject item in itemList) {
            bool hasAdded = tweener.AddTween(item.transform, item.transform.position, endPos, duration);
            if (hasAdded) {
                break;
            }
        }
    }
}
