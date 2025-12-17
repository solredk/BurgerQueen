using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
  
   public void MainGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

   public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }    

  public void EndScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
    }
}
