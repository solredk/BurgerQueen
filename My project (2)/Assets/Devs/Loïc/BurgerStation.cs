using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class BurgerStation : MonoBehaviour
{
    private bool handEmpty = true;
    [SerializeField] private GameObject breadObj;
    [SerializeField] private GameObject burgerObj;
    [SerializeField] private GameObject slaObj;

    private Vector2 mousePos;
    //private float mouseY = Input.mousePosition.y;
    private GameObject currentIngredient;
    [SerializeField] private GameObject prepPlaceObj;
    private int ingedientanmount;

    void Start()
    {
      
    }

 

    public void JustGiveMeTheDamnMousePosition(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }

    void Update()
    {
        if (!handEmpty)
        {
            currentIngredient.transform.position = mousePos;
        }
    }

    public void OnPreCull()
    {
        if (!handEmpty)
        {
            if (ingedientanmount == 0)
            {
                Instantiate(currentIngredient, new Vector2(prepPlaceObj.transform.position.x, prepPlaceObj.transform.position.y - 200), Quaternion.identity);
                ingedientanmount++;
            }
            else if (ingedientanmount == 1)
            {
                Instantiate(currentIngredient, new Vector2(prepPlaceObj.transform.position.x, prepPlaceObj.transform.position.y - 100), Quaternion.identity);
                ingedientanmount++;
            }
            else if (ingedientanmount == 2)
            {
                Instantiate(currentIngredient, new Vector2(prepPlaceObj.transform.position.x, prepPlaceObj.transform.position.y), Quaternion.identity);
                ingedientanmount++;
            }
            else if (ingedientanmount == 3)
            {
                Instantiate(currentIngredient, new Vector2(prepPlaceObj.transform.position.x, prepPlaceObj.transform.position.y + 100), Quaternion.identity);
                ingedientanmount++;
            }
        }
        handEmpty = true;

    }

    public void BroodButton()
    {
        handEmpty = true;
        if (handEmpty)
        {
            currentIngredient = Instantiate(breadObj, mousePos, Quaternion.identity, gameObject.transform);
            handEmpty = false;
        }
    }
    public void BurgerButton()
    {
        handEmpty = true;
        if (handEmpty)
        {
            currentIngredient = Instantiate(breadObj, mousePos, Quaternion.identity, gameObject.transform);
            handEmpty = false;
        }
    }
    public void SlaButton()
    {
        handEmpty = true;
        if (handEmpty)
        {
            currentIngredient = Instantiate(breadObj, mousePos, Quaternion.identity, gameObject.transform);
            handEmpty = false;
        }
    }
}
