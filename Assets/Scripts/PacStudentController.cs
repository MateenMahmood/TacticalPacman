using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    Vector3 prevLoc;
    Vector3 endPos;
    Tweener tweener;
    [SerializeField] Animator animator = null;

    void Start() {
        prevLoc = transform.position;
        tweener = gameObject.GetComponent<Tweener>();
    }
    void Update() {
        
        SetAnim();
        
        // Move Right
        if (Input.GetKeyDown(KeyCode.D)) {
            endPos = transform.position;
            endPos.x += 0.32f;
            bool hasAdded = tweener.AddTween(transform, transform.position, endPos, 1f);
        }

        // Move Left
        if (Input.GetKeyDown(KeyCode.A)) {
            endPos = transform.position;
            endPos.x -= 0.32f;
            bool hasAdded = tweener.AddTween(transform, transform.position, endPos, 1f);
        }

        // Move Up
        if (Input.GetKeyDown(KeyCode.W)) {
            endPos = transform.position;
            endPos.y += 0.32f;
            bool hasAdded = tweener.AddTween(transform, transform.position, endPos, 1f);
        }

        // Move Down
        if (Input.GetKeyDown(KeyCode.S)) {
            endPos = transform.position;
            endPos.y -= 0.32f;
            bool hasAdded = tweener.AddTween(transform, transform.position, endPos, 1f);
        }
    }

    void SetAnim() {
        if (prevLoc != transform.position) {
            animator.SetBool("isMoving", true);

            if (transform.position.x > prevLoc.x) {
                animator.SetBool("goingUp", false);
                animator.SetBool("goingDown", false);
                animator.SetBool("goingLeft", false);
                animator.SetBool("goingRight", true);
            }
            if (transform.position.x < prevLoc.x) {
                animator.SetBool("goingUp", false);
                animator.SetBool("goingDown", false);
                animator.SetBool("goingLeft", true);
                animator.SetBool("goingRight", false);
            }
            if (transform.position.y > prevLoc.y) {
                animator.SetBool("goingUp", true);
                animator.SetBool("goingDown", false);
                animator.SetBool("goingLeft", false);
                animator.SetBool("goingRight", false);
            }
            if (transform.position.y < prevLoc.y) {
                animator.SetBool("goingUp", false);
                animator.SetBool("goingDown", true);
                animator.SetBool("goingLeft", false);
                animator.SetBool("goingRight", false);
            }

        } else {
            animator.SetBool("isMoving", false);
            animator.SetBool("goingUp", false);
            animator.SetBool("goingDown", false);
            animator.SetBool("goingLeft", false);
            animator.SetBool("goingRight", false);
        }
        prevLoc = transform.position;
    }
}
