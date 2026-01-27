using NUnit.Framework;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DrinkMachine : MonoBehaviour
{
    [SerializeField] private Mech mech;
    [SerializeField] private List <Material> ColorDrink;
    [SerializeField] private List<GameObject> Buttons;
    private int chosenColor;
    private float Size;
    private float hight;
    [SerializeField] private float timer = 2;
    private bool Ice = false;
    void Update()
    {
        PushButton();
        FillDrink();
    }
    private void drinkColor(int color)
    {
        if(mech.drink != null&& !mech.filling)
        {
            if(mech.drink.place!= null)
            {
                if(color != 0)
                {
                    mech.filling = true;
                    chosenColor = color;
                }
            }
        }
    }
    private void AddIce()
    {
        if (mech.drink != null && !mech.sprinkeling&&mech.filling)
        {
            if (mech.drink.place != null)
            {
                if (!mech.sprinkeling&& !mech.drink.Ice.activeSelf)
                {
                    mech.sprinkeling = true;
                    mech.drink.Ice.SetActive(true);
                }
            }
        }
    }
    private void Done()
    {
        if (mech.drink.Full)
        {
            mech.drink.Done = true;
            mech.filling = false;
            mech.sprinkeling = false;
            mech.drink = null;
        }
    }
    
    void PushButton()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray,out hit)&& Mouse.current.leftButton.IsPressed())
        {
            if (Buttons.Contains(hit.transform.gameObject))
            {
                MacheneButton button = hit.transform.GetComponent<MacheneButton>();
                switch (button.funcion)
                {
                    case 0: drinkColor(button.flaverNumber);break;
                    case 1: AddIce(); break;
                    case 2: Done(); break;
                    default: break;
                }
            }
        }
    }
    void FillDrink()
    {
        RaycastHit hit;
        Ray ray = new Ray(mech.Dispence.transform.position, mech.Dispence.transform.forward);
        Debug.DrawRay(mech.Dispence.transform.position, mech.Dispence.transform.forward*10, UnityEngine.Color.blue);
        if (Physics.Raycast(ray, out hit, 2))
        {
            if (hit.transform.gameObject.GetComponent<Glass>())
            {
                mech.drink = hit.transform.gameObject.GetComponent<Glass>();
                if (mech.drink.contaner == Glass.container.Drink&& mech.filling)
                {
                    float sizeX = 0;
                    float sizeZ = 0;
                    if (chosenColor != 0)
                    {
                        mech.poorDrink.SetActive(true);
                        Size = 0.125f;
                        hight = 0.13f;
                        sizeX = 0;
                        sizeZ = 0;
                    }
                    mech.poorDrink.GetComponent<MeshRenderer>().material = ColorDrink[chosenColor];
                    mech.drink.Drink.GetComponent<MeshRenderer>().material = ColorDrink[chosenColor];
                    timer = 20;
                    if (mech.drink.Drink.transform.localScale.y <= Size && !mech.drink.Full && mech.drink.place != null)
                    {
                        mech.drink.Drink.transform.localScale = Vector3.MoveTowards(mech.drink.Drink.transform.localScale, mech.drink.Drink.transform.localScale + new Vector3(sizeX * Time.deltaTime, Size * Time.deltaTime, sizeZ * Time.deltaTime), timer);
                        mech.drink.Drink.transform.position = Vector3.MoveTowards(mech.drink.Drink.transform.position, mech.drink.Drink.transform.position + new Vector3(0, hight * Time.deltaTime, 0), timer);
                    }
                    else if (mech.drink.Drink.transform.localScale.y >= Size)
                    {
                        mech.drink.Full = true;
                        mech.poorDrink.SetActive(false);
                    }
                }
            }
        }
    }
}
