using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {
    
    #region Level Information
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
        {2,2,2,2,2,2,5,3,3,0,4,0,0,0,0,0,0,4,0,3,3,5,2,2,2,2,2,2},
        {0,0,0,0,0,2,5,0,0,0,4,0,0,0,0,0,0,4,0,0,0,5,2,0,0,0,0,0},
        {2,2,2,2,2,2,5,3,3,0,4,0,0,0,0,0,0,4,0,3,3,5,2,2,2,2,2,2},
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
    List<int> walkableValues;
    Vector2Int mapPos;
    UIManager uIManager;
    #endregion

    #region Movement
    string direction;
    string prevDirection;
    bool inMovement;
    Vector3 prevPos;
    Vector3 endPos;
    #endregion

    #region Animations
    Tweener tweener;
    Animator animator;
    #endregion

    #region Player Information
    GameObject player;
    #endregion

    void Start() {
        #region GetComponents
        uIManager = GameObject.FindGameObjectWithTag("managers").GetComponent<UIManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        tweener = gameObject.GetComponent<Tweener>();
        animator = gameObject.GetComponent<Animator>();
        #endregion
        
        #region Initial Values
        walkableValues = new List<int>();
        walkableValues.Add(0);
        walkableValues.Add(5);
        walkableValues.Add(6);
        direction = "up";
        prevDirection = direction;
        inMovement = true;
        prevPos = transform.position;
        #endregion

        #region mapPos
        if (tag == "G1") {
            mapPos = new Vector2Int(14, 13);
        }
        if (tag == "G2") {
            mapPos = new Vector2Int(14, 15);
        }
        if (tag == "G3") {
            mapPos = new Vector2Int(14, 12);
        }
        if (tag == "G4") {
            mapPos = new Vector2Int(14, 14);
        }
        #endregion
    }

    void Update() {
        if (uIManager.canPlay) {
            if (!tweener.TweenExists(transform)) {
                if (inMovement) {

                    UpdateAnims(direction);
                    
                    if (direction.Equals("right") || direction.Equals("left")) {
                        if (isWalkable("up") || isWalkable("down")) {
                            inMovement = false; 
                        } else {
                            if (isWalkable(direction)) {
                                SetMapPos(direction);
                                tweener.AddTween(transform, transform.position, endPos, 2.1f);
                                prevDirection = direction;
                            }
                        }
                    }

                    if (direction.Equals("up") || direction.Equals("down")) {
                        if (isWalkable("right") || isWalkable("left")) {
                            inMovement = false; 
                        } else {
                            if (isWalkable(direction)) {
                                SetMapPos(direction);
                                tweener.AddTween(transform, transform.position, endPos, 2.1f);
                                prevDirection = direction;
                            }
                        }
                    }

                } else {
                    // Decide on the direction
                    if (tag == "G1") {
                        direction = G1AI();
                    }

                    if (tag == "G2") {
                        direction = G2AI();
                    }

                    if (tag == "G3") {
                        direction = G3AI();
                    }

                    if (tag == "G4") {
                        direction = G4AI();
                    }

                    if (transform.position == prevPos) {
                        direction = NotMoving();
                    }

                    prevPos = transform.position;

                    if (isWalkable(direction)) {
                        SetMapPos(direction);
                        tweener.AddTween(transform, transform.position, endPos, 2.1f);
                        prevDirection = direction;
                        inMovement = true;
                    }
                }
            }
            UpdateAnims(direction);
        }
    }

    void UpdateAnims(string direction) {

        animator.SetBool("isMoving", false);
        animator.SetBool("isLeft", false);
        animator.SetBool("isRight", false);
        animator.SetBool("isUp", false);
        animator.SetBool("isDown", false);
        animator.SetBool("isScared", false);
        animator.SetBool("isDead", false);
        animator.SetBool("isRecovering", false);


        if (!(animator.GetBool("isDead") || animator.GetBool("isScared") || animator.GetBool("isRecovering"))) {
            if (!uIManager.canPlay) {
                animator.SetBool("isMoving", false);
            } else {
                animator.SetBool("isMoving", true);
            }

            if (direction == "left") {
                animator.SetBool("isLeft", true);
            }

            if (direction == "right") {
                animator.SetBool("isRight", true);
            }

            if (direction == "up") {
                animator.SetBool("isUp", true);
            }

            if (direction == "down") {
                animator.SetBool("isDown", true);
            }
        }
    }

    bool isWalkable(string direction) {

        if (direction.Equals("right")) {
            if (walkableValues.Contains(levelMap[mapPos.x, mapPos.y + 1])) {
                return true;
            }
        }

        if (direction.Equals("left")) {
            if (walkableValues.Contains(levelMap[mapPos.x, mapPos.y - 1])) {
                return true;
            }
        }

        if (direction.Equals("up")) {
            if (walkableValues.Contains(levelMap[mapPos.x - 1, mapPos.y])) {
                return true;
            }
        }

        if (direction.Equals("down")) {
            if (walkableValues.Contains(levelMap[mapPos.x + 1, mapPos.y])) {
                return true;
            }
        }
        return false;
    }

    void SetMapPos(string direction) {
        endPos = transform.position;

        if (direction.Equals("right")) {
            mapPos.y += 1;
            endPos.x += 0.32f;
        }

        if (direction.Equals("left")) {
            mapPos.y -= 1;
            endPos.x -= 0.32f;
        }

        if (direction.Equals("up")) {
            mapPos.x -= 1;
            endPos.y += 0.32f;
        }

        if (direction.Equals("down")) {
            mapPos.x += 1;
            endPos.y -= 0.32f;
        }
    }

    string G1AI() {
        Vector2 tempPos = transform.position;
        int bias = 0;

        if (Random.value > 0.5f) {
            bias = 1;
        }

        // Up Case
        if (isWalkable("up") && prevDirection != "down" && bias == 0) {
            tempPos = transform.position;
            tempPos.y += 0.32f;

            if (Vector2.Distance(tempPos, player.transform.position) >= Vector2.Distance(transform.position, player.transform.position)) {
                return "up";
            }
        }

        // Down Case
        if (isWalkable("down") && prevDirection != "up" && bias == 1) {
            tempPos = transform.position;
            tempPos.y -= 0.32f;

            if (Vector2.Distance(tempPos, player.transform.position) >= Vector2.Distance(transform.position, player.transform.position)) {
                return "down";
            }
        }

        // Right Case
        if (isWalkable("right") && prevDirection != "left" && bias == 1) {
            tempPos = transform.position;
            tempPos.x += 0.32f;

            if (Vector2.Distance(tempPos, player.transform.position) >= Vector2.Distance(transform.position, player.transform.position)) {
                return "right";
            }
        }

        // Left Case
        if (isWalkable("left") && prevDirection != "right" && bias == 0) {
            tempPos = transform.position;
            tempPos.x -= 0.32f;

            if (Vector2.Distance(tempPos, player.transform.position) >= Vector2.Distance(transform.position, player.transform.position)) {
                return "left";
            }
        }

        return "direction";
        
    }

    string G2AI() {
        Vector2 tempPos = transform.position;

        // Up Case
        if (isWalkable("up") && prevDirection != "down") {
            tempPos = transform.position;
            tempPos.y += 0.32f;

            if (Vector2.Distance(tempPos, player.transform.position) <= Vector2.Distance(transform.position, player.transform.position)) {
                return "up";
            }
        }

        // Down Case
        if (isWalkable("down") && prevDirection != "up") {
            tempPos = transform.position;
            tempPos.y -= 0.32f;

            if (Vector2.Distance(tempPos, player.transform.position) <= Vector2.Distance(transform.position, player.transform.position)) {
                return "down";
            }
        }

        // Right Case
        if (isWalkable("right") && prevDirection != "left") {
            tempPos = transform.position;
            tempPos.x += 0.32f;

            if (Vector2.Distance(tempPos, player.transform.position) <= Vector2.Distance(transform.position, player.transform.position)) {
                return "right";
            }
        }

        // Left Case
        if (isWalkable("left") && prevDirection != "right") {
            tempPos = transform.position;
            tempPos.x -= 0.32f;

            if (Vector2.Distance(tempPos, player.transform.position) <= Vector2.Distance(transform.position, player.transform.position)) {
                return "left";
            }
        }

        return "direction";
    }

    string G3AI() {
        int bias = Random.Range(0,4);
        
        if (mapPos.x == 14 && mapPos.y == 12) {
            return "up";
        }
        if (mapPos.x == 13 && mapPos.y == 12) {
            return "up";
        }

        if (isWalkable("up") && prevDirection != "down" && bias == 0) {
            return "up";
        }

        if (isWalkable("down") && prevDirection != "up" && bias == 1) {
            return "down";
        }

        if (isWalkable("right") && prevDirection != "left" && bias == 2) {
            return "right";
        }

        if (isWalkable("left") && prevDirection != "right" && bias == 3) {
            return "left";
        }

        return "direction";
    }

    string G4AI() {
        if (mapPos.x == 14 && mapPos.y == 14) {
            return "up";
        }

        if (mapPos.x == 13 && mapPos.y == 14) {
            return "up";
        }

        if (mapPos.x == 12 && mapPos.y == 14) {
            return "left";
        }

        if (mapPos.x == 12 && mapPos.y == 13) {
            return "up";
        }

        if (mapPos.x == 12 && mapPos.y == 12) {
            return "left";
        }

        if (mapPos.x == 11 && mapPos.y == 12) {
            return "up";
        }

        // Top Left
        if (mapPos.x <= 14 && mapPos.y <= 14) {
            if (prevDirection == "down" && !(isWalkable("down"))) {
                return "right";
            }
            if (isWalkable("left") && prevDirection != "right") {
                return "left";
            }
            if (isWalkable("up") && prevDirection != "down") {
                return "up";
            }
            if (isWalkable("right") && prevDirection != "left") {
                return "right";
            }
            if (isWalkable("down") && prevDirection != "up") {
                return "down";
            }
        }
        // Top Right
        if (mapPos.x <= 14 && mapPos.y >= 14) {
            if (isWalkable("down") && prevDirection == "left" && prevDirection != "up") {
                return "down";
            }

            if (isWalkable("up") && prevDirection != "down") {
                return "up";
            }

            if (isWalkable("right") && prevDirection != "left") {
                return "right";
            }

            if (isWalkable("down") && !isWalkable("right") && prevDirection != "up") {
                return "down";
            }

            if (isWalkable("left") && prevDirection != "right" && prevDirection == "down") {
                return "right";
            }
        }

        // Bottom Left
        if (mapPos.x > 14 && mapPos.y <= 14) {
            if (isWalkable("down") && prevDirection == "left") {
                return "down";
            }

            if (isWalkable("up") && !isWalkable("left")) {
                return "up";
            }

            if (isWalkable("left") && prevDirection != "right") {
                return "left";
            }

            return NotMoving();
        }

        // Bottom Right
        if (mapPos.x > 14 && mapPos.y >= 14) {
            if (isWalkable("right") && prevDirection == "down") {
                return "right";
            }
            if (isWalkable("left") && prevDirection == "left") {
                return "left";
            }
            if (isWalkable("left") && prevDirection == "up") {
                return "left";
            }
            return NotMoving();
        }
        return NotMoving();
    }

    string NotMoving() {
        if (isWalkable("up") && prevDirection != "down") {
            return "up";
        }

        if (isWalkable("down") && prevDirection != "up") {
            return "down";
        }

        if (isWalkable("right") && prevDirection != "left") {
            return "right";
        }

        if (isWalkable("left") && prevDirection != "right") {
            return "left";
        }

        return "direction";
    }
}
