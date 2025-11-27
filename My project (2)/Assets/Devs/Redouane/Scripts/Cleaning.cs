using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cleaning : MonoBehaviour
{
    [SerializeField] private Table tableToClean;
    [SerializeField] private bool isCleaning;

    [SerializeField] private Camera camera;

    private void FixedUpdate()
    {
        if (isCleaning)
            tableToClean.CleanTable(0.1f);
    }
    public void DoCleaning(InputAction.CallbackContext context)
    {

        if (context.performed) 
        { 
            if (!isCleaning)
            {
                Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

                int layerMask = LayerMask.GetMask("Default");
                if (Physics.Raycast(ray, out RaycastHit hit, 20, layerMask, QueryTriggerInteraction.Ignore))
                {
                    if (hit.collider.TryGetComponent<Table>(out Table table))
                    {
                        if (table == tableToClean)
                        {
                            isCleaning = true;
                        }
                    }
                }

            }
        }
        else if (context.canceled)
            isCleaning = false;

    }

}
