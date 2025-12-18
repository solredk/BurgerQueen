using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{

    public void GoToMainGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GoToMinigame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

}