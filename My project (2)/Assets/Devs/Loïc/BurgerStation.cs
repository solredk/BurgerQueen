using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

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
    private int ingedientanmount = 0;
    [SerializeField] private List<GameObject> burger;

    void Start()
    {
        handEmpty = true;
    }

    public void JustGiveMeTheDamnMousePosition(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }

    void Update()
    {
        if (!handEmpty)
        {
            currentIngredient.transform.position = mousePos - new Vector2(666, 530);
        }
    }

    public void OnPreCull()
    {
        if (!handEmpty)
        {
            if (ingedientanmount == 0)
            {
                GameObject ingredient = Instantiate(currentIngredient, new Vector2(prepPlaceObj.transform.position.x, prepPlaceObj.transform.position.y - 200), Quaternion.identity);
                burger.Add(ingredient);
            }
            else if (ingedientanmount == 1)
            {
                GameObject ingredient = Instantiate(currentIngredient, new Vector2(prepPlaceObj.transform.position.x, prepPlaceObj.transform.position.y - 100), Quaternion.identity);
                burger.Add(ingredient);
            }
            else if (ingedientanmount == 2)
            {
               GameObject ingredient = Instantiate(currentIngredient, new Vector2(prepPlaceObj.transform.position.x, prepPlaceObj.transform.position.y), Quaternion.identity);
                burger.Add(ingredient);
            }
            else if (ingedientanmount == 3)
            {
                GameObject ingredient = Instantiate(currentIngredient, new Vector2(prepPlaceObj.transform.position.x, prepPlaceObj.transform.position.y + 100), Quaternion.identity);
                burger.Add(ingredient);
            }
            Destroy(currentIngredient);
            handEmpty = true;
            ingedientanmount++;
        }

    }

    public void BroodButton()
    {
        if (handEmpty)
        {
            currentIngredient = Instantiate(breadObj, mousePos, Quaternion.identity, gameObject.transform);
            handEmpty = false;
        }
    }
    public void BurgerButton()
    {
        if (handEmpty)
        {
            currentIngredient = Instantiate(burgerObj, mousePos, Quaternion.identity, gameObject.transform);
            handEmpty = false;
        }
    }
    public void SlaButton()
    {
        if (handEmpty)
        {
            currentIngredient = Instantiate(slaObj, mousePos, Quaternion.identity, gameObject.transform);
            handEmpty = false;
        }
    }

    public void TrashButton()
    {
        if (!handEmpty)
        {
            Destroy(currentIngredient);
            handEmpty = true;
        }
    }

    public void ClearButton()
    {
        for (int i = 0; i < burger.Count; i++)
        {
            Destroy(burger[i]);
        }
        burger.Clear();
        ingedientanmount = 0;
    }
}
