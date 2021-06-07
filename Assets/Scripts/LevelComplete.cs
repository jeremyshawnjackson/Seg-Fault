using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public void LoadNextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextLevel);
        } else{
            SceneManager.LoadScene(0);
        }
    }
}
