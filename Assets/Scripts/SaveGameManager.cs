using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : MonoBehaviour {
    #region keys
    const string saveScoreKey = "High Score";
    const string saveTimeKey = "Best Time";
    #endregion

    #region GetComponents
    UIManager uIManager;
    #endregion
    
    private void Start() {
        uIManager = GameObject.FindGameObjectWithTag("managers").GetComponent<UIManager>();
    }

    public void SaveScore(int score) {
        int saveValue = score;
        string loadValue = PlayerPrefs.GetString(saveScoreKey);
        if (!saveValue.Equals(loadValue)) {
            PlayerPrefs.SetInt(saveScoreKey, saveValue);
            PlayerPrefs.Save();
        }
    }

    public void SaveTime(float time) {
        float saveValue = time;
        string loadValue = PlayerPrefs.GetString(saveTimeKey);
        if (!saveValue.Equals(loadValue)) {
            PlayerPrefs.SetFloat(saveTimeKey, saveValue);
            PlayerPrefs.Save();
        }
    }
}
