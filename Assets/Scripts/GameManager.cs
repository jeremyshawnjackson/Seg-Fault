using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool GameHasEnded;
    public float SceneDelay = 1f;
    EnemyController EnemyController;
    public GameObject LevelCompletedUI;
    // private List<GameObject> Enemies;

    void Start()
    {
        GameHasEnded = false;
    }

    void Update()
    {
        if (EnemiesDefeated())
        {
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
        Debug.Log("Level completed");
    }
}
