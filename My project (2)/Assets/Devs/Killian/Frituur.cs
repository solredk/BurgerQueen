using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;


public class Frituur : MonoBehaviour
{
    public bool handEmpty = true;
    public GameObject currentIngredient;
    [SerializeField] private GameObject friesObj;
    [SerializeField] public GameObject friedFriesobj;
    public Vector2 mousePos;

    void Start()
    {
        handEmpty = true;
    }

    void Update()
    {
        if (!handEmpty)
        {
            Debug.Log(mousePos);
            currentIngredient.transform.position = mousePos - new Vector2(666, 530);
        }
    }

    public void JustGiveMeTheDamnMousePosition(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }

    public void FriesButton()
    {
        if (handEmpty)
        {
            currentIngredient = Instantiate(friesObj, mousePos, Quaternion.identity, gameObject.transform);
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
}
