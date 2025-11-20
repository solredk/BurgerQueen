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
    private Vector2 mousePos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        handEmpty = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!handEmpty)
        {
            currentIngredient.transform.position = mousePos;
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
