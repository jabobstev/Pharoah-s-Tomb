using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void RestartGame()
    {
        GameManagerBehavior.hasToReset = true;
        SceneManager.UnloadScene("PharaohTomb_Onslaught_GameOver");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
