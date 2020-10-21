﻿using System;
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
    float timer;
    #endregion

    #region GetComponents
    SaveGameManager saveGame;
    #endregion

    public bool canPlay;

    private void Start() {
        canPlay = false;
        timer = 0;
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

    public void DisplayGameOver() {
        countDown.enabled = true;
        countDown.text = "GAME OVER";
        canPlay = false;
        StartCoroutine(HandleGameOver());
    }

    IEnumerator HandleGameOver() {
        yield return new WaitForSeconds(3);
        QuitGame();
    }

    IEnumerator DisplayStartSequence() {
        if (countDown != null) {
            countDown.text = "3";
            yield return new WaitForSeconds(1);
            countDown.text = "2";
            yield return new WaitForSeconds(1);
            countDown.text = "1";
            yield return new WaitForSeconds(1);
            countDown.text  = "GO!";
            yield return new WaitForSeconds(1);
            countDown.enabled = false;
            canPlay = true;
        }
    }
}
