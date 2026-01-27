using UnityEngine;
using UnityEngine.InputSystem;

public class Cleaning : MonoBehaviour
{
    [SerializeField] private Table tableToClean;
    [SerializeField] private bool isCleaning;

    [SerializeField] private Camera cam;
    [SerializeField] private float counter;

    public DrinkManager m_drinkManager;

    private void Start()
    {
        m_drinkManager = FindAnyObjectByType<DrinkManager>();
    }

    public void DoCleaning(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!isCleaning)
            {
                Vector2 screenPos = Mouse.current.position.ReadValue();

                Ray ray = cam.ScreenPointToRay(screenPos);
               
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                {
                    Debug.Log(hit.collider.name);
                    if (hit.collider.name == "Pepis_Button")
                    {
                        m_drinkManager.drinknumber = 0;
                    }
                    if (hit.collider.name == "MD_Button")
                    {
                        m_drinkManager.drinknumber = 1;
                    }
                    if (hit.collider.name == "Fancy_Button")
                    {
                        m_drinkManager.drinknumber = 2;
                    }
                    if (hit.collider.name == "NiceTea_Button")
                    {
                        m_drinkManager.drinknumber = 3;
                    }
                    if (hit.collider.name == "Dr. Salt_Button")
                    {
                        m_drinkManager.drinknumber = 4;
                    }
                    if (hit.collider.name == "Ice Bananna")
                    {
                        m_drinkManager.drinknumber = 5;
                    }
                    if (hit.collider.name == "Ice Straberry")
                    {
                        m_drinkManager.drinknumber = 6;
                    }
                    if (hit.collider.name == "Ice Choclate")
                    {
                        m_drinkManager.drinknumber = 7;
                    }
                    print(m_drinkManager.drinknumber);

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
