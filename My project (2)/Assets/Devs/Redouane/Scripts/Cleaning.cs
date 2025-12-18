using UnityEngine;
using UnityEngine.InputSystem;

public class Cleaning : MonoBehaviour
{
    [SerializeField] private Table tableToClean;
    [SerializeField] private bool isCleaning;

    [SerializeField] private Camera cam;
    [SerializeField] private float counter;

    public void DoCleaning(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!isCleaning)
            {
                Vector2 screenPos = Mouse.current.position.ReadValue();

                Ray ray = cam.ScreenPointToRay(screenPos);
               
                if (Physics.Raycast(ray, out RaycastHit hit, 100))
                {
                    if (hit.collider.TryGetComponent<Table>(out Table table))
                    {
                        tableToClean = table;
                        if (table == tableToClean)
                            tableToClean.isCleaning = true;
                    }
                }
            }
        }

        else if (context.canceled)
            tableToClean.isCleaning = false;
    }

}
