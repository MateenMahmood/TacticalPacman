using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{   
    Text localScore;

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
        }
    }

    public void UpdateLocalScore(int scoreToSet) {
        localScore.text = scoreToSet.ToString();
    }
}
