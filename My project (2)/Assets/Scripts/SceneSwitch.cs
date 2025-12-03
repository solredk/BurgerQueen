using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }
    public void GoToMainGame()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToMinigame()
    {
        SceneManager.LoadScene(1);
    }

}