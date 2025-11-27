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
    private Grabing grabingS;


    void Start()
    {
        grabingS = FindAnyObjectByType<Grabing>();
        handEmpty = true;
    }

    void Update()
    {

    }

    public void FriesButton()
    {
        if (handEmpty)
        {
            currentIngredient = Instantiate(friesObj, mousePos, Quaternion.identity, gameObject.transform);
            grabingS.Helditem = currentIngredient;
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
