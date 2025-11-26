using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cleaning : MonoBehaviour
{
    [SerializeField] private Table tableToClean;
    [SerializeField] private bool isCleaning;

    private void FixedUpdate()
    {
        if (isCleaning)
            tableToClean.CleanTable(0.1f);
    }
    public void DoCleaning(InputAction.CallbackContext context)
    {
        if (context.performed)
            isCleaning = true;

        else if (context.canceled)
            isCleaning = false;

    }
}
