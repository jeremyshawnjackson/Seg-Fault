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
    public GameObject LevelCompletedUI;
    private PlayerController Player;
    private AudioManagerController AudioManager;
    // private List<GameObject> Enemies;

    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        GameHasEnded = false;
        AudioManager = GameObject.Find("Audio Manager").GetComponent<AudioManagerController>();
    }

    void Update()
    {
        if (EnemiesDefeated())
        {
            Player.FinishedLevel();
            Invoke("CompleteLevel", SceneDelay);
        }
    }

    /*public void KilledEnemy(GameObject enemy)
    {
        if (Enemies.Contains(enemy))
        {
            Enemies.Remove(enemy);
        }
        Debug.Log(Enemies.Count + " enemies remain");
    }*/

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
