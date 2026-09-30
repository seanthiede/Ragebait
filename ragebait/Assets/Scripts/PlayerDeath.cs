using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerDeath : MonoBehaviour
{
    private bool isDead;

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        ReloadLevel();
    }

    private void ReloadLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
