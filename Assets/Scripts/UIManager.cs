using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    #region TextFields
    Text localScore;
    Text countDown;
    Text localTimer;
    Text localGhostLabel;
    Text localGhostTimer;
    float timer;
    float gTimer;
    #endregion

    #region GetComponents
    SaveGameManager saveGame;
    GameObject life1;
    GameObject life2;
    GameObject life3;
    #endregion

    public bool canPlay;

    private void Start() {
        canPlay = false;
        timer = 0;
        gTimer = 10;
        saveGame = gameObject.GetComponent<SaveGameManager>();
        GetPlayerPrefs();
    }

    private void GetPlayerPrefs() {
        Text highScore = GameObject.FindGameObjectWithTag("localScore").GetComponent<Text>();
        Text bestTime = GameObject.FindGameObjectWithTag("localTimer").GetComponent<Text>();
        

        if (highScore != null && bestTime != null) {
            if (PlayerPrefs.GetString("High Score") != null) {
                highScore.text = PlayerPrefs.GetInt("High Score").ToString();
                bestTime.text = TimeFormat(PlayerPrefs.GetFloat("Best Time"));
            }
        }
    }

    private void Update() {
        if (canPlay) {
            if (localTimer != null) {
                timer += Time.deltaTime;

                localTimer.text = TimeFormat(timer);
            }

            if (localGhostTimer != null) {
                if (gTimer > 0) {
                    gTimer -= Time.deltaTime;

                    localGhostTimer.text = TimeFormat(gTimer);
                } else {
                    localGhostLabel.enabled = false;
                    localGhostTimer.enabled = false;
                }
            }

        }
    }

    private string TimeFormat(float time) {
        int timeInInt = (int)time;
        int min = timeInInt / 60;
        int sec = timeInInt % 60;
        float mil = (time * 100) % 100;

        string timeFormat = String.Format("{0:00}:{1:00}:{2:00}",
        min, sec, mil);

        return timeFormat;
    }

    public void LoadLevelOne() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(1);
    }

    public void QuitGame() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.buildIndex == 0) {
            GetPlayerPrefs();
        }
        if (scene.buildIndex == 1) {

            Button quitButton = GameObject.FindWithTag("QuitButton").GetComponent<Button>();
            quitButton.onClick.AddListener(QuitGame);

            localScore = GameObject.FindGameObjectWithTag("localScore").GetComponent<Text>();
            countDown = GameObject.FindGameObjectWithTag("countdown").GetComponent<Text>();
            localTimer = GameObject.FindGameObjectWithTag("localTimer").GetComponent<Text>();
            localGhostLabel = GameObject.FindGameObjectWithTag("GTimer").GetComponent<Text>();
            localGhostTimer = GameObject.FindGameObjectWithTag("localGhostTimer").GetComponent<Text>();

            StartCoroutine(DisplayStartSequence());
        }
    }

    public void UpdateLocalScore(int scoreToSet) {
        localScore.text = scoreToSet.ToString();
        if (scoreToSet > PlayerPrefs.GetInt("High Score")) {
            saveGame.SaveScore(scoreToSet);
            saveGame.SaveTime(timer);
        }

        if (scoreToSet == PlayerPrefs.GetInt("High Score") && timer < PlayerPrefs.GetFloat("Best Time")) {
            saveGame.SaveScore(scoreToSet);
            saveGame.SaveTime(timer);
        }
    }

    public void EnableGhostTimer() {
        gTimer = 10f;
        localGhostLabel.enabled = true;
        localGhostTimer.enabled = true;
    }

    public void DisplayGameOver() {
        countDown.enabled = true;
        countDown.text = "GAME OVER";
        canPlay = false;
        StartCoroutine(HandleGameOver());
    }

    public void UpdateLives(int lives) {
        life1 = GameObject.FindGameObjectWithTag("L1");
        life2 = GameObject.FindGameObjectWithTag("L2");
        life3 = GameObject.FindGameObjectWithTag("L3");

        // life1.SetActive(true);
        // life2.SetActive(true);
        // life3.SetActive(true);
        
        if (lives == 2) {
            life1.SetActive(false);
        }

        if (lives == 1) {
            life2.SetActive(false);
        }

        if (lives == 0) {
            life3.SetActive(false);
            DisplayGameOver();
        }
    }

    IEnumerator HandleGameOver() {
        yield return new WaitForSecondsRealtime(3);
        QuitGame();
    }

    IEnumerator DisplayStartSequence() {
        if (countDown != null) {
            countDown.text = "3";
            yield return new WaitForSecondsRealtime(1);
            countDown.text = "2";
            yield return new WaitForSecondsRealtime(1);
            countDown.text = "1";
            yield return new WaitForSecondsRealtime(1);
            countDown.text  = "GO!";
            yield return new WaitForSecondsRealtime(1);
            countDown.enabled = false;
            canPlay = true;
        }
    }
}
