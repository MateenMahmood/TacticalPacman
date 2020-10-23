﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    Normal,
    Dead,
    Power
}

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
    
    List<int> walkableValues;
    Vector2Int mapPos;
    Vector3 prevPos;
    Vector3 endPos;
    Tweener tweener;
    [SerializeField] Animator animator = null;
    [SerializeField] ParticleSystem particle = null;
    [SerializeField] GameObject wallColl = null;
    bool wallSoundTrig;
    bool coinSoundTrig;
    float coinTimer;
    public int localScore;
    int lives;
    UIManager uIManager;
    CharacterGen characterGen;
    KeyCode lastInput;
    KeyCode currentInput;
    [SerializeField] AudioSource soundSource = null;
    [SerializeField] List<AudioClip> soundClips = null;
    public PlayerState playerState;
    GameObject ghost1;
    GameObject ghost2;
    GameObject ghost3;
    GameObject ghost4;

    void Start() {
        walkableValues = new List<int>();
        walkableValues.Add(0);
        walkableValues.Add(5);
        walkableValues.Add(6);
        prevPos = transform.position;
        mapPos = new Vector2Int(1, 1);
        tweener = gameObject.GetComponent<Tweener>();
        wallSoundTrig = false;
        coinSoundTrig = false;
        localScore = 0;
        lives = 3;
        playerState = PlayerState.Normal;
        uIManager = GameObject.FindGameObjectWithTag("managers").GetComponent<UIManager>();
        characterGen = GameObject.FindGameObjectWithTag("LevelGen").GetComponent<CharacterGen>();
        ghost1 = GameObject.FindGameObjectWithTag("G1");
        ghost2 = GameObject.FindGameObjectWithTag("G2");
        ghost3 = GameObject.FindGameObjectWithTag("G3");
        ghost4 = GameObject.FindGameObjectWithTag("G4");
    }
    void Update() {

        CheckPellets();

        HandleDead();

        uIManager.UpdateLocalScore(localScore);

        if (uIManager.canPlay) {

            HandleAnim();
            CollisionHandler();

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

            if (tweener == null) {
                Debug.Log("Tweener is null! Script execution is weird");
            }

            if (!(tweener.TweenExists(transform))) {
                // Player is not moving
                
                // Right
                if (isWalkable(lastInput)) {
                    // Walkable
                    currentInput = lastInput;
                    tweener.AddTween(transform, transform.position, endPos, 2f);
                } else {
                    // Not Walkable
                    // Check previous input
                    if (isWalkable(currentInput)) {
                        tweener.AddTween(transform, transform.position, endPos, 2f);
                    }
                }

                if (!(animator.GetBool("isMoving"))) {
                    soundSource.clip = soundClips[2];
                }

                if (!soundSource.isPlaying && wallSoundTrig) {
                    Instantiate(wallColl, transform.position, Quaternion.identity);
                    soundSource.Play();
                    wallSoundTrig = false;
                }
            } else {
                wallSoundTrig = true;
                HandleAudio();
            }
        }
        prevPos = transform.position;
    }

    void CheckPellets() {
        bool flag = true;
        if (localScore >= 2180) {
            for (int i = 0; i < levelMap.GetLength(0); i++) {
                for (int j = 0; j < levelMap.GetLength(1); j++) {
                    if (levelMap[i,j] == 5) {
                        flag = false;
                    }
                }
            }
            if (flag) {
                HandleAnim();
                uIManager.DisplayGameOver();
            }
        }
    }

    void CollisionHandler() {
        Vector2 rayOrigin = transform.position;
        Vector2 rayDirect = transform.forward;
        
        RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirect);

        if (hit2D) {
            // Check if the gameObject has a collider
            if (hit2D.collider != null) {

                string colliderTag = hit2D.collider.gameObject.tag;
                
                // Left Teleporter
                if (colliderTag == "teleL") {
                    Debug.Log("Collision at Left Teleporter");
                    TeleportPlayer(new Vector3(8.48f, -4.64f, transform.position.z), new Vector2Int(14, 26));
                }
                // Right Teleporter
                if (colliderTag == "teleR") {
                    Debug.Log("Collision at Right Teleporter");
                    TeleportPlayer(new Vector3(0.48f, -4.64f, transform.position.z), new Vector2Int(14, 1));
                }

                // Normal Pellet
                if (colliderTag == "pellet") {
                    localScore += 10;
                    levelMap[mapPos.x, mapPos.y] = 0;
                    coinSoundTrig = true;
                    Destroy(hit2D.collider.gameObject);
                }

                // Power Pellet
                if (colliderTag  == "power") {
                    HandlePower();
                    Destroy(hit2D.collider.gameObject);
                }

                // Cherry
                if (colliderTag == "cherry") {
                    localScore += 100;
                    Destroy(hit2D.collider.gameObject);
                }

                // Any Ghost
                if ((colliderTag == "G1" || colliderTag == "G2" || colliderTag == "G3" || colliderTag == "G4") && playerState == PlayerState.Normal) {
                    Debug.Log("Player Reporting Death");
                    playerState = PlayerState.Dead;
                }

                if ((colliderTag == "G1" || colliderTag == "G2" || colliderTag == "G3" || colliderTag == "G4") && playerState == PlayerState.Power) {
                    GhostController tempController = hit2D.collider.GetComponent<GhostController>();
                    if (tempController.ghostState != GhostState.Dead) {
                        Debug.Log("Player Reporting Ghost Kill");
                        tempController.HandleDeath();
                    }
                }
            }
        }
    }

    void HandlePower() {
        levelMap[mapPos.x, mapPos.y] = 0;
        playerState = PlayerState.Power;
        uIManager.EnableGhostTimer();
        StartCoroutine(PowerCount());
    }

    IEnumerator PowerCount() {
        yield return new WaitForSecondsRealtime(10);
        playerState = PlayerState.Normal;
    }

    void HandleDead() {
        if (playerState == PlayerState.Dead) {
            if (!tweener.TweenExists(transform)) {
                ghost1 = GameObject.FindGameObjectWithTag("G1");
                ghost2 = GameObject.FindGameObjectWithTag("G2");
                ghost3 = GameObject.FindGameObjectWithTag("G3");
                ghost4 = GameObject.FindGameObjectWithTag("G4");
                
                Destroy(ghost1);
                Destroy(ghost2);
                Destroy(ghost3);
                Destroy(ghost4);
                lives -= 1;

                // Never set lives to less than 0
                if (lives < 0) {
                    lives = 0;
                }

                animator.SetBool("isMoving", false);
                animator.SetBool("goingUp", false);
                animator.SetBool("goingDown", false);
                animator.SetBool("goingLeft", false);
                animator.SetBool("goingRight", false);

                Instantiate(wallColl, transform.position, Quaternion.identity);
                Instantiate(wallColl, transform.position, Quaternion.identity);
                Instantiate(wallColl, transform.position, Quaternion.identity);

                currentInput = KeyCode.Space;
                lastInput = KeyCode.Space;

                TeleportPlayer(new Vector3(0.48f, -0.48f, 0), new Vector2Int(1, 1));
                uIManager.UpdateLives(lives);
                characterGen.RespawnGhost();
                playerState = PlayerState.Normal;
            }
        }
    }

    void TeleportPlayer(Vector3 pos, Vector2Int mapCoord) {
        if (!(tweener.TweenExists(transform))) {
            gameObject.transform.position = pos;
            mapPos = mapCoord;
        }
    }

    bool isWalkable(KeyCode keyCode) {
        endPos = transform.position;

        if (keyCode == KeyCode.D) {
            if (walkableValues.Contains(levelMap[mapPos.x, mapPos.y + 1])) {
                mapPos.y += 1;
                endPos.x += 0.32f;
                return true;
            }
        }

        if (keyCode == KeyCode.A) {
            if (walkableValues.Contains(levelMap[mapPos.x, mapPos.y - 1])) {
                mapPos.y -= 1;
                endPos.x -= 0.32f;
                return true;
            }
        }

        if (keyCode == KeyCode.W) {
            if (walkableValues.Contains(levelMap[mapPos.x - 1, mapPos.y])) {
                mapPos.x -= 1;
                endPos.y += 0.32f;
                return true;
            }
        }

        if (keyCode == KeyCode.S) {
            if (walkableValues.Contains(levelMap[mapPos.x + 1, mapPos.y])) {
                mapPos.x += 1;
                endPos.y -= 0.32f;
                return true;
            }
        }

        return false;
    }

    void HandleAnim() {

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

        if (isMoving) {

            if (!particle.isPlaying) {
                particle.Play();
            }

        } else {

            if (particle.isPlaying) {
                particle.Stop();
            }
        }
    }

    void HandleAudio() {
        
        
        if (transform.position != prevPos) {
            if (levelMap[mapPos.x, mapPos.y] == 0 || levelMap[mapPos.x, mapPos.y] == 5) {
                soundSource.clip = soundClips[0];
                wallSoundTrig = true;
            }
        } else {
            if (wallSoundTrig) {
                soundSource.clip = soundClips[2];
                wallSoundTrig = false;
            }
        }

        if (soundSource.isPlaying && coinSoundTrig) {
            soundSource.Stop();
        }

        if (!soundSource.isPlaying) {

            if (coinSoundTrig) {
                soundSource.PlayOneShot(soundClips[1]);
                coinSoundTrig = false;
            } else {
                soundSource.Play();
            }
        }
    }
}
