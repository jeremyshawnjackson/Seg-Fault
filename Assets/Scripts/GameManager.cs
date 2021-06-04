using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Redux;

public class GameManager : MonoBehaviour
{
    private bool GameHasEnded;
    public float SceneDelay = 1f;
    public AudioClip LevelCompleteSound;
    EnemyController EnemyController;
    private GameObject LevelCompletedUI;
    private PlayerController Player;
    private AudioManagerController AudioManager;
    private bool LevelCompleted;

    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        GameHasEnded = false;
        LevelCompleted = false;
        AudioManager = GameObject.Find("Audio Manager").GetComponent<AudioManagerController>();
        LevelCompletedUI = GameObject.Find("UI").transform.Find("LevelComplete").gameObject;
        Debug.Log(LevelCompletedUI.ToString());
    }

    void Update()
    {
        if (EnemiesDefeated() && !LevelCompleted)
        {
            LevelCompleted = true;
            Player.FinishedLevel();
            Invoke("CompleteLevel", SceneDelay);
        }
    }

    public bool EnemiesDefeated()
    {
        if (EnemyController.EnemyCount == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void EndGame()
    {
        if (GameHasEnded == false)
        {
            GameHasEnded = true;
            Debug.Log("Game Over!");
            Invoke("Restart", SceneDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CompleteLevel()
    {
        LevelCompletedUI.SetActive(true);
        AudioManager.PlayClip(LevelCompleteSound);
        Debug.Log("Level completed");
    }
}
