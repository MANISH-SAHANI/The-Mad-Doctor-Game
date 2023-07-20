using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour { 

    public static GamePlayController instance;

    [SerializeField] private Text enemyKillCountTxt;

    private int enemyKillCount;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void EnemyKilled() {
        enemyKillCount++;

        enemyKillCountTxt.text = "ENEMIES KILLED : " + enemyKillCount;

    }

    public void RestartGame() {
        Invoke("Restart", 3f);
    }
    public void Restart() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }

}
