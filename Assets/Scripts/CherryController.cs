using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    [SerializeField] GameObject cherry = null;
    GameObject cherryClone;
    Vector3 startPos;
    Vector3 endPos;
    Tweener tweener;
    Camera mainCamera;
    float timer;
    bool doesExist;

    void Start() {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        startPos = mainCamera.ViewportToWorldPoint(new Vector3(-0.1f, 0.5f, 10f));
        endPos = mainCamera.ViewportToWorldPoint(new Vector3(1.1f, 0.5f, 10));

        timer = 0f;
    }

    void Update() {

        timer += Time.deltaTime;

        // Every 30 Seconds
        if (timer > 30 && !doesExist) {
            cherryClone = Instantiate(cherry, startPos, Quaternion.identity);
            tweener = cherryClone.GetComponent<Tweener>();
            timer = 0;
            doesExist = true;
        }

        if (doesExist) {
            tweener.AddTween(cherryClone.transform, cherryClone.transform.position, endPos, 0.1f);
            if (cherryClone.transform.position.x >= endPos.x) {
                doesExist = false;
                Destroy(cherryClone);
            }
        }
    }
}
