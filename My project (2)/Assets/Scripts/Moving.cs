using UnityEngine;
using UnityEngine.InputSystem;

public class Moving : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;
    private int index = 0;

    public void DoMoving(InputAction.CallbackContext context)
    {
        if (index < gameObjects.Length - 1)
        {
            gameObjects[index].SetActive(false);
            index++;
            gameObjects[index].SetActive(false);
        }
        else
        {
            gameObjects[index].SetActive(false);
            index = 0;
            gameObjects[index].SetActive(false);
        }


    }
}
