using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    int[,] levelMap = {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7,7,2,2,2,2,2,2,2,2,2,2,2,2,1},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4,4,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4,4,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4,4,5,4,0,0,0,4,5,4,0,0,4,6,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3,3,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4,4,4,4,3,5,3,3,5,3,4,4,3,5,2},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3,3,4,4,3,5,4,4,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,5,5,2},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4,4,0,3,4,4,3,4,5,1,2,2,2,2,1},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3,3,0,3,4,4,3,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0,0,0,0,0,0,4,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0,0,4,4,3,0,4,4,5,2,0,0,0,0,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0,0,0,0,4,0,3,3,5,1,2,2,2,2,2},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0,0,0,0,4,0,0,0,5,0,0,0,0,0,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0,0,0,0,4,0,3,3,5,1,2,2,2,2,2},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0,0,4,4,3,0,4,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0,0,0,0,0,0,4,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3,3,0,3,4,4,3,4,5,2,0,0,0,0,0},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4,4,0,3,4,4,3,4,5,1,2,2,2,2,1},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3,3,4,4,3,5,4,4,5,3,4,4,3,5,2},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4,4,4,4,3,5,3,3,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3,3,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4,4,5,4,0,0,0,4,5,4,0,0,4,6,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4,4,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4,4,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7,7,2,2,2,2,2,2,2,2,2,2,2,2,1},
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

        bool isMoving = false;
        bool isUp = false;
        bool isDown = false;
        bool isLeft = false;
        bool isRight = false;

        if (transform.position != prevPos) {
            isMoving = true;
        }

        if (transform.position.x > prevPos.x) {
            isRight = true;
        }
        if (transform.position.x < prevPos.x) {
            isLeft = true;
        }
        if (transform.position.y > prevPos.y) {
            isUp = true;
        }
        if (transform.position.y < prevPos.y) {
            isDown = true;
        }

        animator.SetBool("isMoving", isMoving);
        animator.SetBool("goingUp", isUp);
        animator.SetBool("goingDown", isDown);
        animator.SetBool("goingLeft", isLeft);
        animator.SetBool("goingRight", isRight);
    }
}
