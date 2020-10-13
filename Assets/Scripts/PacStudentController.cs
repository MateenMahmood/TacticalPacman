using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    int[,] levelMap = {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0}, 
    };
    
    Vector2Int mapPos;
    Vector3 prevPos;
    Vector3 endPos;
    Tweener tweener;
    [SerializeField] Animator animator = null;
    KeyCode lastInput;
    KeyCode currentInput;

    void Start() {
        prevPos = transform.position;
        mapPos = new Vector2Int(1, 1);
        tweener = gameObject.GetComponent<Tweener>();
    }
    void Update() {

        SetAnim();

        if (Input.GetKeyDown(KeyCode.D)) {
            lastInput = KeyCode.D;
            Debug.Log(levelMap[mapPos.x, mapPos.y + 1]);
            Debug.Log(mapPos);
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            lastInput = KeyCode.A;
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            lastInput = KeyCode.W;
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            lastInput = KeyCode.S;
        }

        if (!(tweener.TweenExists(transform))) {
            // Player is not moving
            
            // Right
            if (isWalkable(lastInput)) {
                // Walkable
                currentInput = lastInput;
                tweener.AddTween(transform, transform.position, endPos, 1.5f);
            } else {
                // Not Walkable
                // Check previous input
                if (isWalkable(currentInput)) {
                    tweener.AddTween(transform, transform.position, endPos, 1.5f);
                }
            }
        }
        prevPos = transform.position;
    }

    bool isWalkable(KeyCode keyCode) {
        endPos = transform.position;

        if (keyCode == KeyCode.D) {
            if (levelMap[mapPos.x, mapPos.y + 1] == 5) {
                mapPos.y += 1;
                endPos.x += 0.32f;
                return true;
            }
        }

        if (keyCode == KeyCode.A) {
            if (levelMap[mapPos.x, mapPos.y - 1] == 5) {
                mapPos.y -= 1;
                endPos.x -= 0.32f;
                return true;
            }
        }

        if (keyCode == KeyCode.W) {
            if (levelMap[mapPos.x - 1, mapPos.y] == 5) {
                mapPos.x -= 1;
                endPos.y += 0.32f;
                return true;
            }
        }

        if (keyCode == KeyCode.S) {
            if (levelMap[mapPos.x + 1, mapPos.y] == 5) {
                mapPos.x += 1;
                endPos.y -= 0.32f;
                return true;
            }
        }

        return false;
    }

    void SetAnim() {
        if (prevPos != transform.position) {
            animator.SetBool("isMoving", true);

            if (transform.position.x > prevPos.x) {
                animator.SetBool("goingUp", false);
                animator.SetBool("goingDown", false);
                animator.SetBool("goingLeft", false);
                animator.SetBool("goingRight", true);
            }
            if (transform.position.x < prevPos.x) {
                animator.SetBool("goingUp", false);
                animator.SetBool("goingDown", false);
                animator.SetBool("goingLeft", true);
                animator.SetBool("goingRight", false);
            }
            if (transform.position.y > prevPos.y) {
                animator.SetBool("goingUp", true);
                animator.SetBool("goingDown", false);
                animator.SetBool("goingLeft", false);
                animator.SetBool("goingRight", false);
            }
            if (transform.position.y < prevPos.y) {
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
    }
}
