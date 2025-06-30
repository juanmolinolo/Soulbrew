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
        SceneManager.LoadScene((int)SceneBuildIndex.MainMenu);
    }

    public void ToggleMute()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
}
