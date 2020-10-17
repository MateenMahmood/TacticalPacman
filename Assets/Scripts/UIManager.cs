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
    float timer;
    #endregion

    public bool canPlay;

    private void Start() {
        canPlay = false;
        timer = 0;
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
        float mil = (time * 1000) % 1000;

        string timeFormat = String.Format("{0:00}:{1:00}:{2:00}",
        min, sec, mil);

        return timeFormat;
    }

    public void LoadLevelOne() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(1);
    }

    public void QuitGame() {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
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
    }

    IEnumerator DisplayStartSequence() {
        Debug.Log("I can run?");
        if (countDown != null) {
            Debug.Log("I AM RUNNING!!!");
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
