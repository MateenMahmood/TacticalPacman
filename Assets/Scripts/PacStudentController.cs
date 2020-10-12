using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    Tweener tweener;
    [SerializeField] Animator animatorController = null;

    void Start() {
        transform.position = new Vector3(0.32f, -0.32f, 0);
        tweener = gameObject.GetComponent<Tweener>();
    }
    void Update() {
        
        // Move Right
        if (Input.GetKeyDown(KeyCode.D)) {
            Vector3 endPos = transform.position;
            endPos.x += transform.position.x;
            Debug.Log("currentPos" + transform.position + "\nendPos: " + endPos);
            bool hasAdded = tweener.AddTween(transform, transform.position, endPos, 1f);
        }


    }
}
