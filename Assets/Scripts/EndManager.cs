using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Debug.Log("Restarting the game...");
        SceneManager.LoadScene((int)SceneBuildIndex.MainMenu);
    }
}
